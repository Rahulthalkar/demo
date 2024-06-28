import { TestBed } from '@angular/core/testing';

import { GridDetailsService } from './grid-details.service';

describe('GridDetailsService', () => {
  let service: GridDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GridDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
