import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'service-monitor', pathMatch: 'full' },
  { path: 'service-monitor', loadChildren: () => import('./service-monitor/service-monitor.module').then(m => m.ServiceMonitorModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
