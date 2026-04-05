import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { ApiService } from '../../services/api';

@Component({
  selector: 'app-offer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './offer.html'
})
export class OfferComponent implements OnInit {
  private readonly platformId = inject(PLATFORM_ID);

  offer: any;
  appId = isPlatformBrowser(this.platformId) ? localStorage.getItem('appId') : null;

  constructor(private api: ApiService) {}

  ngOnInit() {
    if (!isPlatformBrowser(this.platformId) || !this.appId) {
      return;
    }

    this.api.getOffers(this.appId).subscribe((res: any) => {
      this.offer = res;
    });
  }

  accept() {
    this.api.acceptOffer(this.offer.offerId).subscribe(() => {
      if (isPlatformBrowser(this.platformId) && this.offer?.loanId) {
        localStorage.setItem('loanId', this.offer.loanId);
      }

      alert('Loan Approved!');
    });
  }
}
