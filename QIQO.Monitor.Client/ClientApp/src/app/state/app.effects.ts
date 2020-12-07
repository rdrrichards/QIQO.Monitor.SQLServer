import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { mergeMap, map, tap, catchError } from 'rxjs/operators';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import * as appActions from './app.actions';
// import { AuthenticationService } from '../shared/authentication.service';
import { User } from '../models/user';

@Injectable()
export class ApplicationEffects {
  constructor(private actions$: Actions) { } // private authService: AuthenticationService
  // loadUserInformation$ = createEffect(() => this.actions$.pipe(
  //   ofType(appActions.loadUserInformation),
  //   mergeMap((action) => this.authService.loadUserInformation().pipe(
  //     tap(_ => console.log('Performing LoadUserInformation in loadUserInformation$ effect...')),
  //     map((results: User) => appActions.loadUserInformationSuccess({ payload: results })),
  //     catchError(err => of(appActions.loadUserInformationFail(err)))
  //     ))
  //   )
  // );
}
