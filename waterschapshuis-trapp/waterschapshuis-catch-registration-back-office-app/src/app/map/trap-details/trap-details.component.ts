import { Component, EventEmitter, Input, Output, OnChanges, SimpleChanges, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { Coordinate } from 'ol/coordinate';
import {
  CatchCreateOrUpdateCommand,
  IGetTrapDetailsTrapItem,
  TrapUpdateCommand,
  TrapStatus,
  GetCatchDetailsCatchItem
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { SideBarActions } from 'src/app/core/side-bar/side-bar-abstract/side-bar-actions.enum';
import { SideBarPosition } from 'src/app/core/side-bar/side-bar-abstract/side-bar-positions.enum';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { AlertAction, AlertContentComponent } from 'src/app/shared/alert/alert/alert-content.component';
import { IListItem, ListItem } from 'src/app/shared/models/list-item';
import { OverlayLayerName } from 'src/app/shared/models/overlay-layer-name.enum';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { ToastService } from 'src/app/shared/toast/toast.service';

import { EditCatchesComponent } from '../components/edit-catches/edit-catches.component';
import { CatchDetails } from '../models/catch-details.model';
import { TrapDetails } from '../models/trap-details.model';
import { TrapStatusNL } from '../models/trap-status.enum';
import { CatchService } from '../services/catch.service';
import { MapStateService } from '../services/map-state.service';
import { TrapsService } from '../services/traps.service';

@Component({
  selector: 'app-trap-details',
  templateUrl: './trap-details.component.html',
  styleUrls: ['./trap-details.component.scss']
})
export class TrapDetailsComponent implements OnChanges, OnInit {
  @Input() trap: TrapDetails;
  @Output() trapPanelVisible = new EventEmitter();
  trapTypes: IListItem[] = [];
  trapStatuses: IListItem[] = [];
  protected alertActions: AlertAction[] = [
    { text: 'Nee', action: false, isDefault: false, emptyFillButton: true },
    { text: 'Ja', action: true, isDefault: true }
  ];

  form: FormGroup;
  policyName = PolicyName;

  constructor(
    private sideBarService: SideBarService,
    public trapsService: TrapsService,
    private toastService: ToastService,
    private catchesService: CatchService,
    private dialog: MatDialog,
    private mapService: MapStateService,
    private lookupsService: LookupsService,
    private fb: FormBuilder) {
    this.createForm();
  }

  ngOnInit(): void {
    this.loadTrapTypes();
    this.disableFromFields();
  }
  private disableFromFields() {
    const trapTypeCtrl = this.form.get('trapTypeId');
    const trapStatusCtrl = this.form.get('trapStatus');
    const trapTypeCtrlDisabled = this.isRemoved() || this.hasCatches() || this.notCreatedToday();
    if (trapTypeCtrlDisabled) {
      trapTypeCtrl.disable();
    }
    if (this.isRemoved()) {
      trapStatusCtrl.disable();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.trap && changes.trap.currentValue) {
      this.trap = changes.trap.currentValue;
      this.form.patchValue({
        ...this.trap,
        trapStatus: this.trap.status.toString()
      });
    }
  }

  toggleSideBar(event: Event): void {
    event?.stopPropagation();
    this.sideBarService.toggleSideBar({ position: SideBarPosition.end, action: SideBarActions.close });
  }

  navigateBack(): void {
    this.trapPanelVisible.emit(false);
  }

  loadTrapTypes() {
    this.lookupsService.getTrapTypes(this.trap.trappingTypeId)
      .subscribe(response => {
        this.trapTypes = ListItem.mapToListItems(response);
        this.filterTrapStatuses(response.find(x => x.id === this.trap.trapTypeId).allowedStatuses);
      });
  }

  filterTrapStatuses(allowedStatuses: TrapStatus[]): void {
    allowedStatuses.forEach(statusId =>
      this.trapStatuses.push({
        id: statusId.toString(),
        name: TrapStatusNL[statusId]
      }));
  }

  isRemoved = (): boolean => this.trap.status === TrapStatus.Removed;

  hasCatches = (): boolean => this.trap.catches.length > 0;

  notCreatedToday = (): boolean => this.trap.createdOn.toDateString() !== new Date().toDateString();

  editCatchesDialog(obj: CatchDetails) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      catch: obj
    };

    const dialogRef = this.dialog.open(EditCatchesComponent, dialogConfig);

    dialogRef.afterClosed()
      .subscribe(data => data ?
        this.saveCatch(this.catchCreateOrUpdateCommand(data.catch)) : {});
  }

  catchCreateOrUpdateCommand(obj: CatchDetails): CatchCreateOrUpdateCommand {
    return CatchCreateOrUpdateCommand.fromJS({
      id: obj.id,
      trapId: this.trap.id,
      catchTypeId: obj.catchTypeId,
      number: obj.number
    });
  }

  saveCatch(cmd: CatchCreateOrUpdateCommand): void {
    this.catchesService.post(cmd)
      .subscribe((data: GetCatchDetailsCatchItem) => {
        this.trap.catches = this.trap.catches.filter(c => c.id !== data.id);
        this.trap.catches.push(CatchDetails.fromResponse(data));
        this.mapService.refreshLayers(OverlayLayerName.TrapDetails);
      });
  }

  saveTrap(): void {
    const cmd = this.createTrapUpdateCommand();
    this.save(cmd);
  }

  moveTrap() {
    const cmd = this.createMovingTrapCommand(this.mapService.mapCenter);
    this.save(cmd);
    this.trapsService.setMovingTrapInProgress(false);
  }

  save(command: TrapUpdateCommand) {
    if (this.form.invalid) {
      return;
    }

    this.trapsService.post(command)
      .subscribe(
        (data: IGetTrapDetailsTrapItem) => {
          this.trap = TrapDetails.fromResponse(data);
          this.mapService.refreshLayers(OverlayLayerName.TrapDetails);
          this.trapPanelVisible.emit(false);
        });
  }

  createTrapUpdateCommand() {
    return TrapUpdateCommand.fromJS({
      id: this.trap.id,
      trapTypeId: this.form.getRawValue().trapTypeId,
      remarks: this.form.value.remarks,
      status: Number(this.form.get('trapStatus').value),
      longitude: this.trap.longitude,
      latitude: this.trap.latitude
    });
  }

  createMovingTrapCommand(coordinates?: Coordinate) {
    return TrapUpdateCommand.fromJS({
      id: this.trap.id,
      trapTypeId: this.trap.trapTypeId,
      remarks: this.trap.remarks,
      status: this.trap.status,
      longitude: coordinates ? coordinates[0] : this.trap.longitude,
      latitude: coordinates ? coordinates[1] : this.trap.latitude
    });
  }

  showConfirmDialog() {
    const dialogRef = this.createAlertDialog(
      'Verwijdering',
      'Weet u zeker dat u een vangmiddel wilt verwijderen?'
    );

    dialogRef.afterClosed().toPromise().then(result => {
      if (result) {
        this.deleteTrap();
      }
    });
  }

  deleteTrap() {
    this.trapsService.deleteTrap(this.trap.id).toPromise().then(result => {
      if (result) {
        this.toastService.showSuccessMessage('Vangmiddel succesvol verwijderd');
        this.mapService.refreshLayers(OverlayLayerName.TrapDetails);
        this.toggleSideBar(null);
      } else {
        this.toastService
          .showErrorMessage('Vangmiddel kan niet worden verwijderd');
      }
    });
  }

  canRemoveCatch = (item: CatchDetails): boolean => {
    return item.createdOn.toDateString() === (new Date()).toDateString();
  }

  onClickRemoveCatch = (item: CatchDetails) => {
    const dialogRef = this.createAlertDialog(
      'Verwijdering',
      'Wilt u zeker deze vangst verwijderen?'
    );

    dialogRef.afterClosed().toPromise().then(result => {
      if (result) {
        this.catchesService
          .delete(item.id)
          .subscribe(
            () => {
              this.toastService.showSuccessMessage('Vangst is verwijderd.');
              this.trap.catches = this.trap.catches.filter(x => x.id !== item.id);
              this.mapService.refreshLayers(OverlayLayerName.TrapDetails);
            },
            err => this.toastService.validationError(err, false)
          );
      }
    });
  }

  private createForm(): void {
    this.form = this.fb.group({
      trapTypeId: [null, Validators.required],
      remarks: null,
      trapStatus: [null, Validators.required]
    });
  }

  private createAlertDialog = (dialogTitle: string, dialogMessage: string): MatDialogRef<AlertContentComponent, any> =>
    this.dialog.open(AlertContentComponent, {
      data: {
        title: dialogTitle,
        header: '',
        message: dialogMessage,
        actions: this.alertActions
      }
    })
}
