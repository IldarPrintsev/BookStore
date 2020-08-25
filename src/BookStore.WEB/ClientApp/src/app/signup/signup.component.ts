import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from "@angular/router";
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'signup',
  templateUrl: './signup.component.html',
  providers: [UserService]
})
export class SignUpComponent {
  invalidLogin: boolean;

  constructor(private router: Router, private userService: UserService) { }

  async signUp(form: NgForm) {
    this.invalidLogin = await this.userService.signUp(form);

    if (this.invalidLogin === false) {
      this.router.navigate(["/"]);
    }
  }
}
