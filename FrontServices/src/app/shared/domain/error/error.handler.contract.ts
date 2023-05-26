import { HttpErrorResponse } from '@angular/common/http';

export abstract class ErrorHandlerContract {
    abstract throw(error: HttpErrorResponse): void;
}