import { Result } from './result';
import { ResultType } from './result-instance';

export interface MonitorResult {
  results: Result[];
  resultType: ResultType;
  resultDateTime: Date;
}
