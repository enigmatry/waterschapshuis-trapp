import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IonContent, NavController } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { take } from 'rxjs/operators';
import { TrackingSyncService } from 'src/app/maps/services/tracking-sync.service';
import { Subscription } from 'rxjs';
import { TrackingService } from 'src/app/maps/services/tracking.service';
import { LoaderService } from 'src/app/services/loader.service';
import { ToastService } from 'src/app/services/toast.service';
import { AreaDetails } from 'src/app/shared/models/area-details';
import { IListItem } from 'src/app/shared/models/list-item';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AreasService } from 'src/app/shared/services/areas.service';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { SyncService } from 'src/app/sync/sync.service';

import { TimeRegistrationForm } from '../models/time-registration';
import { TimeRegistrationService } from '../services/time-registration.service';
import { TimeRegistrationFormState } from '../models/time-registration-form-state.enum';
import { parseHours, parseMinutes, toDisplayString } from 'src/app/shared/models/utils';
import { TimeRegistrationStatus } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

@Component({
  selector: 'app-time-registration',
  templateUrl: './time-registration.component.html',
  styleUrls: ['./time-registration.component.scss'],
})
export class TimeRegistrationComponent extends OnDestroyMixin implements OnInit, OnDestroy {
  timeRegistrationForm: FormGroup;
  timeRegistrationItems: FormArray;
  timeRegistrationGeneralItems: FormArray;
  timeRegistrationItemsInCreation: FormArray;
  timeRegistrationSub: Subscription;

  areaDetails: AreaDetails;

  today: string = this.timeRegistrationService.getTodayUtcDateShortString();
  date: string = this.today;
  catchAreas: IListItem[] = [];
  subAreas: IListItem[] = [];
  hourSquares: IListItem[] = [];
  trappingTypes: IListItem[] = [];
  categories: IListItem[] = [];
  loader: any;
  hasChanges = false;
  totalTimeForFilteredOutItems = 0;
  totalTime: string;
  hasAnyLoadedItems: boolean;
  canAddNew: boolean;

  timeRegistrationFormState = TimeRegistrationFormState;
  @ViewChild('content') content: IonContent;
  constructor(
    public navController: NavController,
    private router: Router,
    private trackingSyncService: TrackingSyncService,
    private trackingService: TrackingService,
    private timeRegistrationService: TimeRegistrationService,
    private areaService: AreasService,
    private lookupsService: LookupsService,
    private toastService: ToastService,
    private loaderService: LoaderService,
    private alertService: AlertService,
    private syncService: SyncService,
    private changeDetector: ChangeDetectorRef
  ) {
    super();
    const state = this.router.getCurrentNavigation().extras?.state;
    this.areaDetails = state?.location;
  }

  ngOnInit() {
    this.initView().then(
      () => {

        this.timeRegistrationSub =
          this.timeRegistrationService.timeRegistration$
            .subscribe((timeRegistration: FormGroup) => {
              this.timeRegistrationForm = timeRegistration;
              this.timeRegistrationItems = this.timeRegistrationForm.get('items') as FormArray;
              this.timeRegistrationGeneralItems = this.timeRegistrationForm.get('generalItems') as FormArray;
              this.timeRegistrationItemsInCreation = this.timeRegistrationForm.get('inCreation') as FormArray;
              this.hasAnyLoadedItems = timeRegistration.get('hasAnyLoadedItem').value;
              this.canAddNew = timeRegistration.get('canAddNew').value;
              this.totalTimeForFilteredOutItems = timeRegistration.get('totalTimeOfFilteredOutItems').value;
              this.calculateTotalTime();
              this.changeDetector.detectChanges();
            });

      },
      () => {
        this.loader.dismiss();
        this.toastService.error('Fout opgetreden tijdens inlezen data');
      }
    );
  }

  ngOnDestroy() {
    if (this.timeRegistrationSub) {
      this.timeRegistrationSub.unsubscribe();
    }
  }

  async initView(): Promise<void> {
    this.loader = await this.loaderService.createLoader();
    this.loader.present();

    if (!this.areaDetails) {
      this.catchAreas = await this.areaService.getCatchAreas().toPromise();
    } else {
      this.catchAreas.push(this.areaDetails.catchArea);
      this.subAreas.push(this.areaDetails.subArea);
      this.hourSquares.push(this.areaDetails.hourSquare);
    }

    this.trappingTypes = await this.lookupsService.getTrappingTypes().toPromise();
    this.categories = await this.lookupsService.getTimeRegistrationActiveCategories().toPromise();

    this.trackingService.stopBackgroundGeolocation();

    if (this.syncService.isRunning) {
      this.syncService.isRunning$
        .pipe(
          take(1),
          untilComponentDestroyed(this)
        )
        .subscribe(async () => {
          this.loader.dismiss();
          await this.loadTimeRegistration(this.date);
        });
    } else {
      this.loader.dismiss();

      await this.syncService.sync([this.trackingSyncService], { disableSyncCancel: true });

      await this.loadTimeRegistration(this.date);
    }
  }

  async loadTimeRegistration(date: string): Promise<void> {
    this.loader = await this.loaderService.createLoader();
    this.loader.present();

    await this.timeRegistrationService.loadTimeRegistration(date, this.areaDetails?.subAreaHourSquareId).toPromise();

    this.loader.dismiss();
  }

  saveButtonDisabled(): boolean {
    return !this.saveButtonEnabled();
  }

  saveButtonEnabled(): boolean {
    return (this.anyItemInWrittenStatus() && this.timeRegistrationForm?.valid) || (this.hasAnyLoadedItems && this.allItemsEmpty());
  }

  allItemsEmpty(): boolean {
    return !this.timeRegistrationItems?.length &&
      !this.timeRegistrationGeneralItems?.length &&
      !this.timeRegistrationItemsInCreation?.length;
  }

  anyItemInWrittenStatus(): boolean {
    return this.anyInStatus(this.timeRegistrationItems?.controls, TimeRegistrationStatus.Written) ||
      this.anyInStatus(this.timeRegistrationGeneralItems?.controls, TimeRegistrationStatus.Written) ||
      this.anyInStatus(this.timeRegistrationItemsInCreation?.controls, TimeRegistrationStatus.Written);
  }

  anyInStatus(items: AbstractControl[], status: TimeRegistrationStatus) {
    return items?.some(t => t.get('status').value === status);
  }

  async saveTimeRegistration(): Promise<void> {
    if (!this.timeRegistrationForm.valid) {
      return;
    }

    this.loader = await this.loaderService.createLoader();
    this.loader.present();

    this.pushAddedDataToAppropriateFormArray(this.timeRegistrationForm.value);

    const command = TimeRegistrationForm.fromFormGroup(this.timeRegistrationForm).toCommand();
    command.subAreaHourSquareId = this.areaDetails?.subAreaHourSquareId;

    this.timeRegistrationService.saveTimeRegistration(command).subscribe(
      () => {
        this.hasChanges = false;
        this.hasAnyLoadedItems = !this.allItemsEmpty();
      },
      err => {
        this.loader.dismiss();
        this.toastService.validationError(err, false);
      },
      () => {
        this.loader.dismiss();
      }
    );
  }

  pushAddedDataToAppropriateFormArray(timeRegistrationForm: any) {
    if (timeRegistrationForm.inCreation.length > 0) {
      timeRegistrationForm.inCreation.forEach(item => {
        if (this.itemBelongsToGeneralItems(item)) {
          this.timeRegistrationService.addItemToTimeRegistrationGeneralItemList(item);
        } else {
          this.timeRegistrationService.addItemToTimeRegistrationItemList(item);
        }
      });

      this.clearFormArray(this.timeRegistrationForm.get('inCreation') as FormArray);
    }
  }
  private itemBelongsToGeneralItems(item: any) {
    return item.category !== null && item.generalTime !== null;
  }
  private clearFormArray(formArray: FormArray) {
    while (formArray.length !== 0) {
      formArray.removeAt(0);
    }
  }

  addTimeRegistrationItem(): void {
    this.timeRegistrationService.addTimeRegistrationItem();
    this.hasChanges = true;
    this.scrollToBottom();
  }

  scrollToBottom() {
    setTimeout(() => {
      this.content.scrollToBottom(300);
   }, 1000);
  }

  itemDeleted(data: any): void {
    const formArrayName = this.getFormArrayNameByState(data.state);

    this.timeRegistrationService.deleteTimeRegistrationItem(data.index, formArrayName);
    this.hasChanges = true;
  }

  private getFormArrayNameByState(state: TimeRegistrationFormState) {
    return state === TimeRegistrationFormState.Creation ? 'inCreation' :
      state === TimeRegistrationFormState.Bestrijding ? 'items' : 'generalItems';
  }

  itemChanged(): void {
    this.hasChanges = true;
    this.calculateTotalTime();
  }

  dateChanged(event: CustomEvent): void {
    const value = event.detail.value instanceof Date ? event.detail.value.toISOString() : event.detail.value.toString();
    const date = value.substring(0, 10);
    this.loadTimeRegistration(date);
  }

  calculateTotalTime(): void {
    const allItemsTotalTime =
      this.totalTimeForFilteredOutItems +
      this.getTotalTime(this.timeRegistrationItems.controls, 'time') +
      this.getTotalTime(this.timeRegistrationGeneralItems.controls, 'generalTime') +
      this.getTotalTime(this.timeRegistrationItemsInCreation.controls, 'time') +
      this.getTotalTime(this.timeRegistrationItemsInCreation.controls, 'generalTime');

    this.totalTime = toDisplayString(allItemsTotalTime);
  }

  private getTotalTime(controls: AbstractControl[], timeControlName: string): number {
    if (controls.length === 0) { return 0; }

    return controls.map(x =>
      parseHours(x.get(timeControlName)?.value) * 60 + parseMinutes(x.get(timeControlName)?.value)
    ).reduce((a, b) => a + b, 0);
  }

  async navigateBack(event: CustomEvent): Promise<void> {
    event.stopPropagation();

    if (this.hasChanges) {
      const confirm = await this.alertService.getConfirmDialog(
        'Bevestig teruggaan',
        'Weet u zeker dat u wilt annuleren? Uw wijzingen zullen niet bewaard worden.',
        'Nee',
        'Ja',
        this.doNavigateBack,
        {
          nav: this.navController
        }
      );
      await confirm.present();
    } else {
      await this.doNavigateBack(true);
    }
  }

  async doNavigateBack(dialogResult: boolean, args: any = null): Promise<void> {
    if (dialogResult) {
      if (args) {
        const nav = args.nav as NavController;
        nav.back();
      } else {
        this.navController.back();
      }

    }
  }

}
