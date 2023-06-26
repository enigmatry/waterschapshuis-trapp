import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { SideBarComponent } from './side-bar/side-bar-abstract/side-bar.component';
import { SideBarDirective } from './side-bar/side-bar-abstract/side-bar.directive';
import { SideBarService } from './side-bar/side-bar.service';

@NgModule({
  declarations: [SideBarComponent, SideBarDirective],
  imports: [SharedModule, CommonModule],
  entryComponents: [SideBarComponent],
  exports: [SideBarComponent],
  providers: [
    SideBarService
  ]
})
export class CoreModule { }
