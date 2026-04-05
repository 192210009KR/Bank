import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, PLATFORM_ID, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ApiService } from '../../services/api';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html'
})
export class LoginComponent {
  private readonly platformId = inject(PLATFORM_ID);

  form: any = {};
  errorMessage = '';

  constructor(private api: ApiService, private router: Router) {}

  login() {
    this.errorMessage = '';

    if (!this.form.email || !this.form.password) {
      this.errorMessage = 'Enter both email and password.';
      return;
    }

    this.api.login(this.form).subscribe({
      next: (res: any) => {
        if (isPlatformBrowser(this.platformId) && res?.customerId) {
          localStorage.setItem('customerId', res.customerId);
        }

        alert('Login Successful');
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        this.errorMessage = 'Login failed. Check your email and password.';
      }
    });
  }
}
