import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Component, OnInit, PLATFORM_ID, inject } from '@angular/core';
import { ApiService } from '../../services/api';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin.html'
})
export class AdminComponent implements OnInit {
  private readonly platformId = inject(PLATFORM_ID);

  applications: any[] = [];

  constructor(private api: ApiService) {}

  ngOnInit() {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    this.api.getAllApplications().subscribe((res: any) => {
      this.applications = res;
    });
  }
}
