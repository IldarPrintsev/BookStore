import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from "@angular/router";
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'signin',
  templateUrl: './signin.component.html',
  providers: [UserService]
})
export class SignInComponent {
  invalidLogin: boolean;

  constructor(private router: Router, private userService: UserService) { }

  async signIn(form: NgForm) {
    this.invalidLogin = await this.userService.signIn(form);
    if (this.invalidLogin === false) {
      this.router.navigate(["/"]);
    }
  }
}
