import { TestBed, inject } from '@angular/core/testing';

import { FirstContactService } from './first-contact.service';

describe('FirstContactService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FirstContactService]
    });
  });

  it('should be created', inject([FirstContactService], (service: FirstContactService) => {
    expect(service).toBeTruthy();
  }));
});
