import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Service } from '../models/service';

@Injectable({
  providedIn: 'root'
})
export class MonitoredServiceService {

  constructor(private httpClient: HttpClient) { }
  getMonitoredServices(): Observable<Service[]> {
    return this.httpClient.get<Service[]>(`api/services`);
  }
}
