import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NzIconModule, NzLayoutModule, NzMenuModule,
  RouterLink
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isCollapsed = false;

  isSmallScreen!: boolean;
  // constructor(public translateService: TranslateService) {
  //   this.translateService.setDefaultLang('en')
  // }
  onResize(event: Event): void {
    this.checkScreenWidth();
  }
  private checkScreenWidth(): void {
    this.isSmallScreen = window.innerWidth <= 1023;
  }
  toggleChange() {
    this.isCollapsed = !this.isCollapsed;

  }
  collapseSidebar() {
    this.isCollapsed = true;
  }
}
