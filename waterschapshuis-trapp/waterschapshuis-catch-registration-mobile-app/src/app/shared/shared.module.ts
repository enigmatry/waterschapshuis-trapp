import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import {
  AreasClient,
  LookupsClient,
  BlobStorageClient,
  SyncsClient,
  MapsClient,
  AccountClient,
  ObservationsClient,
  TrapsClient,
  UsersClient,
  SettingsClient
} from '../api/waterschapshuis-catch-registration-mobile-api';
import { CacheModule } from '../cache/cache.module';
import { NetworkModule } from '../network/network.module';
import { HeaderComponent } from './components/header/header.component';
import { InfoIconComponent } from '../common/info-icon/info-icon.component';
import { HelpPageComponent } from '../common/help-page/help-page.component';
import { TrapDetailsComponent } from '../traps/trap-details/trap-details.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    RouterModule,
    NetworkModule,
    CacheModule
  ],
  declarations: [
    HeaderComponent,
    InfoIconComponent,
    HelpPageComponent,
    TrapDetailsComponent
  ],
  providers: [
    AreasClient,
    LookupsClient,
    BlobStorageClient,
    SyncsClient,
    MapsClient,
    AccountClient,
    ObservationsClient,
    TrapsClient,
    UsersClient,
    SettingsClient
  ],
  exports: [
    InfoIconComponent,
    TrapDetailsComponent
  ]
})
export class SharedModule { }
