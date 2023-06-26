import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MapFilter } from 'src/app/shared/models/map-filter.model';
import { IListItem, ListItem } from 'src/app/shared/models/list-item';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { MapStateService } from '../../services/map-state.service';
import { dateAsYearMonthDay } from 'src/app/shared/models/utils';

@Component({
  selector: 'app-map-filter',
  templateUrl: './map-filter.component.html',
  styleUrls: ['./map-filter.component.scss']
})
export class MapFilterComponent implements OnInit, OnDestroy {
  catchTypes = [
    { id: 0, name: 'Beverrat' },
    { id: 1, name: 'Muskusrat' },
    { id: 2, name: 'Alle' }];
  trapTypes: IListItem[] = [];
  form: FormGroup;
  end = new Date();
  start = new Date(this.end.getFullYear() - 1, this.end.getMonth(), this.end.getDate());

  constructor(private fb: FormBuilder,
              private lookupsService: LookupsService,
              private mapStateService: MapStateService) {
    this.createForm();
   }

  ngOnInit() {
    this.loadTrapTypes();
  }

  private createForm(): void {
    this.form = this.fb.group({
      trapStartDate: [null],
      trapEndDate: [null],
      trapTypeId: [null],
      catchStartDate: [this.start],
      catchEndDate: [this.end],
      catchType: [null],
      showTrapsWithCatches: [false]
    });
  }

  applyFilter(): void {
    const filter = this.toMapFilter();
    this.mapStateService.updateFilter(filter);
  }

  loadTrapTypes() {
    this.lookupsService.getTrapTypes()
      .subscribe(response => {
        this.trapTypes = ListItem.mapToListItems(response);
      });
  }

  private toMapFilter(): MapFilter {
    return {
      trapStartDate: this.form.value.trapStartDate ? dateAsYearMonthDay(this.form.value.trapStartDate) : null,
      trapEndDate: this.form.value.trapEndDate ? dateAsYearMonthDay(this.form.value.trapEndDate) : null,
      trapTypeId: this.form.value.trapTypeId,
      catchStartDate: this.form.value.catchStartDate ? dateAsYearMonthDay(this.form.value.catchStartDate) : null,
      catchEndDate: this.form.value.catchEndDate ? dateAsYearMonthDay(this.form.value.catchEndDate) : null,
      catchType: this.form.value.catchType,
      showTrapsWithCatches: this.form.value.showTrapsWithCatches ? 1 : 0
    };
  }

  ngOnDestroy(): void {
    this.mapStateService.updateFilter(new MapFilter());
  }

}
