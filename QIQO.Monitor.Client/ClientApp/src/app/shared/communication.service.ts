import { Injectable } from '@angular/core';
import * as sr from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { AppState } from '../state/state';
import * as appActions from '../state/app.actions';
import { ResultInstance } from '../models/result-instance';

@Injectable({
  providedIn: 'root'
})
export class CommunicationService {
  private conn: sr.HubConnection | undefined;
  constructor(private appStore: Store<AppState>) {
    this.openWSConnection();
  }
  private openWSConnection(): void {
    this.conn = new sr.HubConnectionBuilder()
      .configureLogging(sr.LogLevel.Error)
      .withUrl('/monitorHub',
        { skipNegotiation: true, transport: sr.HttpTransportType.WebSockets })
      .withAutomaticReconnect()
      .build();
    this.conn.start().catch(err => console.log(err));
    this.conn.on('join', (userName: string) => console.log(`user ${userName} has logged into Specimen Transport.`));
    this.conn.on('monitorresult', (result: ResultInstance) => {
      console.log('result', result);
      this.appStore.dispatch(appActions.processResultInstance({ payload: result }));
      // console.log('result parsed', JSON.parse(result));
      // // We need to dispatch to state here, rather than use a subject
      // if (this.processor.processResult(pResult)) {
      //   this.conn.invoke('acknowledge', pResult.racks[0].barcode);
      // }
    });
    this.conn.on('joingroupconfirmed', (result: string) => {
      console.log('join group confirmed', result);
    });
    this.conn.onreconnected(connectionId => {
      console.log('sockets reconnected', connectionId);
      // if (this.compName) {
      //   this.conn.invoke('joingroup', this.compName);
      // }
    });
  }
  joinIn(user: string = ''): void {}
  // joinIn(user: User): void {
  //   if (this.conn.state === sr.HubConnectionState.Connected) {
  //     this.conn.invoke('join', user.userInits);
  //     if (this.compName) {
  //       this.conn.invoke('joingroup', this.compName);
  //     }
  //   }
  // }
}
