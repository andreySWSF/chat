
import { Component } from '@angular/core';
import { DataService } from './data.service';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { User } from './user';
//import * as signalR from '@aspnet/signalr';
//import signalR = require('@aspnet/signalr');

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [DataService]
})
export class AppComponent {
   
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
    
  }
  
  //public sendMessage(): void {
  //  this.hubConnection
  //    .invoke('Send', this.message).then(res => {
  //      console.log(res);
  //    })
  //    .catch(err => console.error(err));
  //  }
    
}
