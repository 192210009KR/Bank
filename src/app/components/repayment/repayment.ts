import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { ApiService } from '../../services/api';

@Component({
  selector: 'app-repayment',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './repayment.html'
})
export class RepaymentComponent implements OnInit {
  private readonly platformId = inject(PLATFORM_ID);

  schedule: any[] = [];

  constructor(private api: ApiService) {}

  ngOnInit() {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const loanId = localStorage.getItem('loanId');
    if (!loanId) {
      return;
    }

    this.api.getRepaymentSchedule(loanId).subscribe((res: any) => {
      this.schedule = res;
    });
  }
}
