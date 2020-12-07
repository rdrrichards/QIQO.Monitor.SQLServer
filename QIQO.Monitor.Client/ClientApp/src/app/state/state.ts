import { ResultInstance } from '../models/result-instance';
import { User } from '../models/user';

export interface AppState {
  user: User;
  title: string;
  resultInstances: ResultInstance[];
}

