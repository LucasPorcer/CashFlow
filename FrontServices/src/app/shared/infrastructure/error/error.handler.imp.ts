import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandlerContract } from "../../domain/error/error.handler.contract";
import { AlertServiceConstract } from "../../domain/service/alert.service.contract";
import { Injectable } from "@angular/core";

@Injectable()
export class ErrorHandler extends ErrorHandlerContract {
    constructor(private alertService: AlertServiceConstract) {
        super();
    }

    throw(response: HttpErrorResponse) {
        if (response) {
            switch(response.status) {
                case 0: 
                    this.alertService.error('Erro', 'Não foi possível concluir a solicitação', 'A comunicação não está disponível neste momento');
                break;
                case 400:
                    this.alertService.errors('Não foi possível concluir a solicitação', response.error.errors);
                break;
                case 401:
                    this.alertService.error('Erro', 'Não foi possível concluir a solicitação', 'Você não está logado ou não possui permissões para esta solicitação');
                break;
                case 404:
                    const message = response.error ? response.error.error : 'Recurso não encontrado'; 
                    this.alertService.error('Erro', 'Não foi possível concluir a solicitação', message);
                break;
                case 500:
                    this.alertService.error('Erro', 'Não foi possível concluir a solicitação', response.error.error);
                break;
                default:
                    this.alertService.error('Erro', 'Não foi possível concluir a solicitação', 'Erro desconhecido');
            }
        }
    }
}