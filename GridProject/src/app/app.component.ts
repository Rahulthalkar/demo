import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { NZ_ICONS, NzIconModule } from 'ng-zorro-antd/icon';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { EmployeeComponent } from './pages/employee/employee.component';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NgIconComponent, NgIconsModule, provideIcons } from '@ng-icons/core';
import { featherAirplay } from '@ng-icons/feather-icons';
import { heroUsers } from '@ng-icons/heroicons/outline';
import { UserService } from './service/userData.service';
import { matCircleOutline, matGridViewOutline } from '@ng-icons/material-icons/outline';
import { FileTextOutline, SettingOutline } from '@ant-design/icons-angular/icons';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, NzLayoutModule, NzMenuModule,EmployeeComponent,TranslateModule,
  NzDropDownModule,
  NzBreadCrumbModule,
  NgIconComponent,
  RouterLink,
  NzIconModule,
  NgIconComponent
],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  viewProviders: [provideIcons({ featherAirplay,heroUsers})],
  providers: [provideIcons({ featherAirplay, heroUsers,matCircleOutline,})],
  
})
export class AppComponent {

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
