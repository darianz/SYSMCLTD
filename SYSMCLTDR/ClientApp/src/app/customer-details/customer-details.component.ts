import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
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


  constructor(private http: HttpClient, private route: ActivatedRoute) { }

  async ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    const baseUrl = "https://localhost:7245/Customers/fullinfo/";
    await this.http.get(baseUrl + id)
      .subscribe((data: any )=> {
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
