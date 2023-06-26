import { Component, OnInit, Inject } from '@angular/core';
import { TrapType } from '../../models/trap-type.model';
import { PolicyName } from 'src/app/core/auth/policy-name.enum';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ListItem } from 'src/app/shared/models/list-item';
import { LookupsService } from 'src/app/shared/services/lookups.service';

@Component({
  selector: 'app-edit-trap-type',
  templateUrl: './edit-trap-type.component.html',
  styleUrls: ['./edit-trap-type.component.scss']
})
export class EditTrapTypeComponent implements OnInit {
  selectedTrapType: TrapType = null;
  policyName = PolicyName;
  form: FormGroup;
  trappingTypes: ListItem[] = [];

  constructor(
    public dialogRef: MatDialogRef<EditTrapTypeComponent>,
    @Inject(MAT_DIALOG_DATA) data,
    private fb: FormBuilder,
    private lookupsService: LookupsService
  ) {
    this.selectedTrapType = data.selectedTrapType;
    this.createForm();
  }

  get f() { return this.form.controls; }

  ngOnInit(): void {
    if (!this.selectedTrapType) {
      this.selectedTrapType = new TrapType(null, null, null, null, null);
    }
    this.loadTrappingTypes();
    this.form.patchValue({
      ...this.selectedTrapType
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  saveAndClose(): void {
    if (this.form.invalid) {
      return;
    }

    this.dialogRef.close({ event: 'close', selectedTrapType: this.getFormValues() });
  }

  private createForm(): void {
    this.form = this.fb.group({
      name: [null, Validators.required],
      trappingTypeId: [null, Validators.required],
      active: [null, Validators.required],
      order: [null, [Validators.required, Validators.min(0), Validators.max(255), Validators.pattern('^[0-9]+$')]]
    });
  }

  loadTrappingTypes(): void {
    this.lookupsService.getTrappingTypes()
      .subscribe(response => {
        this.trappingTypes = response;
      });
  }

  getFormValues(): TrapType {
    return {
      ...this.selectedTrapType,
      name: this.form.value.name,
      trappingTypeId: this.form.value.trappingTypeId,
      trappingType: this.form.value.trappingType,
      active: this.form.value.active,
      order: this.form.value.order
    };
  }

}
