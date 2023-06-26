import { FormGroup } from '@angular/forms';

export function groupBy(xs: Array<any>, key: string): any {
  return xs.reduce((rv, x) => {
    (rv[x[key]] = rv[x[key]] || []).push(x);
    return rv;
  }, {});
}

export function parseHours(value: string): number {
  if (value) {
    const values = value.split(':');
    return Number.parseInt(values[0], 10);
  }
}

export function parseMinutes(value: string): number {
  if (value) {
    const values = value.split(':');
    return Number.parseInt(values[1], 10);
  }
}

export function toDisplayString(minutes: number, hours?: number): string {
  if (isNaN(minutes)) {
    return;
  }
  if (hours !== undefined && minutes < 60) {
    return `${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}`;
  } else {
    hours = Math.floor(minutes / 60);
    minutes = hours === 0 ? minutes : minutes % (hours * 60);
    return toDisplayString(minutes, hours);
  }
}

export function resetFormAndClearValidators(itemFormGroup: FormGroup, keysToOmit?: string[]) {
  Object.keys(itemFormGroup.controls).forEach(key => {
    if (keysToOmit && keysToOmit.some(e => e === key)) { return; }

    itemFormGroup.controls[key].clearValidators();
    itemFormGroup.controls[key].reset();
  });
}

export function updateFormValueAndValidity(itemFormGroup: FormGroup, keysToOmit?: string[]) {
  Object.keys(itemFormGroup.controls).forEach(key => {
    if (keysToOmit && keysToOmit.some(e => e === key)) { return; }

    itemFormGroup.controls[key].updateValueAndValidity();
  });
}
