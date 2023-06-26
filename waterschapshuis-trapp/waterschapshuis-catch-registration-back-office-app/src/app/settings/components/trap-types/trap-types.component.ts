import { Component, OnInit, AfterViewInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { SettingsBaseComponent } from '../settings-base.component';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { TrapType } from '../../models/trap-type.model';
import { MatTableDataSource } from '@angular/material/table';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { fromEvent, merge, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap, catchError, finalize } from 'rxjs/operators';
import { QueryModel } from 'src/app/shared/models/query-model';
import { TrapTypeCreateOrUpdateCommand, PagedResponseOfGetTrapTypeResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { EditTrapTypeComponent } from '../edit-trap-type/edit-trap-type.component';
import { TrapTypesService } from '../../services/trap-types.service';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-trap-types',
  templateUrl: './trap-types.component.html',
  styleUrls: ['./trap-types.component.scss']
})
export class TrapTypesComponent
  extends SettingsBaseComponent
  implements OnInit, AfterViewInit, OnDestroy {
  columns: string[] = ['name', 'trappingType', 'active', 'order', 'action'];
  pageSize = 10;
  pageSizeOptions = [10, 20, 50];
  policyName = PolicyName;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('searchBox', { static: true }) searchBox: ElementRef;

  dataSource: MatTableDataSource<TrapType> = new MatTableDataSource();
  itemsCount = 0;

  constructor(
    private trapTypeService: TrapTypesService,
    sideBarService: SideBarService,
    private dialog: MatDialog
  ) {
    super(sideBarService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.loadData();
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    fromEvent(this.searchBox.nativeElement, 'keyup')
      .pipe(
        debounceTime(150),
        distinctUntilChanged(),
        untilComponentDestroyed(this)
      )
      .subscribe(() => {
        this.paginator.pageIndex = 0;
        this.loadData();
      });

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.loadData()),
        untilComponentDestroyed(this)
      ).subscribe();
  }

  loadData() {
    const query = new QueryModel();
    query.keyword = this.searchBox.nativeElement.value;
    query.sortField = this.sort.active;
    query.sortDirection = this.sort.direction;
    query.pageSize = this.paginator.pageSize || this.pageSize;
    query.currentPage = this.paginator.pageIndex + 1;
    this.trapTypeService.getAllTrapTypes(query).pipe(
      catchError(() => of([])),
      untilComponentDestroyed(this)
    ).subscribe((response: PagedResponseOfGetTrapTypeResponse) => {
      this.dataSource.data = response.items.map(item => {
        return TrapType.fromResponse(item);
      });
      this.itemsCount = response.itemsTotalCount;
    });
  }

  editTrapTypeDialog(obj: TrapType | null) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      selectedTrapType: obj
    };

    const dialogRef = this.dialog.open(EditTrapTypeComponent, dialogConfig);

    dialogRef.afterClosed()
      .subscribe(data => data ?
        this.saveTrapType(TrapTypeCreateOrUpdateCommand.fromJS(data.selectedTrapType)) : {});
  }

  saveTrapType(cmd: TrapTypeCreateOrUpdateCommand): void {
    this.trapTypeService.saveTrapType(cmd).subscribe(() => {
      this.loadData();
    });
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

}
