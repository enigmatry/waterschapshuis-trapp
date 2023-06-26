import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { fromEvent, merge, of } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, finalize, tap } from 'rxjs/operators';
import {
  IGetUsersResponseItem,
  IPagedResponseOfGetUsersResponseItem,
  UserUpdateCommand
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { QueryModel } from 'src/app/shared/models/query-model';
import { mapToCommand } from '../../models/user-create-or-update-command.extensions';
import { UsersService } from '../../services/users.service';
import { OrganizationsChecklistComponent } from '../organizations-checklist/organizations-checklist.component';
import { SettingsBaseComponent } from '../settings-base.component';
import { DeactivateUserDialogComponent } from './deactivate-user-dialog/deactivate-user-dialog.component';

@Component({
  selector: 'app-users-overview',
  templateUrl: './users-overview.component.html',
  styleUrls: ['./users-overview.component.scss']
})
export class UsersOverviewComponent
  extends SettingsBaseComponent
  implements OnInit, AfterViewInit, OnDestroy {
  columns: string[] = ['email', 'name', 'surname', 'givenName', 'inactiveOn', 'authorized', 'organizationName'];
  pageSize = 10;
  pageSizeOptions = [10, 20, 50];
  policyName = PolicyName;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('searchBox', { static: true }) searchBox: ElementRef;

  dataSource: MatTableDataSource<IGetUsersResponseItem> = new MatTableDataSource();
  itemsCount = 0;

  constructor(
    private usersService: UsersService,
    sideBarService: SideBarService,
    private dialog: MatDialog,
    private changeDetector: ChangeDetectorRef
  ) {
    super(sideBarService);
  }

  ngOnInit() {
    super.ngOnInit();
    this.loadData();
  }

  ngAfterViewInit() {
    this.sort.sortChange
      .pipe(
        untilComponentDestroyed(this)
      ).subscribe(() => (this.paginator.pageIndex = 0));

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

    this.usersService.getUsers(query).pipe(
      catchError(() => of([])),
      untilComponentDestroyed(this)
    ).subscribe((response: IPagedResponseOfGetUsersResponseItem) => {
      this.dataSource.data = response.items;
      this.itemsCount = response.itemsTotalCount;

      // TODO WVR-1662: fixes problem with ngx permission only attribute and the expression ExpressionChangedAfterItHasBeenCheckedError
      this.changeDetector.detectChanges();
    });
  }

  editUserOrganizationDialog(obj: IGetUsersResponseItem) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      selectedOrganization: obj.organizationId
    };

    const dialogRef = this.dialog.open(OrganizationsChecklistComponent, dialogConfig);

    dialogRef.afterClosed()
      .subscribe(data => data ?
        this.saveUser(this.updateUserOrganizationCommand(obj, data.organizationId)) : {});
  }

  saveUser(cmd: UserUpdateCommand): void {
    this.usersService.updateUser(cmd).subscribe(() => {
      this.loadData();
    });
  }

  changeUserAuthorizedFlag(obj: IGetUsersResponseItem, value: boolean): void {
    const cmd = this.updateUserAuthorizedFlagCommand(obj, value);
    if (value) {
      this.saveUser(cmd);
    } else {
      const dialogConfig = new MatDialogConfig();
      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;

      const dialogRef = this.dialog.open(DeactivateUserDialogComponent, dialogConfig);

      dialogRef.afterClosed()
        .pipe(
          untilComponentDestroyed(this)
        ).subscribe(data =>
          data ? this.saveUser(cmd) : {});
    }
  }

  private updateUserAuthorizedFlagCommand(obj: IGetUsersResponseItem, value: boolean): UserUpdateCommand {
    const command = mapToCommand(obj);
    command.authorized = value;
    return command;
  }

  private updateUserOrganizationCommand(obj: IGetUsersResponseItem, organization: string): UserUpdateCommand {
    const command = mapToCommand(obj);
    command.organizationId = organization;
    return command;
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }
}
