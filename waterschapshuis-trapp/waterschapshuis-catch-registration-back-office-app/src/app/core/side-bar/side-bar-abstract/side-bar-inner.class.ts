import { Type } from '@angular/core';
import { ISideBar } from './side-bar.interface';

export class SideBarInner {

  instance: ISideBar;

  constructor(public component: Type<ISideBar>, public data: any) { }

  setData(data: any): void {
    this.instance.data = data;
  }

  setInstance(instance: ISideBar): void {
    this.instance = instance;
    this.setData(this.data);
  }

  clear(): void {
    this.data = null;
    this.instance = null;
  }
}
