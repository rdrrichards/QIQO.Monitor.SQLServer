import { createAction, props, union } from '@ngrx/store';
import { User } from '../models/user';

export const loadUserInformation = createAction('[Application] Load User Information');
export const loadUserInformationSuccess = createAction('[Application] Load User Information Success', props<{ payload: User }>());
export const loadUserInformationFail = createAction('[Application] Load User Information Fail', props<{ payload: string }>());

const actions = union({
  loadUserInformation, loadUserInformationSuccess, loadUserInformationFail
});
export type ApplicationActionsUnion = typeof actions;
