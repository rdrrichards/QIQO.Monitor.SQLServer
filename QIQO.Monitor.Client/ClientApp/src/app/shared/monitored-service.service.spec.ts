import { TestBed } from '@angular/core/testing';

import { MonitoredServiceService } from './monitored-service.service';

describe('MonitoredServiceService', () => {
  let service: MonitoredServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MonitoredServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
