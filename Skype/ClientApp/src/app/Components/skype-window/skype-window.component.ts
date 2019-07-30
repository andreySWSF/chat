import { Component } from '@angular/core';
import { DataService } from '../../data.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
//import signalR = require('@aspnet/signalr');
import * as signalR from '@aspnet/signalr';
import { User } from '../../User';
import { Contact } from './Contact';
import { forEach } from '@angular/router/src/utils/collection';
import { debug } from 'util';


@Component({
  selector: 'skype-window',
  templateUrl: './skype-window.component.html',
  styleUrls: ['./skype-window.component.css'],
  providers: [DataService]
})

export class SkypeWindowComponent {

  hello: string = "";
 // reqObq: object = { values = '' ;};
  receivedUser: User; // полученный пользователь
  //contact = { id: "", name: "" }
  contacts: Contact[] = [];
  done: boolean = false;
  private hubConnection: HubConnection;
  nick = '';
  message = '';
  info = '';
  contact: Contact;
  searchResult: any[] = [];
  recivedText = "";
  token = localStorage.getItem("token");

  messages: string[] = [];
  data: any;

  constructor(private dataService: DataService) { }

  ngOnInit() {
   

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chat/', { accessTokenFactory: () => localStorage.getItem("token") })
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started!');
      })
      .catch(err => console.log('Error while establishing connection :('));

    this.hubConnection.on("Receive", (data) => {
      //debugger;
      this.recivedText = data;
      this.messages.push(this.recivedText);
    });

    //this.hubConnection.on("Receive", (data, user) => {
      
    //  this.recivedText = data;
    //  //this.messages.push(this.recivedText);
    //});
  }


  //getContactsFromDb(name: string, pass: string) {

  //  var user: User = new User(name, pass);

  //  this.dataService.post("Account/GetUsers", user).subscribe((userList) => {
      
  //  });

  //}

  tryToSearch( query: string ) {

    if (query.length >= 3) {
      this.dataService.checkPost(query).subscribe((reqUsers: Contact[]) => {
        
        if (!reqUsers) { this.message = "Sorry, can't find user" }
        this.contacts = reqUsers;
      
      }); 
    }
   
  }
  public addToFriendList(userAddId: string) {

    this.dataService.addFriendPost(userAddId).subscribe((request) => {


    }); 

  }
  public sendMessage(): void {
    this.hubConnection
      .invoke('Send', this.message, 'user' ).then(res => {
        console.log(res);
      })
      .catch(err => console.error(err));
  }
 

  reportHandler() {

  }


}

    //this.hubConnection.on('Send', (nick: string, receivedMessage: string) => {
    //  const text = `${nick}: ${receivedMessage}`;
    //  this.messages.push(text);
    //});


  //getValueFromInput(id) {
  //  var value;
  //}
  //ngOnInit() {
  //}

 //checkUser(name: string, pass: string) {

  //  var nick = name;
  //  var password = pass;
  //  var user: User = new User(nick, password);

  //  this.dataService.postData(user).subscribe((data: boolean) => {
  //    this.hello = "dfdf";

  //  });

  //}

