import { Component, OnInit, Inject } from '@angular/core';
import { FieldTest } from '../../models/field-test.model';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AreasService } from 'src/app/shared/services/areas.service';
import { ListItem } from 'src/app/shared/models/list-item';

const periodRegEx = '^[0-9]{4}-[0-9]{2}$';
@Component({
  selector: 'app-edit-field-test',
  templateUrl: './edit-field-test.component.html',
  styleUrls: ['./edit-field-test.component.scss']
})
export class EditFieldTestComponent implements OnInit {
  selectedFieldTest: FieldTest = null;
  policyName = PolicyName;
  form: FormGroup;
  allHourSquares: ListItem[] = [];


  constructor(public dialogRef: MatDialogRef<EditFieldTestComponent>,
              @Inject(MAT_DIALOG_DATA) data,
              private fb: FormBuilder,
              private areasService: AreasService) {
                this.selectedFieldTest = data.selectedFieldTest;
                this.createForm();
              }

  get f() { return this.form.controls; }

  ngOnInit(): void {
    if (!this.selectedFieldTest) {
      this.selectedFieldTest = new FieldTest(null, null, null, null, []);
    }
    this.loadHourSquares();
    this.form.patchValue({
      ...this.selectedFieldTest,
      hourSquareIds: this.selectedFieldTest.hourSquares.map(x => x.id)
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  saveAndClose(): void {
    if (this.form.invalid) {
      return;
    }
    this.toCatchType();
    this.dialogRef.close({ event: 'close', selectedFieldTest: this.selectedFieldTest});
  }

  private createForm(): void {
    this.form = this.fb.group({
      name: [null, Validators.required],
      startPeriod: [null, [Validators.required, Validators.pattern(periodRegEx)]],
      endPeriod: [null, [Validators.required, Validators.pattern(periodRegEx)]],
      hourSquareIds: [null]
    });
  }

  toCatchType(): FieldTest {
    return Object.assign(this.selectedFieldTest, this.form.value);
  }

  loadHourSquares(): void {
    this.areasService.getHourSquares(undefined)
      .subscribe(response => {
        this.allHourSquares = response;
      });
  }

}
