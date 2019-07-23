import { Component } from '@angular/core';
import { DataService } from '../../data.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
//import signalR = require('@aspnet/signalr');
import * as signalR from '@aspnet/signalr';
import { User } from '../../User';

@Component({
  selector: 'chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  providers: [DataService]
})

export class Chat {

  hello: string = "";

  receivedUser: User; // полученный пользователь
  done: boolean = false;
  private hubConnection: HubConnection;
  nick = '';
  message = '';
  recivedText = "";

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
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this.hubConnection.on("Send", (data) => {
      this.recivedText = data;
      this.messages.push(this.recivedText);
    });
  }
  getContactsFromDb(name: string, pass: string) {

    var user: User = new User(name, pass);

    this.dataService.post("Account/GetUsers", user).subscribe((userList) => {

    });

  }
  public sendMessage(): void {
    this.hubConnection
      .invoke('Send', this.message, 'user').then(res => {
        console.log(res);
      })
      .catch(err => console.error(err));
  }


  reportHandler() {

  }


}
