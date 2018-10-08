import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { User } from '../data/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public baseUrl: string = '/api';

  constructor(private http: Http) {
  }

  getUserDetails(): Observable<User> {
    return this.http.get('auth/discord/details');
  }
}
