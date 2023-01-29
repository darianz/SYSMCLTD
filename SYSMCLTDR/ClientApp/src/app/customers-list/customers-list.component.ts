import { Component, OnInit } from '@angular/core';
import { CustomersService } from '../services/CustomersService';

@Component({
  selector: 'app-customers-list',
  templateUrl: './customers-list.component.html',
  styleUrls: ['./customers-list.component.css']
})
export class CustomersListComponent implements OnInit {
  customers?: any[] = [];
  loading = false;
  newCustomer = { Name: '', CustomerNumber: '' };
  editingId = null;
  showAddCustomer = false;

  constructor(private customersService: CustomersService) { }

  async ngOnInit() {
    this.loading = true;
    this.customers = await this.customersService.getCustomers();
    this.loading = false;
  }

  async addCustomer() {
    await this.customersService.addCustomer(this.newCustomer);
    // Reset the form
    this.newCustomer = { Name: '', CustomerNumber: '' };
    this.ngOnInit();
  }

 

  async updateCustomer(customerNumber: any, name: any) {
    await this.customersService.updateCustomer(customerNumber, name).then(
      data => {
        alert('Customer updated successfully');
        this.cancelEditing();
      },
      error => {
        alert('Error updating customer');
        this.cancelEditing();
      }
    );
  }

  async deleteCustomer(customerNumber: any) {
    await this.customersService.deleteCustomer(customerNumber).then(
      data => {
        alert('Customer deleted successfully');
        this.ngOnInit();
      },
      error => {
        alert('Error deleted customer');
        this.ngOnInit();
      }
    );
  }

  cancelEditing() {
    this.editingId = null;
  }
}
