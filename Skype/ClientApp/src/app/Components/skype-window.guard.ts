import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
@Injectable()
export class SkypeWindowGuard implements CanActivate {
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {

    return confirm('Вы уверены, что хотите перейти?');
    }

  //canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {

    //constructor(public router: Router) { }

    //canActivate(): boolean {

    //  if (sessionStorage.getItem('id') == null) {
    //    this.router.navigate(['home']);
    //    return false;
    //  }
    //  return true;
    //}
    
  //}
}
