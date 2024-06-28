import { TestBed } from '@angular/core/testing';

import { GriddetailgridService } from './griddetailgrid.service';

describe('GriddetailgridService', () => {
  let service: GriddetailgridService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GriddetailgridService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
