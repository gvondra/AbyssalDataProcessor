import { TestBed, inject } from '@angular/core/testing';

import { WebMetricsService } from './web-metrics.service';

describe('WebMetricsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WebMetricsService]
    });
  });

  it('should be created', inject([WebMetricsService], (service: WebMetricsService) => {
    expect(service).toBeTruthy();
  }));
});
