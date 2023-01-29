import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from '../services/CustomerService';
@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css'],
})
export class CustomerDetailsComponent implements OnInit {
  customer: any;
  addresses: any[] = [];
  contacts: any[] = [];
  hasAddresses = false;
  hasContacts = false;


  constructor( private route: ActivatedRoute,private customerService: CustomerService) { }

  async ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id !== null) {
      await this.customerService.getCustomerDetails(id)
        .then((data: any) => {
          if (Object.keys(data).includes('customer')) {
            this.customer = data.customer;
          }
          if (Object.keys(data).includes('addresses')) {
            this.addresses = data.addresses;
            if (data.addresses.length > 0) this.hasAddresses = true;
          }
          if (Object.keys(data).includes('contacts')) {
            this.contacts = data.contacts;
            if (data.contacts.length > 0) this.hasContacts = true;
          }
        });
    }
  
  }

 


}
