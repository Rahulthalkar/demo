import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutoComplatedTextComponent } from './auto-complated-text.component';

describe('AutoComplatedTextComponent', () => {
  let component: AutoComplatedTextComponent;
  let fixture: ComponentFixture<AutoComplatedTextComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AutoComplatedTextComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AutoComplatedTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
