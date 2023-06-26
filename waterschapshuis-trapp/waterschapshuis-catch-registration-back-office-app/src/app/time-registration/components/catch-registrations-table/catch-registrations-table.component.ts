import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import {
  PagedResponseOfGetCatchTypeResponse
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { CatchTypesService } from 'src/app/settings/services/catch-type.service';
import { IListItem, ListItem } from 'src/app/shared/models/list-item';
import { QueryModel } from 'src/app/shared/models/query-model';
import { CatchRegistration } from '../../models/catch-registration';

@Component({
  selector: 'app-catch-registrations-table',
  templateUrl: './catch-registrations-table.component.html',
  styleUrls: ['./catch-registrations-table.component.scss']
})
export class CatchRegistrationsTableComponent implements OnInit {
  @Input() daysOfWeek: Date[] = [];
  @Input() catchesData: CatchRegistration[];

  catchesOnly: IListItem[] = [];
  byCatchesOnly: IListItem[] = [];

  constructor(
    private catchTypeService: CatchTypesService,
    private datePipe: DatePipe) { }

  ngOnInit() {
    this.loadCatchTypes();
  }

  loadCatchTypes(): void {
    const query = new QueryModel();
    query.pageSize = 1000;
    this.catchTypeService.getAllCatchTypes(query)
      .pipe()
      .subscribe((response: PagedResponseOfGetCatchTypeResponse) => {
        this.catchesOnly = ListItem.mapToListItems(response.items.filter(x => !x.isByCatch));
        this.byCatchesOnly = ListItem.mapToListItems(response.items.filter(x => x.isByCatch));
      });
  }

  getCatchTypeItemDisplayName(item: CatchRegistration): any {
    return item.isByCatch ? this.byCatchesOnly.find(i => i.id === item.catchTypeId)?.name :
      this.catchesOnly.find(i => i.id === item.catchTypeId)?.name;
  }

  getCatchesForDate(date: Date): any {
    return this.catchesData?.filter(x =>
      this.datePipe.transform(x?.recordedOn, 'yyyy-MM-dd') === this.datePipe.transform(date, 'yyyy-MM-dd')
    );
  }
}
