import { Component, PLATFORM_ID, inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loan-application',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './loan-application.html'
})
export class LoanApplication {
  private readonly platformId = inject(PLATFORM_ID);

  form: any = {};

  constructor(private api: ApiService, private router: Router) {}

  submit() {
    this.api.applyLoan(this.form).subscribe((res: any) => {
      if (isPlatformBrowser(this.platformId)) {
        localStorage.setItem('appId', res.applicationId);
      }

      this.router.navigate(['/offer']);
    });
  }
}
