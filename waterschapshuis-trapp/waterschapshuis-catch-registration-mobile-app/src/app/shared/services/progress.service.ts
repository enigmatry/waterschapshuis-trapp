import { Injectable, NgZone } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { ProgressEmitter } from '../models/progressEmitter';
import { Progress } from '../models/progress';

@Injectable({
  providedIn: 'root'
})
export class ProgressService {
  alreadyEmittedValues: number[] = [];
  private progressSubject: BehaviorSubject<Progress> = new BehaviorSubject<Progress>(
    { progressEmitter: ProgressEmitter.None, progressPercentage: 0 });

  progress$ = this.progressSubject.asObservable();
  private showIndeterminateBarSubject: Subject<boolean> = new Subject<boolean>();
  showIndeterminateBar$ = this.showIndeterminateBarSubject.asObservable();
  private showDeterminateBarSubject: Subject<boolean> = new Subject<boolean>();
  showDeterminateBar$ = this.showDeterminateBarSubject.asObservable();

  constructor(private ngZone: NgZone) { }

  showIndeterminateBar() {
    this.showIndeterminateBarSubject.next(true);
  }

  hideIndeterminateBar() {
    this.showIndeterminateBarSubject.next(false);
  }

  showDeterminateBar() {
    this.showDeterminateBarSubject.next(true);
  }

  hideDeterminateBar() {
    this.showDeterminateBarSubject.next(false);
  }

  resetProgress() {
    this.alreadyEmittedValues = [];
    this.progressSubject.next({ progressEmitter: ProgressEmitter.None, progressPercentage: 0 });
  }

  emitProgressPercentage(progress: any, progressEmitter: ProgressEmitter) {
    const value = Math.round((progress.loaded / progress.total) * 10) / 10;
    if (!this.alreadyEmittedValues.includes(value)) {
      // cordova-plugin-zip works outside of ng zone
      this.ngZone.run(() => { this.progressSubject.next({ progressEmitter, progressPercentage: value }); });
      this.alreadyEmittedValues.push(value);
    }
  }
}




