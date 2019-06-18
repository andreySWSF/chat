
import { Component } from '@angular/core';
import { DataService } from './data.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { User } from './user';
//import signalR = require('@aspnet/signalr');

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
  private hubConnection: HubConnection;
  nick = '';
  message = '';
  recivedText = "";
  
  messages: string[] = [];
    data: any;


  constructor(private dataService: DataService) { }

  ngOnInit() {
    //this.nick = window.prompt('Your name:', 'John');

    //this.hubConnection = new signalR.HubConnectionBuilder()
    //  .withUrl('https://localhost:5001/chat')
    //  .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    //this.hubConnection.on('Send', (nick: string, receivedMessage: string) => {
    //  const text = `${nick}: ${receivedMessage}`;
    //  this.messages.push(text);
    //});
    this.hubConnection.on("Send", data => {
      this.recivedText = data;
      this.messages.push(this.recivedText);
    });
  }
  
  public sendMessage(): void {
    this.hubConnection
      .invoke('Send', this.message).then(res => {
        console.log(res);
      })
      .catch(err => console.error(err));
    }
    
}
