
import { ServiceBaseContract } from '@app/shared/domain/service/service.base.contract';
import { CashflowModel } from '../model/cashflow.model';
import { CashflowDailyBalanceDto } from '../dto/cashflow-daily-balance.dto';
import { Observable } from 'rxjs';

export abstract class CashflowServiceContract extends ServiceBaseContract<CashflowModel> {
    abstract getConsolidatedDailyBalanceObservable(): Observable<CashflowDailyBalanceDto[]>
}