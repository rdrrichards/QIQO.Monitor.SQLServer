import { createSelector, createReducer, on, Action, createFeatureSelector } from '@ngrx/store';
import { MonitoredServiceState } from './state';
import * as serviceMonitorActions from './service-monitor.actions';

const getServiceMonitorState = createFeatureSelector<MonitoredServiceState>('service-monitor');

const initialState: MonitoredServiceState = {
  resultInstances: [],
  monitoredServices: []
};

export const currentMonitoredServices = createSelector(
  getServiceMonitorState,
  state => state?.monitoredServices
                  .filter(ms => ms.monitors.some(m => m.monitorProperties
                        .some(p => p.propertyType === 'Monitor API Active' &&
                                   p.propertyDataType === 'boolean' &&
                                   p.propertyValue === '1')))
);

const appReducer = createReducer(initialState,
  on(serviceMonitorActions.processResultInstance, (state, { payload }) =>
    {
      const ris = state.resultInstances.map(r => r);
      ris.push(payload);
      const monServices = state.monitoredServices.map(s => s);
      const currService = monServices.find(s => s.serviceName === payload.serviceName);
      if (currService) {
        const currMon = currService.monitors.find(m => m.monitorName === payload.monitorName);
        if (currMon) { currMon.lastMonitorResult = payload; }
       }
      return { ...state, resultInstances: ris.slice(-250), monitoredServices: monServices };
    }),
  on(serviceMonitorActions.loadMonitoredServicesSuccess, (state, { payload }) =>
    ({ ...state, monitoredServices: payload })),
  on(serviceMonitorActions.loadMonitoredServicesFail, (state, { payload }) =>
    ({ ...state, monitoredServices: initialState.monitoredServices })),
);
// eslint-disable-next-line prefer-arrow/prefer-arrow-functions
export function reducer(state: MonitoredServiceState | undefined, action: Action): MonitoredServiceState {
  return appReducer(state, action);
}
