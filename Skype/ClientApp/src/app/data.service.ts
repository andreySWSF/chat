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
    return this.http.post('https://localhost:5001/api/User/SearchUser', search);
  }

  //postUserJoin(user: User) {

  //  const body = { nickName: user.nickName, password: user.password };
  //  return this.http.post('https://localhost:5001/api/Account/Register', body);
  //}
  //createUser(user: User) {
  //  return this.http.post(this.url, user);
  //}
  //updateProduct(user: User) {

  //  return this.http.put(this.url + '/' + user.id, user);
  //}
  //deleteUser(id: number) {
  //  return this.http.delete(this.url + '/' + id);
  //}
}
