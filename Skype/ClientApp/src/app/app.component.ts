//import { Component } from '@angular/core';
import { Component } from '@angular/core';
import { DataService } from './data.service';
import { User } from './user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [DataService]
})
export class AppComponent {
  //user: User = new User("yjjy","htj");  
  users: User[];               
  //tableMode: boolean = true;          // табличный режим


  constructor(private dataService: DataService) { }

  //save() {
  //  if (this.name == null) {
  //    this.dataService.createUser(this.user)
  //      .subscribe((data: User) => this.users.push(data));
  //  } else {
  //    this.dataService.updateUser(this.user);
  //  }
  //  this.cancel();
  //}
  //editUser(u: User) {
  //  this.user = u;
  //}
  //cancel() {
  //  this.user = new User();
  //  this.tableMode = true;
  //}
  //delete(u: User) {
  //  this.dataService.deleteUser(u.nickName)
  //    .subscribe(data => this.loadUserss());
  //}
  //add() {
  //  this.cancel();
  //  this.tableMode = false;
  //}
}
