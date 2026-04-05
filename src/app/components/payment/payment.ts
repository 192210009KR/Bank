import { isPlatformBrowser } from '@angular/common';
import { Component, PLATFORM_ID, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './payment.html'
})
export class PaymentComponent {
  private readonly platformId = inject(PLATFORM_ID);

  form: any = {};
  loanId = isPlatformBrowser(this.platformId) ? localStorage.getItem('loanId') : null;

  constructor(private api: ApiService) {}

  pay() {
    this.api.makePayment(this.loanId, this.form).subscribe(() => {
      alert("Payment Successful");
    });
  }
}
