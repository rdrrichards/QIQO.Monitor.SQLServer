import { Monitor } from './monitor';
import { Environment } from './environment';

export interface Service {
  serviceKey: number;
  serverKey: number;
  serviceName: string;
  instanceName: string;
  serviceSource: string;
  serviceType: number;
  monitors: Monitor[];
  environments: Environment[];
}

