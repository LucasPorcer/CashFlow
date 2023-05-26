import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DomainEntity } from '../../domain/model/domain.entity.model';
import { ErrorHandlerContract } from '../../domain/error/error.handler.contract';

export class ServiceBase<T extends DomainEntity> {
    protected baseUri!: string;

	public constructor(public http: HttpClient,
		public errorHandler: ErrorHandlerContract) {
    }

    public addObservable(model: T): Observable<T> {''
		return this.http.post<T>(this.baseUri, model)
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