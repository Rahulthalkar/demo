import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NgIconComponent, NgIconsModule, provideIcons } from '@ng-icons/core';
import { featherAirplay } from '@ng-icons/feather-icons';
import { heroUsers } from '@ng-icons/heroicons/outline';
import { UserService } from '../service/userData.service';

@Component({
  selector: 'app-layout-default',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NzIconModule, NzLayoutModule, NzMenuModule,TranslateModule,
    NzDropDownModule,
    NzBreadCrumbModule,
    NgIconComponent,
    RouterLink,
  ],
  templateUrl: './layout-default.component.html',
  styleUrl: './layout-default.component.css'
})
export class LayoutDefaultComponent {
  isCollapsed = false;
  isSmallScreen!: boolean;
  constructor(public translateService: TranslateService,
    private userData:UserService,
    private router:Router
  ) {
    this.translateService.setDefaultLang('en')
  }
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
  Logout(){
    this.userData.logout();
    sessionStorage.clear();
    this.router.navigate(['login']);
  }
}
