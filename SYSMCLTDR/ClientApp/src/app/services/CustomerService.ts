import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class CustomerService {
  baseUrl = "https://localhost:7245/Customers/fullinfo/";

  constructor(private http: HttpClient) { }

  async getCustomerDetails(id: string) {
    return await this.http.get(`${this.baseUrl}${id}`).toPromise();
  }
}
