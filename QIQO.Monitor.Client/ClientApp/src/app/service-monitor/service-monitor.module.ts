import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { reducer } from './state/service-monitor.reducer';

import { ServiceMonitorRoutingModule } from './service-monitor-routing.module';
import { ServiceMonitorComponent } from './service-monitor.component';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { MonitoredServiceEffects } from './state/service-monitor.effects';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [ServiceMonitorComponent],
  imports: [
    CommonModule,
    SharedModule,
    StoreModule.forFeature('service-monitor', reducer),
    EffectsModule.forFeature([MonitoredServiceEffects]),
    ServiceMonitorRoutingModule
  ]
})
export class ServiceMonitorModule { }
