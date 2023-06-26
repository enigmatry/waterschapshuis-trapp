import { Component, OnInit } from '@angular/core';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { AuthService } from 'src/app/core/auth/auth.service';
import { CurrentUserService } from 'src/app/shared/current-user.service';

@Component({
  selector: 'app-home-screen-dashboard',
  templateUrl: './home-screen-dashboard.component.html',
  styleUrls: ['./home-screen-dashboard.component.scss']
})
export class HomeScreenDashboardComponent implements OnInit {
  policyName = PolicyName;
  MapRoutePolicies = [ PolicyName.MapRead ];
  SettingsRoutePolicies = [ PolicyName.UserRead, PolicyName.RoleRead ];
  ReportRoutePolicies =  [ PolicyName.ReportReadWrite ];


  constructor(
    private authService: AuthService,
    private currentUserService: CurrentUserService
  ) { }

  ngOnInit(): void {
    this.initView();
  }

  async initView(): Promise<void> {
  }

  currentUserName = (): string => this.currentUserService.currentUser?.name;

  logout = () => this.authService.logout().subscribe();
}
