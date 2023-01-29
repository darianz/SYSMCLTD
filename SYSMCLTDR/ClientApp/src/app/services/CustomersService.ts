import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  private readonly baseUrl = 'https://localhost:7245/Customers/';

  constructor(private http: HttpClient) { }

  async getCustomers() {
    try {
      return await this.http.get<any[]>(this.baseUrl).toPromise();
    } catch (error) {
      console.error(error);
      return [];
    }
  }

  async addCustomer(customer: any) {
    try {
      await this.http.post(this.baseUrl, customer).toPromise();
      console.log('Customer added successfully');
    } catch (error) {
      console.error(error);
    }
  }

  async updateCustomer(CustomerNumber: any, name: string) {
    try {
    
      const res = await this.http.patch(this.baseUrl + CustomerNumber, { Name: name } ).toPromise();
      return res;
    }
    catch (e) {
      console.log(e);
    }
   return
  }

  async deleteCustomer(CustomerNumber: any) {
    try {

      const res = await this.http.delete(this.baseUrl + CustomerNumber).toPromise();
      return res;
    }
    catch (e) {
      console.log(e);
    }
    return
  }
}
