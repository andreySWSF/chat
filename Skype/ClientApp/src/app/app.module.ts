import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './Components/nav-menu/nav-menu.component';
import { HomeComponent } from './Components/home/home.component';
import { CounterComponent } from './Components/counter/counter.component';
import { FetchDataComponent } from './Components/fetch-data/fetch-data.component';
import { LoginComponent } from './Components/login/login.component';
import { SkypeWindowComponent } from './Components/skype-window/skype-window.component';
import { SkypeWindowGuard } from './Components/skype-window.guard';

//const appRoutes: Routes = [
//  { path: '', component: HomeComponent },
//  { path: 'skype-window', component: SkypeWindowComponent, canActivate: [SkypeWindowGuard] },
//  { path: '**', redirectTo: '/' }

//];


@NgModule({
  declarations: [
   
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    SkypeWindowComponent
  ],
  imports: [
    BrowserModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'skype-window', component: SkypeWindowComponent, canActivate: [SkypeWindowGuard] },
      { path: 'login', component: LoginComponent }
    ])
  ],
  providers: [SkypeWindowGuard],
  bootstrap: [AppComponent],
  exports: [BsDropdownModule, TooltipModule, ModalModule]
})
export class AppModule { }
//export class AppBootstrapModule { }
