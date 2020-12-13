import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { SharedModule } from 'primeng/api';
import { ApplicationEffects } from '../state/app.effects';

import { ServiceMonitorComponent } from './service-monitor.component';
import { reducer } from './state/service-monitor.reducer';
import * as fromApp from '../state/app.reducer';
import { MonitoredServiceEffects } from './state/service-monitor.effects';

describe('ServiceMonitorComponent', () => {
  let component: ServiceMonitorComponent;
  let fixture: ComponentFixture<ServiceMonitorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceMonitorComponent ],
      imports: [BrowserAnimationsModule, SharedModule, HttpClientTestingModule, RouterTestingModule,
        StoreModule.forRoot({ app: fromApp.reducer }, {
          runtimeChecks: {
            strictStateImmutability: false,
            strictActionImmutability: false
          }
        }),
        EffectsModule.forRoot([ApplicationEffects]),
        SharedModule,
        StoreModule.forFeature('service-monitor', reducer),
        EffectsModule.forFeature([MonitoredServiceEffects])
      ]
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
