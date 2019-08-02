import { Component } from '@angular/core';
import { DataService } from '../../data.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
//import signalR = require('@aspnet/signalr');
import * as signalR from '@aspnet/signalr';
import { User } from '../../User';
import { Contact } from './Contact';
import { forEach } from '@angular/router/src/utils/collection';
import { debug } from 'util';
import { Connection } from './Connection';


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
  connections: Connection[] = [];
  contacts: Contact[] = [];
  done: boolean = false;
  isFriend: boolean = false;
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
    //var options = {
    //  //transport: TransportType.WebSockets,
    //  logging: signalR.LogLevel.Trace,
    //  accessToken: 
    //};

    //this.hubConnection = new signalR.HubConnectionBuilder()
    //  .withUrl('https://localhost:5001/chat/', {
    //    skipNegotiation: true,
    //    accessTokenFactory: () => localStorage.getItem("token")
    //  })// + '?token=' + localStorage.getItem("token"))
    //  .build();

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chat/', { accessTokenFactory: () => localStorage.getItem("token") })
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started!');
      })
      .catch(err => console.log('Error while establishing connection :('));

    //this.hubConnection.on("OnConnectedAsync", (data) => {
      
    //});

    this.hubConnection.on("SendChatMessage", (data) => {
      //debugger;
      this.recivedText = data;
      this.messages.push(this.recivedText);
    });

    this.hubConnection.on("SendFriendRequest", (data) => {
      this.recivedText = data;
      this.messages.push(this.recivedText);
    });
  }
  sendRequest(userAddName): string{
    this.hubConnection
      .invoke('SendFriendRequest', userAddName).then(res => {
        console.log(res);
      })
      .catch(err => console.error(err));
  }

  //getContactsFromDb(name: string, pass: string) {

  //  var user: User = new User(name, pass);

  //  this.dataService.post("Account/GetUsers", user).subscribe((userList) => {
      
  //  });

  //}

  public sendMessage(): void {
    this.hubConnection
      .invoke('SendChatMessage', this.message, 'user').then(res => {
        console.log(res);
      })
      .catch(err => console.error(err));
  }

  tryToSearch( query: string ) {

    if (query.length >= 3) {
      this.dataService.checkPost(query).subscribe((reqUsers: Contact[]) => {
        
        if (!reqUsers) { this.message = "Sorry, can't find user" }
        this.contacts = reqUsers;
      
      }); 
    }
  }

 
  //public addToFriendList(userAddId: string) {

  //  this.dataService.addFriendPost(userAddId).subscribe((request: boolean) => {

  //    this.isFriend = request;
  //  }); 

  //}
 
 

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

