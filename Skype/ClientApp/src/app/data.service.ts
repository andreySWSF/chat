import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from './User';
import { Injectable } from '@angular/core';

@Injectable()
export class DataService {

  private url = "/api/products";
  
  headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': "Bearer " + localStorage.getItem("token")
  });

  //User userModel;

  constructor(private http: HttpClient) {

  }

  
  getUser() {
    return this.http.get(this.url);
  }
  post(url: string, body: any) {
    
    //const body = { nickName: user.nickName, password: user.password };

    if (!localStorage.getItem("token")) {
      return this.http.post('https://localhost:5001/api/' + url, body);
    }

    return this.http.post('https://localhost:5001/api/' + url, body, { headers: this.headers });
  }
 
  checkPost(search: string) {
    var body = { query: search };
    return this.http.post('https://localhost:5001/api/User/SearchUser', body);
  }

  addFriendPost(userAddId: string) {
    var body = { query: userAddId };
    return this.http.post('https://localhost:5001/api/User/SearchUser', body);
  }

}
