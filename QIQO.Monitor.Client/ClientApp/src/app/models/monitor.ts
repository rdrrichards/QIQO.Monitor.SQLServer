/* eslint-disable no-shadow */
/* eslint-disable @typescript-eslint/naming-convention */
import { Query } from './query';
import { ResultInstance } from './result-instance';

export enum MonitorLevel {
  Instance = 1,
  Database
}

export enum MonitorCategories {
  Version = 1,
  SQLServerHardware,
  DetectBlocking,
  OpenTranactions,
  WaitStatistics
}

export interface Monitor {
  monitorKey: number;
  monitorName: string;
  monitorType: number;
  monitorLevel: MonitorLevel;
  monitorCategory: MonitorCategories;
  queries: Query[];
  monitorProperties: MonitorProperty[];
  lastMonitorResult: ResultInstance;
}

export interface MonitorCategory {
  categoryKey: number;
  categoryName: string;
}

export interface MonitorProperty {
  propertyType: string;
  propertyDataType: string;
  propertyValue: string;
}
