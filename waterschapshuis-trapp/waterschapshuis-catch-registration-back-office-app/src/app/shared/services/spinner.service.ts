import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SpinnerService {
    isLoading = new Subject<boolean>();
    private requestCounter = 0;

    constructor() { }

    hide() {
        if (this.requestCounter > 0) {
            this.requestCounter--;
        }
        this.isLoading.next(this.requestCounter > 0);
    }

    show() {
        this.requestCounter++;
        this.isLoading.next(true);
    }

}
