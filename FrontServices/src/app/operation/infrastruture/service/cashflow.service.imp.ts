import { environment } from '@src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ErrorHandlerContract } from '@app/shared/domain/error/error.handler.contract';
import { ServiceBase } from '@app/shared/infrastructure/service/service.base.imp';
import { CashflowModel } from '@app/operation/domain/cashflow/model/cashflow.model';
import { CashflowServiceContract } from '@app/operation/domain/cashflow/service/cashflow.service.contract';
import { CashflowDailyBalanceDto } from '@app/operation/domain/cashflow/dto/cashflow-daily-balance.dto';
import { Observable, catchError, tap, throwError } from 'rxjs';

@Injectable()
export class CashflowService extends ServiceBase<CashflowModel> implements CashflowServiceContract {
    override baseUri = `${environment.api.endpoint}/cashflow`;

    public constructor(http: HttpClient, errorHandler: ErrorHandlerContract) {
        super(http, errorHandler);
    } 
	
	public getConsolidatedDailyBalanceObservable(): Observable<CashflowDailyBalanceDto[]> {
		return this.http.get<CashflowDailyBalanceDto[]>(`${this.baseUri}`)
			.pipe(
				tap(response => {
					return response;
				}),
				catchError(error => {
					this.errorHandler.throw(error);
					return throwError(error);
				})
			);
    }
}