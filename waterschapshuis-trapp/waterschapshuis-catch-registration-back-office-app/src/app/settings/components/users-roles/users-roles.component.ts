import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import {
  IPagedResponseOfGetUsersResponseItem,
  UpdateUserRolesCommand
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { fromEvent, merge, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap, catchError, finalize } from 'rxjs/operators';
import { UsersService } from '../../services/users.service';
import { SettingsBaseComponent } from '../settings-base.component';
import { UserRoles } from '../../models/user-roles.model';
import { Role } from '../../models/role.model';
import { NgxPermissionsService } from 'ngx-permissions';
import { CurrentUserService } from 'src/app/shared/current-user.service';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { Router } from '@angular/router';
import { QueryModel } from 'src/app/shared/models/query-model';
import { RolesChecklistComponent } from '../roles-checklist/roles-checklist.component';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-roles',
  templateUrl: './users-roles.component.html',
  styleUrls: ['./users-roles.component.scss']
})
export class UsersRolesComponent
  extends SettingsBaseComponent
  implements OnInit, OnDestroy, AfterViewInit {
  columns: string[] = ['email', 'name', 'rolesDisplayText', 'wijzig'];
  pageSize = 10;
  pageSizeOptions = [10, 20, 50];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('searchBox', { static: true }) searchBox: ElementRef;

  dataSource: MatTableDataSource<UserRoles> = new MatTableDataSource();
  itemsCount = 0;


  constructor(
    sideBarService: SideBarService,
    private usersService: UsersService,
    private dialog: MatDialog,
    private currentUserService: CurrentUserService,
    private permissionsService: NgxPermissionsService,
    private route: Router) {
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

    this.usersService.getUsers(query).pipe(
      catchError(() => of([])),
      untilComponentDestroyed(this)
    ).subscribe((response: IPagedResponseOfGetUsersResponseItem) => {
      this.dataSource.data = response.items.map(res => {
        return UserRoles.fromResponse(res);
      });
      this.itemsCount = response.itemsTotalCount;
    });
  }

  editRolesDialog(obj: UserRoles) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      roles: obj.roles
    };

    const dialogRef = this.dialog.open(RolesChecklistComponent, dialogConfig);

    dialogRef.afterClosed()
      .subscribe(data => data ?
        this.saveUserRoles(this.createUpdateUserRolesCommand(data.roles, obj.id)) : {});
  }

  saveUserRoles(cmd: UpdateUserRolesCommand): void {
    this.usersService
      .updateUserRoles(cmd)
      .subscribe(x => this.tryLoadDataAfterUserUpdate(cmd.id));
  }

  private tryLoadDataAfterUserUpdate = (userId: string) => {
    if (userId !== this.currentUserService.currentUser.id) {
      this.loadData();
      return;
    }
    this.currentUserService
      .createUserFromIdentity()
      .then(() => {
        this.permissionsService
          .hasPermission(PolicyName.UserRead)
          .then(hasPermission => {
            if (hasPermission) { this.loadData(); } else { this.route.navigate(['home']); }
          });
      });
  }

  createUpdateUserRolesCommand(roles: Role[], id: string): UpdateUserRolesCommand {
    // tslint:disable-next-line: no-shadowed-variable
    const selectedIds = roles.filter(r => r.checked).map(({ id }) => id);
    return UpdateUserRolesCommand.fromJS({ id, roles: selectedIds });
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }
}
