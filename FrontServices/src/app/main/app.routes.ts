import { Routes } from '@angular/router';
import { SharedRoutes } from '../shared';
import { OperationRoutes } from '@app/operation';

export const AppRoutes: Routes = [
	...OperationRoutes,
	...SharedRoutes,
];

