import { Component, OnDestroy, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { MonitoredServiceState } from './state/state';
import * as monitorServiceActions from './state/service-monitor.actions';
import * as monitorServiceReducer from './state/service-monitor.reducer';
import { Observable, Subscription } from 'rxjs';
import { Service } from '../models/service';
import { PrimeIcons } from 'primeng/api';

@Component({
  selector: 'qiqo-service-monitor',
  templateUrl: './service-monitor.component.html',
  styles: [
  ]
})
export class ServiceMonitorComponent implements OnInit, OnDestroy {
  healthStatus = ['Waiting...', 'Healthly', 'Degraded', 'Unhealthy'];
  healthStatusIcon = [PrimeIcons.QUESTION_CIRCLE, PrimeIcons.CHECK, PrimeIcons.EXCLAMATION_TRIANGLE, PrimeIcons.EXCLAMATION_CIRCLE];
  healthStatusColor = ['darkorange', 'limegreen', 'darkorange', 'orangered'];
  services$!: Observable<Service[]>;
  subs: Subscription[] = [];
  constructor(private featureStore: Store<MonitoredServiceState>) { }
  ngOnInit(): void {
    this.featureStore.dispatch(monitorServiceActions.loadMonitoredServices());
    // this.subs.push(this.featureStore.pipe(select(monitorServiceReducer.currentMonitoredServices)).subscribe());
    this.services$ = this.featureStore.pipe(select(monitorServiceReducer.currentMonitoredServices));
  }
  ngOnDestroy(): void {
    this.subs.forEach(sub => sub.unsubscribe());
  }
  healthStatusLabel(status?: number): string {
    return (status && status > -1) ? this.healthStatus[status] : 'Unknown';
  }
}
