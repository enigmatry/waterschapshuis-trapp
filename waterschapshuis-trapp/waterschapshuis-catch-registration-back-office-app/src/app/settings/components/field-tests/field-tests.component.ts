import { Component, OnInit, AfterViewInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { SettingsBaseComponent } from '../settings-base.component';
import { FieldTest } from '../../models/field-test.model';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { MatTableDataSource } from '@angular/material/table';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fromEvent, merge, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap, catchError, finalize } from 'rxjs/operators';
import { QueryModel } from 'src/app/shared/models/query-model';
import { FieldTestsService } from '../../services/field-tests.service';
import {
  PagedResponseOfGetFieldTestResponse,
  FieldTestCreateOrUpdateCommand
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { EditFieldTestComponent } from '../edit-field-test/edit-field-test.component';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-field-tests',
  templateUrl: './field-tests.component.html',
  styleUrls: ['./field-tests.component.scss']
})
export class FieldTestsComponent
  extends SettingsBaseComponent
  implements OnInit, AfterViewInit, OnDestroy {
  columns: string[] = ['name', 'startPeriod', 'endPeriod', 'action'];
  pageSize = 10;
  pageSizeOptions = [10, 20, 50];
  policyName = PolicyName;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('searchBox', { static: true }) searchBox: ElementRef;

  dataSource: MatTableDataSource<FieldTest> = new MatTableDataSource();
  itemsCount = 0;

  constructor(
    private fieldTestService: FieldTestsService,
    sideBarService: SideBarService,
    private dialog: MatDialog) {
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
    this.fieldTestService.getAllFieldTests(query).pipe(
      catchError(() => of([])),
      untilComponentDestroyed(this)
    ).subscribe((response: PagedResponseOfGetFieldTestResponse) => {
      this.dataSource.data = response.items.map(item => {
        return FieldTest.fromResponse(item);
      });
      this.itemsCount = response.itemsTotalCount;
    });
  }

  editFieldTestDialog(obj: FieldTest | null) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      selectedFieldTest: obj
    };

    const dialogRef = this.dialog.open(EditFieldTestComponent, dialogConfig);

    dialogRef.afterClosed()
      .subscribe(data => data ?
        this.saveFieldTest(FieldTestCreateOrUpdateCommand.fromJS(data.selectedFieldTest)) : {});
  }

  saveFieldTest(cmd: FieldTestCreateOrUpdateCommand): void {
    this.fieldTestService.saveFieldTest(cmd).subscribe(() => {
      this.loadData();
    });
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }


}
