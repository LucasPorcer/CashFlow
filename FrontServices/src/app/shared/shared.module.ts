import { CommonModule, CurrencyPipe } from "@angular/common";
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { FooterComponent } from "./presentation/footer/footer.component";
import { HeaderComponent } from "./presentation/header/header.component";
import { ErrorHandlerContract } from "./domain/error/error.handler.contract";
import { ErrorHandler } from "./infrastructure/error/error.handler.imp";
import { AlertServiceConstract } from "./domain/service/alert.service.contract";
import { AlertService } from "./infrastructure/service/alert.service.imp";

@NgModule({
	imports: [
		CommonModule,
		BrowserModule,
		RouterModule,
		FormsModule,
	],
	declarations: [
		FooterComponent,
		HeaderComponent,
	],
	exports: [
		FooterComponent,
		HeaderComponent,
		RouterModule,
		CommonModule,
	],
	schemas: [CUSTOM_ELEMENTS_SCHEMA],
	providers: [
		{
			provide: ErrorHandlerContract,
			useClass: ErrorHandler
		},
		{
			provide: AlertServiceConstract,
			useClass: AlertService
		},
	]
})
export class SharedModule { }
