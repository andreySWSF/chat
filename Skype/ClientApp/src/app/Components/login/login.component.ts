import { Component, OnInit } from '@angular/core';
import { DataService } from '../../data.service';
import { User } from '../../User';
import { Routes } from '@angular/router';

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
  
  
  isValid: boolean = false;
  reportMessage: string = "";
  receivedUser: User; 
  done: boolean = false;
  constructor(private dataService: DataService) { }
  //submit(user: User) {
  //  this.dataService.postData(user)
  //    .subscribe(
  //      (data: User) => { this.receivedUser = data; this.done = true; },
  //      error => console.log(error)
  //    );
  //}
  checkUser(name: string, pass: string) {
    
    var nick = name;
    var password = pass;
    var user: User = new User(nick, password);
   
    this.dataService.postUserData(user).subscribe((data: boolean) => {
      this.isValid = data;
      if (this.isValid) {
        this.reportMessage = "User is valid";
        
      }
      else { this.reportMessage = "User not found, register please"; }
    });   
    
  }

  registerUser(name: string, pass: string, repeatPass: string) {

    var nick = name;
    var password = pass;
    var repeatPassword = repeatPass;
    
    if (password != repeatPassword) {
      this.reportMessage = "Your password incorrect";
      return;
    }
    else {
      var userOnRegistration: User = new User(nick, password);

      this.dataService.postUserData(userOnRegistration).subscribe((data: boolean) => {
        this.isValid = data;
        if (this.isValid) {
          this.reportMessage = "User allready exist";
        }
        else { this.reportMessage = "User not found, register please"; }
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
