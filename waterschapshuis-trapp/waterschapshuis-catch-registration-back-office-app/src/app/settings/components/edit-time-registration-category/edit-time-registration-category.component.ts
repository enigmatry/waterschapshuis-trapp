import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { TimeRegistrationCategory } from '../../models/time-registration-category';

@Component({
  selector: 'app-edit-time-registration-category',
  templateUrl: './edit-time-registration-category.component.html',
  styleUrls: ['./edit-time-registration-category.component.scss']
})
export class EditTimeRegistrationCategoryComponent implements OnInit {
  selectedTimeRegistrationCategory: TimeRegistrationCategory = null;
  policyName = PolicyName;
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<EditTimeRegistrationCategoryComponent>,
    @Inject(MAT_DIALOG_DATA) data,
    private fb: FormBuilder
  ) {
    this.selectedTimeRegistrationCategory = data.selectedTimeRegistrationCategory;
    this.createForm();
  }

  ngOnInit(): void {
    if (!this.selectedTimeRegistrationCategory) {
      this.selectedTimeRegistrationCategory = new TimeRegistrationCategory(null, null, true);
    }
    this.form.patchValue({
      ...this.selectedTimeRegistrationCategory
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  saveAndClose(): void {
    if (this.form.invalid) {
      return;
    }

    this.dialogRef.close({ event: 'close', selectedTimeRegistrationCategory: this.getFormValues() });
  }

  private createForm(): void {
    this.form = this.fb.group({
      name: [null, Validators.required],
      active: [null, Validators.required]
    });
  }

  getFormValues(): TimeRegistrationCategory {
    return {
      ...this.selectedTimeRegistrationCategory,
      name: this.form.value.name,
      active: this.form.value.active
    };
  }

}
