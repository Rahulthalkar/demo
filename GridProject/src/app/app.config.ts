import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideNzIcons } from './icons-provider';
import { en_US, provideNzI18n } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormsModule } from '@angular/forms';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgIconComponent, provideIcons, provideNgIconsConfig, withContentSecurityPolicy } from '@ng-icons/core';
registerLocaleData(en);

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideNzIcons(), provideNzI18n(en_US), importProvidersFrom(FormsModule), provideAnimationsAsync(), provideHttpClient(),
  importProvidersFrom(
  TranslateModule.forRoot({
    loader:{
      provide:TranslateLoader,
      useFactory:httpTranslateLoader,
      deps:[HttpClient]
    },
  }),
  ),
  provideNgIconsConfig({
    size: '1.5em',
  },
  withContentSecurityPolicy()),
],
  
} 

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http,'./assets/i18n/', '.json');
}
