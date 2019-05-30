import { TestBed } from '@angular/core/testing';

import { ListPageService } from './list-page.service';

describe('ListPageService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ListPageService = TestBed.get(ListPageService);
    expect(service).toBeTruthy();
  });
});
