import { EnvironmentProviders, importProvidersFrom } from '@angular/core';
import {
  MenuFoldOutline,
  MenuUnfoldOutline,
  FormOutline,
  DashboardOutline,
  UserOutline,
  ProfileOutline,
  FileOutline,
  BackwardOutline,
  LogoutOutline,
  SettingOutline,
  BellOutline,
  FileTextFill,
  MinusOutline,
  PlusOutline,
  AccountBookFill,
  FileTextOutline,
  EyeInvisibleFill,
  EyeInvisibleOutline,
  SearchOutline,
  EditOutline

} from '@ant-design/icons-angular/icons';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { matCircleOutline, matGridViewOutline, matSearchOutline } from '@ng-icons/material-icons/outline';
import { heroUserPlus } from '@ng-icons/heroicons/outline';

const icons = [MenuFoldOutline, 
  MenuUnfoldOutline,
  DashboardOutline, 
  FormOutline,
  UserOutline,
  ProfileOutline,
  FileOutline,
  BackwardOutline,
  LogoutOutline,
  SettingOutline,
  BellOutline,
  FileTextFill,
  MinusOutline,
  PlusOutline,
  FileTextOutline,
  EyeInvisibleOutline,
  SearchOutline,
  EditOutline
];

export function provideNzIcons(): EnvironmentProviders {
  return importProvidersFrom(NzIconModule.forRoot(icons));
}
