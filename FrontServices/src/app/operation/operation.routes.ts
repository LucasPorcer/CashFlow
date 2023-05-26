import { EntryComponent } from "./presentation/entry/entry.component";
import { ReportComponent } from "./presentation/report/report.component";

type PathMatch = "full" | "prefix" | undefined;

export const OperationRoutes = [
	{ 
		path: '', 
		title: 'Home',
		redirectTo: 'cashEntry', 
		pathMatch: 'full' as PathMatch,
	},
	{
		path: 'cashEntry',
		component: EntryComponent,
	},
	{
		path: 'report',
		component: ReportComponent,
	},
];
