import { Component, PLATFORM_ID, inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-kyc',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './kyc.html'
})
export class KycComponent {
  private readonly platformId = inject(PLATFORM_ID);

  form: any = {};
  customerId = isPlatformBrowser(this.platformId)
    ? localStorage.getItem('customerId')
    : null;

  constructor(private api: ApiService, private router: Router) {}

  submit() {
    this.api.submitKyc(this.customerId, this.form).subscribe(() => {
      this.router.navigate(['/loan']);
    });
  }
}
