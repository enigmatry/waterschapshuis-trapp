import { Component, OnInit } from '@angular/core';
import { NavController, Platform } from '@ionic/angular';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { filter, tap } from 'rxjs/operators';
import { LoaderService } from 'src/app/services/loader.service';
import { ConnectionStatus } from 'src/app/shared/offline/models/connection-status.enum';
import { AuthService } from '../auth/auth.service';
import { NetworkService } from './../../network/network.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent extends OnDestroyMixin implements OnInit {

  loader: any;
  errorText: string;

  constructor(
    private platform: Platform,
    private authService: AuthService,
    private nav: NavController,
    private loaderService: LoaderService,
    private networkService: NetworkService,
  ) { super(); }

  ngOnInit(): void {
    this.platform.ready()
      .then(async () => {
        // handle situation when we are offline but were never logged in
        this.networkService.onNetworkChange().pipe(
          tap(result => {
            this.errorText = result.status === ConnectionStatus.Offline
              ? 'No internet connection detected' : '';
          }),
          filter(result => result.status === ConnectionStatus.Online),
          untilComponentDestroyed(this))
          .subscribe(async () => {
            await this.authenticate();
          });
      });
  }

  private async authenticate(): Promise<void> {
    this.loader = await this.loaderService.createLoader();
    this.loader.present();
    try {
      const success = await this.authService.authenticateAndLoadUser();
      if (success) {
        await this.nav.navigateRoot('/home');
      } else {
        this.errorText = 'Unable to authenticate';
      }
    } finally {
      this.loader.dismiss();
    }
  }
}
