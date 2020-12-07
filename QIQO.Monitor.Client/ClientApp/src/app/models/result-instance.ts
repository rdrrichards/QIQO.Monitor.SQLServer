import { MonitorResult } from './monitor-result';
import { Exception } from './exception';

export interface ResultInstance {
  monitorResult: MonitorResult;
  hasError: boolean;
  exception?: Exception;
  healthStatus: HealthStatus;
  monitorName: string;
}

export interface ResultMessage {
  result: ResultInstance;
  resultType: ResultType;
}

export enum ResultType {
  Health,
  Blocking,
  OpenTransaction,
  WaitStats
}

export enum HealthStatus {
  Healthly,
  Degraded,
  Unhealthy
}

export interface ResultInstanceValue {
  imagePath?: string;
  inError?: boolean;
  status?: string;
  styles?: boolean;
  styleClass?: string;
  errorMessage?: string;
}

export interface ResultInstanceValueMessage extends ResultMessage {
  value: ResultInstanceValue;
}

export interface ResultInstanceStore extends ResultInstanceValueMessage {
  serverKey: number;
  serviceKey: number;
  monitorKey?: number;
}
