import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';
import { User } from '../../User';
import { Routes, Router } from '@angular/router';
import { SkypeWindowComponent } from '../skype-window/skype-window.component';
import { HttpHeaders } from '@angular/common/http';

//class User
//{ 
//  nickName: string;
//  password: string;
//  constructor(nickName: string, password: string)
//  {
//    this.nickName = nickName;
//    this.password = password;
    
//  }
//}

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
   providers: [DataService]
})

//const routes: Routes = [
//  { path: '', redirectTo: '/AppComponent', pathMatch: 'full' },
//  { path: 'skype', component: skype-window },

//];

export class LoginComponent { 
  
  tokenKey = "accessToken";
  isValid: boolean = false;
  reportMessage: string = "";
  //receivedUser: User; 
  done: boolean = false;

  
  constructor(private dataService: DataService, private router: Router) {
  }
  
  skype: SkypeWindowComponent = new SkypeWindowComponent(this.dataService);
 

  goToItem(skype: SkypeWindowComponent) {

    this.router.navigate(
      ['/skype-window']
    );
  }

 
  loginUser(name: string, pass: string) {    
    
    var user: User = new User(name, pass);
   
    this.dataService.post("Account/LoginByToken", user).subscribe((obj) => {
      this.isValid = (obj!=null)?true:false;
      localStorage.setItem('token', obj['access_token']);
      if (this.isValid) {

        this.reportMessage = "User is valid";

        this.goToItem(this.skype);
      }
      else { this.reportMessage = "User not found, register please"; }
    });   
  }

  registerUser(name: string, pass: string, repeatPass: string) {
            
    if (pass != repeatPass) {
      this.reportMessage = "Your password incorrect";
      return;
    }
    else {
      var userOnRegistration: User = new User(name, pass);

      this.dataService.post('Account/Register', userOnRegistration).subscribe((data: boolean) => {
        this.isValid = data;
        if (this.isValid) {
          this.reportMessage = "User allready exist";
        }
        else { this.reportMessage = "You have been registered successfully"; }
      });
    }

  }

  reportHandler() {

  }

  //getValueFromInput(id) {
  //  var value;
  //}
  //ngOnInit() {
  //}

}
