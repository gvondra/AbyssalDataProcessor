import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestMembershipComponent } from './request-membership.component';

describe('RequestMembershipComponent', () => {
  let component: RequestMembershipComponent;
  let fixture: ComponentFixture<RequestMembershipComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RequestMembershipComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestMembershipComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
