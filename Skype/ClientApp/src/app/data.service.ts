import { HttpClient } from '@angular/common/http';
import { User } from './User';
import { Injectable } from '@angular/core';

@Injectable()
export class DataService {

  private url = "/api/products";

  constructor(private http: HttpClient) {

  }
 
  getUser() {
    return this.http.get(this.url);
  }
  postUserData(user: User) {

    const body = { nickName: user.nickName, password: user.password };
    return this.http.post('https://localhost:5001/api/User/PostUserResult', body);
  }
  
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
