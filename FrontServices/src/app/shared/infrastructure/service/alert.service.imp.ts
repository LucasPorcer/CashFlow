import Swal, { SweetAlertResult } from 'sweetalert2';
import { Injectable } from '@angular/core';
import { AlertServiceConstract } from '../../domain/service/alert.service.contract';

@Injectable()
export class AlertService extends AlertServiceConstract {
	public success(message: string): Promise<SweetAlertResult> {
		return Swal.fire({
      title: 'Sucesso',
      html: message,
      heightAuto: false,
      timer: 5000,
      icon: 'success',
      didOpen: () => {
        Swal.hideLoading();
      },
    });   
	}
  
  public error(title: string, message: string, error: string) {
    Swal.fire({
			icon: 'error',
			title: title,
      html: `<b>${message}</b><br><br>${error}<br>`,
      heightAuto: false,
      timer: 5000,
      didOpen: () => {
        Swal.hideLoading();
      },
      showConfirmButton: false,
		});
  }

  public errors(title: string, errors: string[]): void {
		Swal.fire({
      icon: 'error',
			title: title,
      html:`${errors.join("<br>")}`,
      heightAuto: false,
      timer: 5000,
      didOpen: () => {
        Swal.hideLoading();
      },
		});
  }
    
  public loading(): void {
    Swal.fire({
      title: '',
      allowEscapeKey: false,
      allowOutsideClick: false,
      html: 'Processando...',
      heightAuto: false,
      didOpen: () => {
        Swal.showLoading();
      },
    });
  }

  public confirm(message: string): Promise<SweetAlertResult> {
    return Swal.fire({
      title: 'Exclusão de registro',
      html: message,
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim, desejo deletar',
      cancelButtonText: 'Cancelar',
      icon: 'info',
      didOpen: () => {
        Swal.hideLoading();
      }
    });
  }

  public close(): void {
    Swal.close();
  }
}
