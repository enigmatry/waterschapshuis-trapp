import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, OnDestroy } from '@angular/core';
import { catchError, finalize, debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { of, fromEvent, merge } from 'rxjs';
import { QueryModel } from 'src/app/shared/models/query-model';
import { CatchType } from '../../models/catch-type.model';
import {
  CatchTypeCreateOrUpdateCommand,
  PagedResponseOfGetCatchTypeResponse
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SettingsBaseComponent } from '../settings-base.component';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EditCatchTypeComponent } from '../edit-catch-type/edit-catch-type.component';
import { CatchTypesService } from '../../services/catch-type.service';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-catch-types',
  templateUrl: './catch-types.component.html',
  styleUrls: ['./catch-types.component.scss']
})
export class CatchTypesComponent
  extends SettingsBaseComponent
  implements OnInit, AfterViewInit, OnDestroy {
  columns: string[] = ['name', 'isByCatch', 'animalType', 'order', 'action'];
  pageSize = 10;
  pageSizeOptions = [10, 20, 50];
  policyName = PolicyName;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('searchBox', { static: true }) searchBox: ElementRef;

  dataSource: MatTableDataSource<CatchType> = new MatTableDataSource();
  itemsCount = 0;

  constructor(
    private catchTypeService: CatchTypesService,
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
    this.catchTypeService.getAllCatchTypes(query).pipe(
      catchError(() => of([])),
      untilComponentDestroyed(this)
    ).subscribe((response: PagedResponseOfGetCatchTypeResponse) => {
      this.dataSource.data = response.items.map(item => {
        return CatchType.fromResponse(item);
      });
      this.itemsCount = response.itemsTotalCount;
    });
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  editOrAddCatchTypeDialog(obj: CatchType | null) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      selectedCatchType: obj
    };

    const dialogRef = this.dialog.open(EditCatchTypeComponent, dialogConfig);

    dialogRef.afterClosed()
      .subscribe(data => data ?
        this.saveCatchType(CatchTypeCreateOrUpdateCommand.fromJS(data.selectedCatchType)) : {});
  }

  saveCatchType(cmd: CatchTypeCreateOrUpdateCommand): void {
    this.catchTypeService.saveCatchType(cmd).subscribe(() => {
      this.loadData();
    });
  }
}
