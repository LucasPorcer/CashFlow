import { Component, OnDestroy, OnInit } from '@angular/core';
import { CashflowDailyBalanceDto } from '@app/operation/domain/cashflow/dto/cashflow-daily-balance.dto';
import { CashflowServiceContract } from '@app/operation/domain/cashflow/service/cashflow.service.contract';
import { AlertServiceConstract } from '@app/shared/domain/service/alert.service.contract';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
})

export class ReportComponent implements OnInit, OnDestroy {

    report!: CashflowDailyBalanceDto[];
    get$ = new Subscription();

    constructor( private alertService: AlertServiceConstract,
        private cashFlowService: CashflowServiceContract)  {
    }

    ngOnDestroy(): void {
        this.get$.unsubscribe();
    }

    ngOnInit() {
        this.alertService.loading();
        this.get$ = this.cashFlowService.getConsolidatedDailyBalanceObservable().subscribe(report => {
            this.report = report;
            this.alertService.close();
        });
    }
}