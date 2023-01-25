import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-customers-list',
  templateUrl: './customers-list.component.html',
  styleUrls: ['./customers-list.component.css']
})
export class CustomersListComponent implements OnInit {
  customers: any[] = [];
  loading = false;
  newCustomer = { Name: '', CustomerNumber: '' };
  editingId = null;
  showAddCustomer = false;


  constructor(private http: HttpClient, private router: Router) { }

  async ngOnInit() {
    this.loading = true;
    try {
      const data = await this.http.get<any[]>('https://localhost:7245/Customers/').toPromise();
      this.customers = data ? data : [];
      console.log('this.customers', this.customers);
    } catch (error) {
      console.error(error);
    } finally {
      this.loading = false;
    }
  }

  async addCustomer() {
    try {
      await this.http.post('https://localhost:7245/Customers/', this.newCustomer).toPromise();
      console.log('Customer added successfully');
    } catch (error) {
      console.error(error);
    } finally {
      // Reset the form
      this.newCustomer = { Name: '', CustomerNumber: '' };
      this.ngOnInit();
    }
  }

  updateCustomer(editingId: any) {

  }

  cancelEditing() {
    this.editingId = null;
  }
}
