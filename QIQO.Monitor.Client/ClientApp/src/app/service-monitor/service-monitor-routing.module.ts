import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ServiceMonitorComponent } from './service-monitor.component';

const routes: Routes = [{ path: '', component: ServiceMonitorComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ServiceMonitorRoutingModule { }
