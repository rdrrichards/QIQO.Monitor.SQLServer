import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceMonitorComponent } from './service-monitor.component';

describe('ServiceMonitorComponent', () => {
  let component: ServiceMonitorComponent;
  let fixture: ComponentFixture<ServiceMonitorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceMonitorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceMonitorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
