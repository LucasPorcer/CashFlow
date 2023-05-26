import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CashflowModel } from '@app/operation/domain/cashflow/model/cashflow.model';
import { CashflowServiceContract } from '@app/operation/domain/cashflow/service/cashflow.service.contract';
import { AlertServiceConstract } from '@app/shared/domain/service/alert.service.contract';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-entry',
  templateUrl: './entry.component.html',
  styleUrls: ['./entry.component.css']
})
export class EntryComponent implements OnInit, OnDestroy {

    entryForm!: FormGroup;
    save$ = new Subscription();
    submitted = false;

    get fields() { 
        return this.entryForm.controls; 
    }

    constructor(private formBuilder: FormBuilder,
        private alertService: AlertServiceConstract,
        private cashFlowService: CashflowServiceContract) {
    }

    ngOnInit() {
        this.entryForm = this.formBuilder.group({
            entry: ['', Validators.required],
            paymentType: ['', Validators.required]
        });
    }

    onClickSubmit() {
        this.submitted = true;
        if(!this.entryForm.valid) {
            return;
        }

        this.alertService.loading();

        const entryInputValue = parseFloat(this.entryForm.value.entry);

        // Verifica qual radiobutton está selecionado
        const isDebitSelected = this.entryForm.value.paymentType === 'debit';
      
        // Transforma o valor para negativo se for débito
        const entryValue = isDebitSelected ? -entryInputValue : entryInputValue;

        console.log(entryValue);

        const cashflow = CashflowModel.CashflowModelBuilder.Create()
            .withValue(entryValue)
            .Make();

        this.save$ = this.cashFlowService.addObservable(cashflow).subscribe(() => {
            this.submitted = false;
            this.entryForm.reset();

            this.alertService.success(`Entrada de fluxo de caixa efetuada com sucesso.`)
        });
    }

    onEntryInputChange(event: any) {
        const inputValue: string = event.target.value;
        const sanitizedValue: string = inputValue.replace(/-/g, '');
        this.entryForm.get('entry')?.setValue(sanitizedValue);
      }

      focusToEnd(inputElement: any) {
        inputElement.setSelectionRange(inputElement.value.length, inputElement.value.length);
      }

    ngOnDestroy(): void {
        this.save$.unsubscribe();
    }
}
