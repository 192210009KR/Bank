import { Component, PLATFORM_ID, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './signup.html'
})
export class SignupComponent {
  private readonly platformId = inject(PLATFORM_ID);

  form: any = {};

  constructor(private api: ApiService, private router: Router) {}

  submit() {
    this.api.createCustomer(this.form).subscribe((res: any) => {
      if (isPlatformBrowser(this.platformId)) {
        localStorage.setItem('customerId', res.customerId);
      }

      this.router.navigate(['/kyc']);
    });
  }
}
