import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { fromEvent, merge, of } from 'rxjs';
import { catchError } from 'rxjs/internal/operators/catchError';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { PagedResponseOfGetTimeRegistrationCategoryResponse, TimeRegistrationCategoryCreateOrUpdateCommand } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { QueryModel } from 'src/app/shared/models/query-model';
import { TimeRegistrationCategory } from '../../models/time-registration-category';
import { TimeRegistrationCategoriesService } from '../../services/time-registration-categories.service';
import { EditTimeRegistrationCategoryComponent } from '../edit-time-registration-category/edit-time-registration-category.component';
import { SettingsBaseComponent } from '../settings-base.component';

@Component({
  selector: 'app-time-registration-categories',
  templateUrl: './time-registration-categories.component.html',
  styleUrls: ['./time-registration-categories.component.scss']
})
export class TimeRegistrationCategoriesComponent
  extends SettingsBaseComponent
  implements OnInit, AfterViewInit, OnDestroy {
  columns: string[] = ['name', 'active', 'action'];
  pageSize = 10;
  pageSizeOptions = [10, 20, 50];
  policyName = PolicyName;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('searchBox', { static: true }) searchBox: ElementRef;

  dataSource: MatTableDataSource<TimeRegistrationCategory> = new MatTableDataSource();
  itemsCount = 0;

  constructor(
    private timeRegistrationCategoriesService: TimeRegistrationCategoriesService,
    sideBarService: SideBarService,
    private dialog: MatDialog) {
    super(sideBarService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.loadData();
  }

  ngAfterViewInit(): void {
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
    this.timeRegistrationCategoriesService.getAllTimeRegistrationCategories(query).pipe(
      catchError(() => of([])),
      untilComponentDestroyed(this)
    ).subscribe((response: PagedResponseOfGetTimeRegistrationCategoryResponse) => {
      this.dataSource.data = response.items.map(item => {
        return TimeRegistrationCategory.fromResponse(item);
      });
      this.itemsCount = response.itemsTotalCount;
    });
  }

  editTimeRegistrationCategoryDialog(obj: TimeRegistrationCategory | null) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      selectedTimeRegistrationCategory: obj
    };

    const dialogRef = this.dialog.open(EditTimeRegistrationCategoryComponent, dialogConfig);

    dialogRef.afterClosed()
      .subscribe(data => data ?
        this.saveTimeRegistrationCategory(
          TimeRegistrationCategoryCreateOrUpdateCommand.fromJS(data.selectedTimeRegistrationCategory)) : {});
  }

  saveTimeRegistrationCategory(cmd: TimeRegistrationCategoryCreateOrUpdateCommand): void {
    this.timeRegistrationCategoriesService.saveTimeRegistrationCategory(cmd).subscribe(() => {
      this.loadData();
    });
  }
}
