import { Routes } from '@angular/router';

import { SignupComponent } from './components/signup/signup';
import { KycComponent } from './components/kyc/kyc';
import { LoanApplication } from './components/loan-application/loan-application';
import { OfferComponent } from './components/offer/offer';

// 🔥 ADD THESE IMPORTS
import { DashboardComponent } from './components/dashboard/dashboard';
import { RepaymentComponent } from './components/repayment/repayment';
import { PaymentComponent } from './components/payment/payment';
import { AdminComponent } from './components/admin/admin';
import { LoginComponent } from './components/login/login';
export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'kyc', component: KycComponent },
  { path: 'loan', component: LoanApplication },
  { path: 'offer', component: OfferComponent },

  // 🔥 DEV 2 ROUTEs
  { path: 'dashboard', component: DashboardComponent },
  { path: 'repayment', component: RepaymentComponent },
  { path: 'payment', component: PaymentComponent },
  { path: 'admin', component: AdminComponent }
];