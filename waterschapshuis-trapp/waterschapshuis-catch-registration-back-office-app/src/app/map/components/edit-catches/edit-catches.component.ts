import { Component, OnInit, Inject } from '@angular/core';
import { CatchDetails } from '../../models/catch-details.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { IListItem } from 'src/app/shared/models/list-item';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-catches',
  templateUrl: './edit-catches.component.html',
  styleUrls: ['./edit-catches.component.scss']
})
export class EditCatchesComponent implements OnInit {
  catch: CatchDetails;
  catchTypes: IListItem[] = [];
  policyName = PolicyName;
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CatchDetails>,
    @Inject(MAT_DIALOG_DATA) data,
    private lookupsService: LookupsService,
    private fb: FormBuilder
  ) {
    this.catch = data.catch;
    this.createForm();
  }

  ngOnInit(): void {
    this.loadCatchTypes();
    this.form.patchValue({
      ...this.catch
    });
  }

  loadCatchTypes(): void {
    const isByCatch = this.form.get('isByCatch').value != null ? this.form.get('isByCatch').value : this.catch.isByCatch;
    this.lookupsService.getCatchTypes(isByCatch)
      .subscribe(response => {
        this.catchTypes = response;
      });
  }

  close(): void {
    this.dialogRef.close();
  }

  saveAndClose(): void {
    this.dialogRef.close({ event: 'close', catch: this.getFormValues() });
  }

  onChangeIsByCatch(): void {
    this.loadCatchTypes();
  }

  private createForm(): void {
    this.form = this.fb.group({
      isByCatch: [null, Validators.required],
      catchTypeId: [null, Validators.required],
      number: [null, Validators.required]
    });
  }

  getFormValues(): CatchDetails {
    return {
      ...this.catch,
      catchTypeId: this.form.value.catchTypeId,
      number: this.form.value.number,
      isByCatch: this.form.value.isByCatch
    };
  }

}
