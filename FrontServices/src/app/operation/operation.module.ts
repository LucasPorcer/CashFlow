import { CommonModule } from "@angular/common";
import { EntryComponent } from "./presentation/entry/entry.component";
import { ReportComponent } from "./presentation/report/report.component";
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { SharedModule } from "@app/shared";
import { CashflowServiceContract } from "./domain/cashflow/service/cashflow.service.contract";
import { CashflowService } from "./infrastruture/service/cashflow.service.imp";
import { CurrencyMaskModule } from "ng2-currency-mask";

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		BrowserModule,
		SharedModule,
		ReactiveFormsModule,
		CurrencyMaskModule
	],
	declarations: [
		EntryComponent,
		ReportComponent,
	],
	providers: [
		{
			provide: CashflowServiceContract,
			useClass: CashflowService
		},
	],
	schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class OperationModule { }
