import { createAction, props, union } from '@ngrx/store';
import { ResultInstance } from '../../models/result-instance';
import { Service } from '../../models/service';

export const processResultInstance = createAction('[Monitoring] Process Result Instance', props<{ payload: ResultInstance }>());

export const loadMonitoredServices = createAction('[Application] Load Monitored Services');
export const loadMonitoredServicesSuccess = createAction('[Application] Load Monitored Services Success', props<{ payload: Service[] }>());
export const loadMonitoredServicesFail = createAction('[Application] Load Monitored Services Fail', props<{ payload: string }>());

const actions = union({
  processResultInstance, loadMonitoredServices, loadMonitoredServicesSuccess, loadMonitoredServicesFail
});
export type MonitoredServiceActionsUnion = typeof actions;
