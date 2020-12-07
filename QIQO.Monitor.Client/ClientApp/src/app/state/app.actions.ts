import { createAction, props, union } from '@ngrx/store';
import { ResultInstance } from '../models/result-instance';
import { User } from '../models/user';

export const loadUserInformation = createAction('[Application] Load User Information');
export const loadUserInformationSuccess = createAction('[Application] Load User Information Success', props<{ payload: User }>());
export const loadUserInformationFail = createAction('[Application] Load User Information Fail', props<{ payload: string }>());
export const processResultInstance = createAction('[Monitoring] Process Result Instance', props<{ payload: ResultInstance }>());

const actions = union({
  loadUserInformation, loadUserInformationSuccess, loadUserInformationFail, processResultInstance
});
export type ApplicationActionsUnion = typeof actions;
