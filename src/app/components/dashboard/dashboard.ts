import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { ApiService } from '../../services/api';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html'
})
export class DashboardComponent implements OnInit {
  private readonly platformId = inject(PLATFORM_ID);

  loan: any;

  constructor(private api: ApiService) {}

  ngOnInit() {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const loanId = localStorage.getItem('loanId');
    if (!loanId) {
      return;
    }

    this.api.getLoanDetails(loanId).subscribe((res: any) => {
      this.loan = res;
    });
  }
}
