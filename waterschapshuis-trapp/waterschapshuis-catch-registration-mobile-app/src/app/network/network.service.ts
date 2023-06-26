import { Injectable } from '@angular/core';
import { Network } from '@ionic-native/network/ngx';
import { Platform, ToastController } from '@ionic/angular';
import { BehaviorSubject, interval, Observable } from 'rxjs';
import { takeWhile } from 'rxjs/operators';
import { ConnectionStatus } from '../shared/offline/models/connection-status.enum';
import { NetworkServiceResult } from '../shared/offline/models/network-service-result';
import { Logger } from '../core/logger/logger';

const logger = new Logger('NetworkService');

@Injectable({
  providedIn: 'root'
})
export class NetworkService {
  private status: BehaviorSubject<NetworkServiceResult> =
    new BehaviorSubject({ status: ConnectionStatus.Offline, simulateOffline: false });

  constructor(private network: Network, private toastController: ToastController, private platform: Platform) {
    this.platform.ready().then(() => {
      this.initializeNetworkEvents();
      const status = this.getConnectionStatus();
      const simulateOffline = this.status.value.simulateOffline;
      this.status.next({ status, simulateOffline });
    });
  }

  private getConnectionStatus(): ConnectionStatus {
    // logger.debug('network type: ' + this.network.type);
    return this.network.type !== 'none' ? ConnectionStatus.Online : ConnectionStatus.Offline;
  }

  public initializeNetworkEvents() {
    this.network.onDisconnect().pipe(takeWhile(() => !this.status.value.simulateOffline)).subscribe(event => {
      const value = this.status.getValue();
      if (value.status === ConnectionStatus.Online) {
        logger.debug('WE ARE OFFLINE');
        this.updateNetworkStatus(ConnectionStatus.Offline, value.simulateOffline);
      }
    });

    this.network.onConnect().pipe(takeWhile(() => !this.status.value.simulateOffline)).subscribe(event => {
      const value = this.status.getValue();
      if (value.status === ConnectionStatus.Offline) {
        logger.debug('WE ARE ONLINE');
        this.updateNetworkStatus(ConnectionStatus.Online, value.simulateOffline);
      }
    });

    const source = interval(3000);
    source.subscribe(val => this.checkConnection());
  }

  private checkConnection() {
    // logger.debug('network type: ' + this.network.type);
  }

  simulateOfflineMode(simulateOffline) {
    const status = simulateOffline ? ConnectionStatus.Offline : this.getConnectionStatus();
    this.updateNetworkStatus(status, simulateOffline, false);
  }

  forceOnlineMode(showToast = true) {
    if (this.getConnectionStatus() === ConnectionStatus.Offline) {
      const toastOptions = this.toastController.create({
        message: 'Er is geen netwerkverbinding',
        duration: 3000,
        position: 'bottom'
      });
      toastOptions.then(toast => toast.present());
      // you cannot force go to online if you are really offline
      // api calls will break
    } else {
      this.updateNetworkStatus(ConnectionStatus.Online, false, showToast);
    }
  }

  forceOfflineMode(showToast = true) {
    this.updateNetworkStatus(ConnectionStatus.Offline, true, showToast);
  }

  private async updateNetworkStatus(status: ConnectionStatus, simulateOffline: boolean, showToast = true) {
    this.status.next({ status, simulateOffline });

    const connection = status === ConnectionStatus.Offline ? 'Offline' : 'Online';
    if (!showToast) { return; }

    const toastOptions = this.toastController.create({
      message: `Je bent nu ${connection}`,
      duration: 3000,
      position: 'bottom'
    });
    toastOptions.then(toast => toast.present());
  }

  public onNetworkChange(): Observable<NetworkServiceResult> {
    return this.status.asObservable();
  }

  public getCurrentNetworkServiceStatus(): NetworkServiceResult {
    return this.status.getValue();
  }

  public isOffline() {
    return this.getCurrentNetworkServiceStatus().status === ConnectionStatus.Offline;
  }
}
