import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { CommunicationService } from './shared/communication.service';
import { AppState } from './state/state';
import * as appActions from './state/app.actions';

@Component({
  selector: 'qiqo-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent {
  title = 'monitor-client';
  constructor(private commsService: CommunicationService) {
    this.commsService.joinIn();
  }
}
