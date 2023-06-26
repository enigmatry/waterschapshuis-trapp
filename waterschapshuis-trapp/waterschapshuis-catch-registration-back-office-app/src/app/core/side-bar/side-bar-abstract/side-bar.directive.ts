import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appSidebarHost]'
})
export class SideBarDirective {
  constructor(public viewContainerRef: ViewContainerRef) { }
}
