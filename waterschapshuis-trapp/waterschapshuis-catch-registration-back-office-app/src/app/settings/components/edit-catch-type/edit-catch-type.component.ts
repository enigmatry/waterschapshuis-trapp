import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CatchType } from '../../models/catch-type.model';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { animals } from '../../models/animal-type.model';


@Component({
  selector: 'app-edit-catch-type',
  templateUrl: './edit-catch-type.component.html',
  styleUrls: ['./edit-catch-type.component.scss']
})
export class EditCatchTypeComponent implements OnInit {
  selectedCatchType: CatchType = null;
  policyName = PolicyName;
  form: FormGroup;
  animals = animals;


  constructor(public dialogRef: MatDialogRef<EditCatchTypeComponent>,
              @Inject(MAT_DIALOG_DATA) data,
              private fb: FormBuilder) {
                this.selectedCatchType = data.selectedCatchType;
                this.createForm();
               }

  get f() { return this.form.controls; }

  ngOnInit() {
    if (!this.selectedCatchType) {
      this.selectedCatchType = new CatchType(null, null, null, null, null);
    }
    this.form.patchValue({
      ...this.selectedCatchType
    });
  }

  close() {
    this.dialogRef.close();
  }

  saveAndClose() {
    if (this.form.invalid) {
      return;
    }
    this.toCatchType();
    this.dialogRef.close({ event: 'close', selectedCatchType: this.selectedCatchType});
  }

  private createForm(): void {
    this.form = this.fb.group({
      name: [null, Validators.required],
      isByCatch: [null, Validators.required],
      animalType: [null, Validators.required],
      order: [null, [Validators.required, Validators.min(0), Validators.max(255), Validators.pattern('^[0-9]+$')]]
    });
  }

  toCatchType(): CatchType {
    return Object.assign(this.selectedCatchType, this.form.value);
  }

}

