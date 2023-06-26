import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Diagnostic } from '@ionic-native/diagnostic/ngx';
import { Insomnia } from '@ionic-native/insomnia/ngx';
import { NavController } from '@ionic/angular';
import { OnDestroyMixin } from '@w11k/ngx-componentdestroyed';
import { forkJoin } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BackgroundJobService } from '../background-job/background-job.service';
import { AuthService } from '../core/auth/auth.service';
import { Logger } from '../core/logger/logger';
import { LocalGeoJsonService } from '../geo-json/services/local-geo-json.service';
import { OfflineMapService } from '../maps/services/offline-map.service';
import { NetworkService } from '../network/network.service';
import { SqliteProviderService } from '../services/sqlite-provider.service';
import { ToastService } from '../services/toast.service';
import { SqliteTableConfig } from '../shared/models/sqlite-table-config';
import { ISqliteTableData } from '../shared/models/sqlite-table-data';
import { AlertService } from '../shared/services/alert.service';
import { CurrentUserProviderService } from '../shared/services/current-user-provider.service';
import { GeolocationService } from '../shared/services/geolocation.service';
import { ProgressService } from '../shared/services/progress.service';
import { PreFetchSyncService } from '../sync/prefetch-sync.service';
import { TrapService } from '../traps/services/trap.service';
import { AndroidSettings, NativeSettings } from 'capacitor-native-settings';
import { PlatformService } from '../services/platform.service';
import { Plugin, PermissionState } from '@capacitor/core';

const logger = new Logger('HomeComponent');

@Component({
  selector: 'app-main-menu',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [Diagnostic]
})
export class HomeComponent extends OnDestroyMixin implements OnInit {
  component: any = this;

  catchCount = 0;
  trapCount = 0;

  isProduction = environment.production;
  simulateOffline = false;

  version = environment.appVersion;

  isOnline: boolean;

  currentUserName: string;

  /* START USED ONLY FOR DEVELOPER OPTIONS */

  tableList: Array<ISqliteTableData> = [
    SqliteTableConfig.tracking,
    SqliteTableConfig.observations,
    SqliteTableConfig.appSettings,
    SqliteTableConfig.offlineMapDetails,
    SqliteTableConfig.geoJsonCacheMetadata,
    SqliteTableConfig.geoJsonUpdateCommand,
    SqliteTableConfig.backgroundJobQueuedRequests
  ];

  showSqlLiteTableList = false;

  /* END USED ONLY FOR DEVELOPER OPTIONS */

  constructor(
    public offlineService: OfflineMapService,
    public networkService: NetworkService,
    public progressService: ProgressService,
    private preFetchSyncService: PreFetchSyncService,
    private localGeoJsonService: LocalGeoJsonService,
    private geolocationService: GeolocationService,
    private backgroundJobService: BackgroundJobService,
    private trapService: TrapService,
    private sqliteProvider: SqliteProviderService,
    private router: Router,
    private toastService: ToastService,
    private diagnostic: Diagnostic,
    private authService: AuthService,
    private insomnia: Insomnia,
    private currentUserService: CurrentUserProviderService,
    private nav: NavController,
    private alertService: AlertService,
    private platformService: PlatformService
  ) { super(); }

  ngOnInit(): void {
    if (this.platformService.isIos()) {
      this.geolocationService.showLocationConsentPrompt();
    } else {
      this.diagnostic.getLocationAuthorizationStatus().then(result => {
        if (result !== 'GRANTED') {
          this.showBackgroundLocationPermissionDialog();
        }
      });
    }

    this.networkService.onNetworkChange().subscribe(result =>
      this.simulateOffline = result.simulateOffline
    );
    this.currentUserName = this.currentUserService.currentUser.name;

    this.isOnline = !this.networkService.isOffline();

    this.diagnostic.isLocationEnabled().then(isEnabled => {
      if (!isEnabled) {
        this.toastService.warning('Ophalen van locatie lukt niet (activeer locatietoegang in instellingen)', null, [{
          role: 'cancel',
          icon: 'close'
        }]);
      }
    });
  }

  ionViewWillEnter() {
    if (!this.networkService.isOffline()) {
      this.trapService.getCurrentUserSummary(false)
        .subscribe(result => {
          this.catchCount = result.catchesThisWeek;
          this.trapCount = result.outstandingTraps;
        });
    }
  }

  async goToMaps(): Promise<void> {
    const offlineDataAvailable = await this.offlineService.offlineMapAvailabilityStatus().pipe(take(1)).toPromise();
    if (this.networkService.isOffline() && !offlineDataAvailable.available) {
      this.toastService.error('U heeft geen toegang tot Internet');
    } else {
      this.router.navigateByUrl('/maps');
    }
  }

  logout = async (): Promise<void> => {
    await this.authService.logout();
    await this.nav.navigateRoot('');
  }

  // BELOW: Development functionalities to be removed in Production version!!!

  syncRecordedItems() {
    this.progressService.showIndeterminateBar();
    this.backgroundJobService.deQueue().subscribe(() => {
      this.progressService.hideIndeterminateBar();
    },
      err => {
        logger.error(err);
        this.progressService.hideIndeterminateBar();
      });
  }

  simulateOfflineMode(evt: any) {
    const newStatus = evt.detail.checked;
    this.networkService.simulateOfflineMode(newStatus);
  }

  prefetchAll() {
    this.progressService.showIndeterminateBar();

    const prefetchData = this.preFetchSyncService.prefetchAllForCurrentLocation();
    const prefetchJson = this.localGeoJsonService.prefetchAll();

    this.insomnia.keepAwake().then(_ =>
      logger.debug('Device keep awake mode activated'));

    forkJoin([prefetchData, prefetchJson])
      .subscribe(() => {
        logger.debug('Offline data successfully downloaded.');
        this.insomnia.allowSleepAgain().then(_ =>
          logger.debug('Device keep awake mode deactivated'));
        this.progressService.hideIndeterminateBar();
      },
        err => {
          logger.error(err, 'Offline data download error');
          this.insomnia.allowSleepAgain().then(_ =>
            logger.debug('Device keep awake mode deactivated'));
          this.progressService.hideIndeterminateBar();
        });
  }

  toggleTableList(): void {
    this.showSqlLiteTableList = !this.showSqlLiteTableList;
  }

  async tableToConsole(table: ISqliteTableData): Promise<void> {
    await this.sqliteProvider.getAll(table, 'id')
      .then(
        results => {
          logger.debug(`Table ${table.name} rows ${results.length}`);
          results.forEach(result =>
            logger.debug(result)
          );
        },
        err =>
          logger.error(err)
      );
  }

  async countToConsole(table: ISqliteTableData): Promise<void> {
    await this.sqliteProvider.count(table)
      .then(
        count => {
          logger.debug(`Table ${table.name} rows ${count}`);
        },
        err =>
          logger.error(err)
      );
  }

  private showBackgroundLocationPermissionDialog(): void {

    this.alertService.getConfirmDialog(
      'Machtigingen bijwerken!',
      `Om deze app correct te gebruiken, moet u het gebruik van de achtergrondlocatie toestaan,
       zodat de toepassing u kan volgen, zelfs als deze niet is ingeschakeld.
       Ga naar (app toegang tot locatie) en zoek je app. Selecteer op het volgende scherm de eerste optie (Altijd toestaan).`,
      'Annuleer',
      'Ga naar Instellingen',
      (res) => {
        if (res) {
          NativeSettings.openAndroid({
            option: AndroidSettings.Location,
          });
        }
      },
      null,
      false,
      false
    ).then(x => x.present());
  }
}
