import { Observable } from 'rxjs';
import { DomainEntity } from '../model/domain.entity.model';
import { HttpParams } from '@angular/common/http';

export abstract class ServiceBaseContract<T extends DomainEntity> {
	abstract addObservable(model: T): Observable<T>;
}