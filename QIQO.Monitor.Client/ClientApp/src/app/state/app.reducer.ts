import { createSelector, createReducer, on, Action } from '@ngrx/store';
import { AppState } from './state';
import * as applicationActions from './app.actions';
import { User } from '../models/user';

export const getAppState = (state: any) => state.app as AppState;

const initialState: AppState = {
  user: {} as User,
  title: 'QIQO Monitor'
};

export const getCurrentUser = createSelector(
  getAppState,
  state => state?.user
);
export const getTitle = createSelector(
  getAppState,
  state => state?.title
);

const appReducer = createReducer(initialState,
  on(applicationActions.loadUserInformationSuccess, (state, { payload }) =>
    ({ ...state, user: payload })),
  on(applicationActions.loadUserInformationFail, (state, { payload }) =>
    ({ ...state, user: initialState.user })),
);
// eslint-disable-next-line prefer-arrow/prefer-arrow-functions
export function reducer(state: AppState | undefined, action: Action): AppState {
  return appReducer(state, action);
}
