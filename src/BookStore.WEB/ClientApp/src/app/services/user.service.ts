import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class UserService {

  private url = "/api/account";

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService, ) { }

  signIn(form: NgForm): Promise<boolean> {
    const credentials = JSON.stringify(form.value);

    return this.http.post(this.url + "/signin", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).toPromise()
      .then(response => {
        const token = (<any>response).token;
        localStorage.setItem("jwt", token);

        return false;
      }).catch(err => {

        return true;
      });
  }

  signUp(form: NgForm): Promise<boolean> {
    const credentials = JSON.stringify(form.value);

    return this.http.post(this.url + "/signup", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).toPromise()
      .then(response => {
        const token = (<any>response).token;
        localStorage.setItem("jwt", token);

        return false;
      }).catch(err => {

        return true;
      });
  }

  isUserAuthenticated() {
    const token: string = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }
}
