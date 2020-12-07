import { HealthStatus } from './result-instance';

export interface BlockingResult {
  lockType: string;
  database: string;
  blockObject: number;
  lockRequest: string;
  waiterSid: number;
  waitTime: number;
  waiterBatch: string;
  waiterStatement: string;
  blockerSid: number;
  blockerBatch: string;
}
export interface OpenTransactionResult {
  sessionId: number;
  hostName: string;
  loginName: string;
  transactionID: number;
  transactionName: string;
  transactionBegan: Date;
  databaseId: number;
  databaseName: string;
}

export interface HealthStatusResult {
  healthStatus: HealthStatus;
}
export type Result = BlockingResult
  | OpenTransactionResult
  | HealthStatusResult;
