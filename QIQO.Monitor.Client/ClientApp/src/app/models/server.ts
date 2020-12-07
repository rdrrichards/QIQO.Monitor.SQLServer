import { Service } from './service';
import { Environment } from './environment';

export interface Server {
  serverKey: number;
  serverName: string;
  services: Service[];
  environments: Environment[];
}
