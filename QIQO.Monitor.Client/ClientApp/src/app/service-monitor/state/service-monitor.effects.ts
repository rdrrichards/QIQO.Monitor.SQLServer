import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { mergeMap, map, tap, catchError } from 'rxjs/operators';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import * as monitorServiceActions from './service-monitor.actions';
import { MonitoredServiceService } from '../../shared/monitored-service.service';
import { Service } from '../../models/service';

@Injectable()
export class MonitoredServiceEffects {
  loadMonitoredServices$ = createEffect(() => this.actions$.pipe(
    ofType(monitorServiceActions.loadMonitoredServices),
    mergeMap(_ => this.monServices.getMonitoredServices().pipe(
      tap(__ => console.log('Performing LoadUserInformation in loadUserInformation$ effect...')),
      map((results: Service[]) => monitorServiceActions.loadMonitoredServicesSuccess({ payload: results })),
      catchError(err => of(monitorServiceActions.loadMonitoredServicesFail(err)))
      ))
    )
  );
  constructor(private actions$: Actions, private monServices: MonitoredServiceService) { } // private authService: AuthenticationService
}
