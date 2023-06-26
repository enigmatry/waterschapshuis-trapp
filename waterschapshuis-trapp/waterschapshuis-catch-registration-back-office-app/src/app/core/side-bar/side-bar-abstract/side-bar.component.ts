import { Component, ComponentFactoryResolver, Host, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { filter } from 'rxjs/operators';

import { Router } from '@angular/router';
import { SideBarService } from '../side-bar.service';
import { ISideBarConfig } from './side-bar-config.interface';
import { SideBarInner } from './side-bar-inner.class';
import { SideBarDirective } from './side-bar.directive';
import { ISideBar } from './side-bar.interface';
import { HasBackConfirmDialog } from 'src/app/shared/alert/alert/has-back-confirm-dialog';
import { MatDialog } from '@angular/material/dialog';
import { HelpPageComponent } from 'src/app/common/help-page/help-page.component';
import { OnDestroyMixin, untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';

@Component({
  selector: 'app-sidebar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent extends OnDestroyMixin implements OnInit, OnDestroy {
  private sideBar: SideBarInner;
  initialized = false;
  expanded = true;
  headerVisible = false;
  showExpansionButton = false;
  showBackButton = false;
  title = '';
  backConfirmComponent: HasBackConfirmDialog;

  @ViewChild(SideBarDirective, { static: true }) sideBarHost: SideBarDirective;


  constructor(
    @Host() private parent: MatSidenav,
    private componentFactoryResolver: ComponentFactoryResolver,
    private sideBarService: SideBarService,
    private router: Router,
    public dialog: MatDialog
  ) {
    super();
  }

  ngOnInit(): void {
    this.sideBarService.sideBarInnerType
      .pipe(
        filter((config: ISideBarConfig) => this.parent.position === config.position),
        untilComponentDestroyed(this)
      ).subscribe((conf: ISideBarConfig) => this.updateComponentType(conf));

    this.sideBarService.toggleSideBarSwitch
      .pipe(
        filter((config: ISideBarConfig) => this.parent.position === config.position),
        untilComponentDestroyed(this)
      ).subscribe((config: ISideBarConfig) => {
        const data = Array.isArray(config.data) ? [...config.data] : { ...config.data };
        this.sideBar.setData(data);
        this.parent.mode = config.mode;
        this.sideBar.instance[config.action]();
        this.parent[config.action]();
      });
  }

  ngOnDestroy(): void { }

  onOpen(): void { }

  navigateToHome(): void {
    if (this.backConfirmComponent) {
      this.backConfirmComponent.showDialog().subscribe(result => {
        if (result) {
          this.router.navigateByUrl('/home');
        }
      });
    } else {
      this.router.navigateByUrl('/home');
    }
  }

  expandPanel(expanded: boolean): void {
    this.expanded = !expanded;
  }

  private updateComponentType(config: ISideBarConfig): void {
    this.clearPreviousComponentInstance();
    if (config.type === null) {
      return;
    }

    this.sideBar = new SideBarInner(config.type, config.data);
    this.createNewComponentInstance(config);
  }

  private clearPreviousComponentInstance(): void {
    const viewContainerRef = this.sideBarHost.viewContainerRef;
    viewContainerRef.clear();
    this.sideBar = null;
    this.initialized = false;
  }

  private createNewComponentInstance(config: ISideBarConfig): void {
    const viewContainerRef = this.sideBarHost.viewContainerRef;
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(config.type);

    const componentRef = viewContainerRef.createComponent(componentFactory);
    const instance = componentRef.instance as ISideBar;

    this.setHeaderConfiguration(config);
    this.sideBar.setInstance(instance);
    this.initialized = true;
  }

  setHeaderConfiguration(config: ISideBarConfig): any {
    if ((config.header)) {
      this.headerVisible = true;
      this.title = config.header.title;
      this.showBackButton = config.header.showBackButton;
      this.showExpansionButton = config.header.showExpansionButton;
      this.expanded = config.header.expanded;
      this.backConfirmComponent = config.header.backConfirmComponent;
    }
  }

  onInfoIconClick(title: string) {
    const dialogRef = this.dialog.open(HelpPageComponent, {
      width: '50%',
      height: '80%',
      data: { pageName: title }
    });
  }
}
