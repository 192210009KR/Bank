import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl = 'https://localhost:7135/api';

  constructor(private http: HttpClient) {}

  createCustomer(data: any) {
    return this.http.post(`${this.baseUrl}/customers`, data);
  }

  submitKyc(customerId: any, data: any) {
    return this.http.post(`${this.baseUrl}/customers/${customerId}/kyc`, data);
  }

  applyLoan(data: any) {
    return this.http.post(`${this.baseUrl}/loans/applications`, data);
  }

  getOffers(appId: any) {
    return this.http.get(`${this.baseUrl}/applications/${appId}/offers`);
  }

  acceptOffer(offerId: any) {
    return this.http.post(`${this.baseUrl}/offers/${offerId}/accept`, {});
  }
  getLoanDetails(loanId: any) {
  return this.http.get(`${this.baseUrl}/loans/${loanId}`);
}

getRepaymentSchedule(loanId: any) {
  return this.http.get(`${this.baseUrl}/loans/${loanId}/schedule`);
}

makePayment(loanId: any, data: any) {
  return this.http.post(`${this.baseUrl}/loans/${loanId}/payments`, data);
}

getAllApplications() {
  return this.http.get(`${this.baseUrl}/admin/applications`);
}
login(data: any) {
  return this.http.post(`${this.baseUrl}/login`, data);
}
}
