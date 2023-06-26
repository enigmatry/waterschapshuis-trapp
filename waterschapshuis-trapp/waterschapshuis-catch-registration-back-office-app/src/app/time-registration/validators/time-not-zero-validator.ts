import { AbstractControl, ValidatorFn } from '@angular/forms';

export function forbidZeroTimeValueValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const forbidden = (/00:00/i).test(control.value);
    return forbidden ? { forbiddenName: { value: control.value } } : null;
  };
}
