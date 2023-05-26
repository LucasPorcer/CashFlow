import { SweetAlertResult } from 'sweetalert2';

export abstract class AlertServiceConstract {
    abstract success(message: string): Promise<SweetAlertResult>;
    abstract errors(title: string, errors: string[]): void;
    abstract error(title: string, message: string, error: string): void;
    abstract loading(): void;
    abstract close(): void;	
    abstract confirm(message: string): Promise<SweetAlertResult>;
}