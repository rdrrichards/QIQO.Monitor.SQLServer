import { ResultInstance } from '../../models/result-instance';
import { Service } from '../../models/service';

export interface MonitoredServiceState {
  resultInstances: ResultInstance[];
  monitoredServices: Service[];
}

