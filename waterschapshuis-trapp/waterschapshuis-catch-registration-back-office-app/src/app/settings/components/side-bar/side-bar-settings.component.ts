import { Component, OnDestroy, OnInit, Input } from '@angular/core';
import { SideBarPosition } from 'src/app/core/side-bar/side-bar-abstract/side-bar-positions.enum';
import { SideBarService } from 'src/app/core/side-bar/side-bar.service';
import { ISideBar } from '../../../core/side-bar/side-bar-abstract/side-bar.interface';
import { SideBarActions } from 'src/app/core/side-bar/side-bar-abstract/side-bar-actions.enum';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { Router } from '@angular/router';
import { NgxPermissionsService } from 'ngx-permissions';
import { CurrentUserService } from 'src/app/shared/current-user.service';

@Component({
  selector: 'app-side-bar-settings',
  templateUrl: './side-bar-settings.component.html'
})
export class SideBarSettingsComponent implements ISideBar, OnInit, OnDestroy {
  data: any;
  policyName = PolicyName;
  panelOpenState = true;

  sideBarItems = [
    {
      name: 'users',
      title: 'Gebruikers',
      visible: true,
      policies: [this.policyName.UserRead, this.policyName.RoleWrite],
      items: [
        {
          link: 'users-overview',
          title: 'Overzicht',
          policy: this.policyName.UserRead
        },
        {
          link: 'users-roles',
          title: 'Rollen',
          policy: this.policyName.UserRead
        },
        {
          link: 'users-roles-and-permissions',
          title: 'Rechten toewijzing',
          policy: this.policyName.RoleWrite
        }
      ]
    },
    {
      name: 'list',
      title: 'Lijsten',
      visible: this.isGroupVisible(),
      policies: [this.policyName.Management],
      items: [
        {
          link: 'catch-types-list',
          title: 'Vangsttype',
          policy: this.policyName.Management
        },
        {
          link: 'trap-types-list',
          title: 'Vangmiddeltype',
          policy: this.policyName.Management
        },
        {
          link: 'field-tests-list',
          title: 'Veldproef',
          policy: this.policyName.Management
        },
        {
          link: 'time-registration-categories-list',
          title: 'Urenregistratie categorieën',
          policy: this.policyName.Management
        }
      ]
    },
    {
      name: 'topology',
      title: 'Topologie',
      policies: [this.policyName.Management],
      items: [
        {
          link: 'topology-maintenance',
          title: 'Beheer deelgebieden',
          policy: this.policyName.Management
        }
      ]
    }
  ];

  constructor(private router: Router,
              private sideBarService: SideBarService,
              private currentUserService: CurrentUserService,
              private permissionsService: NgxPermissionsService
  ) { }
  open(): void { }

  close(): void { }

  toggle(): void { }

  ngOnInit(): void {

  }

  async isGroupVisible() {
    const hasPermission = await this.permissionsService.hasPermission(this.policyName.Management);
    return hasPermission;
  }

  ngOnDestroy(): void { }

  toggleSideBar(): void {
    this.sideBarService.toggleSideBar({ position: SideBarPosition.start, action: SideBarActions.toggle });
  }

  public createRouterLink = (link: string): string[] => ['/settings', link];

  isAccordionExpanded = (groupName: string): boolean =>
    this.router.url.includes(groupName)


}
