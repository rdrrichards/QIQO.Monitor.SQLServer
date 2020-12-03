import { Component } from '@angular/core';
import { CommunicationService } from './shared/communication.service';

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
