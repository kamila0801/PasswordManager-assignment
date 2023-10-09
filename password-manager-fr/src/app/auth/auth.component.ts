import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {AuthService} from "./service/auth.service";
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {
  isLoginMode = true;
  username: string | undefined;
  password: string | undefined;
  confirmPassword: string | undefined;


  constructor(private authService: AuthService, private router: Router) { }

  onInit(){

    const loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl(['', Validators.required, Validators.minLength(5)])
  });
  }

  onSubmit() {
    console.log(this.username);
    console.log(this.password);
    if (this.isLoginMode) {
      this.authService.login(this.username, this.password)
        .subscribe(
          (result: any) => {
            if (result.jwt) {
              console.log("result", result);
              this.router.navigate(['/main']);
            }
            else {
              alert(result.message);
            }
          },
          (error: any) => console.log(error)
        );
    } else {
      this.authService.register(this.username, this.password, this.confirmPassword)
        .subscribe(
          (result: any) => {
            if (result) {
              this.authService.login(this.username, this.password).subscribe(res => {
                  if (res.jwt) {
                    console.log("result", res);
                    this.router.navigate(['/main']);
                  }
                  else {
                    alert(result.message);
                  }
                },
                (error: any) => console.log(error)
              );
            }
          },
          (error: any) => console.log(error)
        );
    }
  }


  onSwitchMode() {
    this.isLoginMode = !this.isLoginMode;
  }
}
