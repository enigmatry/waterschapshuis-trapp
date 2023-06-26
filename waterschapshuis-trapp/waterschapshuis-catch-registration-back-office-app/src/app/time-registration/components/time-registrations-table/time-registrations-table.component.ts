import { Component, OnInit, Input } from '@angular/core';
import { FormArray } from '@angular/forms';
import { IListItem } from 'src/app/shared/models/list-item';
import { AreasService } from 'src/app/shared/services/areas.service';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { TimeRegistrationStatus } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { TotalTimeHelper } from '../../models/registered-time-helper';

@Component({
  selector: 'app-time-registrations-table',
  templateUrl: './time-registrations-table.component.html',
  styleUrls: ['./time-registrations-table.component.scss']
})
export class TimeRegistrationsTableComponent implements OnInit {
  @Input() timeRegistrationData: FormArray;
  @Input() enabledStatus: TimeRegistrationStatus;
  @Input() disableAddingNewItems: boolean;
  @Input() set rayonId(newValue: string) {
    this.changeCatchAreas(newValue);
  }

  catchAreas: IListItem[] = [];
  trappingTypes: IListItem[] = [];

  columns: Array<string> = ['catchArea', 'subArea', 'hourSquare', 'time', 'trappingType', 'actionColumn'];

  constructor(
    private areasService: AreasService,
    private lookupsService: LookupsService) { }

  async ngOnInit(): Promise<void> {
    this.trappingTypes = await this.lookupsService.getTrappingTypes().toPromise();
  }

  async changeCatchAreas(newValue: string) {
    const filterByOrganization = newValue === undefined;
    this.catchAreas = await this.areasService.getCatchAreas(newValue, filterByOrganization).toPromise();
  }

  calculateTotalTimeForRow(formItems: any[]) {
    return TotalTimeHelper.toDisplayString(TotalTimeHelper.calculateTotalTimeForRow(formItems));
  }

  rowContainsTime(formItems: any[]) {
    return TotalTimeHelper.itemsContainTime(formItems);
  }
}
