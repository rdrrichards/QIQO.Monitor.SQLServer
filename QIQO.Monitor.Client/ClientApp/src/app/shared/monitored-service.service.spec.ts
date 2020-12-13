import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MonitoredServiceService } from './monitored-service.service';

describe('MonitoredServiceService', () => {
  let service: MonitoredServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(MonitoredServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
