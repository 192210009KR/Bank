import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RepaymentComponent } from './repayment';

describe('Repayment', () => {
  let component: RepaymentComponent;
  let fixture: ComponentFixture<RepaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RepaymentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RepaymentComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
