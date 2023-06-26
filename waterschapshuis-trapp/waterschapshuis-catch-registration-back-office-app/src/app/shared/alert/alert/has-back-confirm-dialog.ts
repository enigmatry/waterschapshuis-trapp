import { Observable } from 'rxjs';

export interface HasBackConfirmDialog {
    showDialog(): Observable<boolean>;
}
