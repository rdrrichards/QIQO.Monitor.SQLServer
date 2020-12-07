import { createSelector, createReducer, on, Action } from '@ngrx/store';
import { AppState } from './state';
import * as applicationActions from './app.actions';
import { User } from '../models/user';

export const getAppState = (state: any) => state.app;

const initialState: AppState = {
  user: {} as User,
  title: 'QIQO Monitor',
  resultInstances: []
};

export const getCurrentUser = createSelector(
  getAppState,
  state => { if (state) { return state.user; } else { return initialState.user; } }
);
export const getTitle = createSelector(
  getAppState,
  state => { if (state) { return state.title; } else { return initialState.title; } }
);
export const currentWorkMode = createSelector(
  getAppState,
  state => state?.workMode
);

const appReducer = createReducer(initialState,
  on(applicationActions.loadUserInformationSuccess, (state, { payload }) =>
    ({ ...state, user: payload })),
  on(applicationActions.loadUserInformationFail, (state, { payload }) =>
    ({ ...state, user: initialState.user })),
  on(applicationActions.processResultInstance, (state, { payload }) =>
    {
      state.resultInstances.push(payload);
      return { ...state };
    }),
);
export function reducer(state: AppState | undefined, action: Action): AppState {
  return appReducer(state, action);
}
