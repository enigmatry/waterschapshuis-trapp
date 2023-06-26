import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Network } from '@ionic-native/network/ngx';
import { NetworkService } from './network.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [Network]
})
export class NetworkModule { }
