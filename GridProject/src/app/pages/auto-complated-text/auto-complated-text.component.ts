import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NzAutocompleteModule } from 'ng-zorro-antd/auto-complete';
@Component({
  selector: 'app-auto-complated-text',
  standalone: true,
  imports: [NzAutocompleteModule,ReactiveFormsModule],
  templateUrl: './auto-complated-text.component.html',
  styleUrl: './auto-complated-text.component.css'
})
export class AutoComplatedTextComponent {

  inputValue?: string;
  options: string[] = [];

  onInput(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.options = value ? [value, value + value, value + value + value] : [];
  }
}
