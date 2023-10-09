import {Component, OnInit} from '@angular/core';
import {PasswordManagerService} from "./Service/password-manager.service";
import {PasswordUnitModel} from "../auth/Model/PasswordUnitModel";
import { Router } from '@angular/router';

@Component({
  selector: 'app-password-manager',
  templateUrl: './password-manager.component.html',
  styleUrls: ['./password-manager.component.scss']
})
export class PasswordManagerComponent implements OnInit{
  units: PasswordUnitModel[] = [];
  showPage: any = false;
  newUnit: PasswordUnitModel = { usernameAuth: 'none', masterPassword:  "none", website: '', username: '', password: '' };

  res: PasswordUnitModel[] = [];
  website: string | undefined;
  username: string | undefined;
  password: string | undefined;
  MasterPassword: string | undefined;
  webpageUsername: string | undefined;
  constructor(private unitService: PasswordManagerService, private router: Router) {}

  ngOnInit() {
    this.getUnits();
    //this.getUnits();
    let token =  localStorage.getItem('token');
    if(token){
      this.showPage = true
    }else{
      this.showPage = false
    }

    console.log("manager: ", this.showPage);
    this.timerForLogout();
  }


  timerForLogout(){
    setTimeout(() => {
        this.logout();
    }, 600000); //sign out after 10 min
}

  showUnits(){
    this.getUnits();
  }

  hideUnits(){
    location.reload();
  }


  getUnits(): void {
    let usernameFromLocal =  localStorage.getItem('username');
    let passewordFromLocal =  localStorage.getItem('password');
    let newUnit : PasswordUnitModel = { masterPassword: passewordFromLocal|| "none", usernameAuth: usernameFromLocal|| "none", website:"none", username:"none", password:"none"};


    this.unitService.getUnits(newUnit)
      .subscribe((createdUnit: PasswordUnitModel[]) => {
        console.log("createdUnit", createdUnit);
        console.log("userinfo", createdUnit)
        createdUnit
        this.res = createdUnit;
        //this.units.push(createdUnit);
      });

  }

  async addUnit() {
    this.newUnit = {
      usernameAuth: "none",
      masterPassword: "none",
      website: this.website || "none",
      username: this.webpageUsername || "none",
      password: this.password || "none"
    };
    this.units.push(this.newUnit);
    await this.createUnit();
    this.res = [];
    this.getUnits();
  }
  clearUnit() {
    this.website = '';
    this.username = '';
    this.webpageUsername = '';
    this.password = '';
    this.newUnit = { masterPassword: "none", website: '', usernameAuth: 'none', username: '', password: '' };
  }

  async createUnit(){
    let usernameFromLocal =  localStorage.getItem('username');
    let passewordFromLocal =  localStorage.getItem('password');
    const unit: PasswordUnitModel = { usernameAuth: usernameFromLocal|| "none", masterPassword: passewordFromLocal || "none", website: this.website || "none", username: this.webpageUsername || "none", password: this.password || "none" }; //materpassword: this.MasterPassword || "none"

    this.unitService.createUnit(unit)
      .subscribe(createdUnit => {

        this.units.push(createdUnit);
      });
  }


 logout(){
  localStorage.removeItem("token");
  this.router.navigateByUrl('/login');
  }

  editUnit(unitFromHtml: PasswordUnitModel): void {
    let usernameFromLocal =  localStorage.getItem('username');
    let passewordFromLocal =  localStorage.getItem('password');
    const unit: PasswordUnitModel = { id:unitFromHtml.id, usernameAuth: usernameFromLocal|| "none", masterPassword: passewordFromLocal || "none", website: unitFromHtml.website || "none", username: unitFromHtml.username || "none", password: unitFromHtml.password || "none" }; //materpassword: this.MasterPassword || "none"


    this.unitService.createUnit(unit)
      .subscribe(createdUnit => {

        this.units.push(createdUnit);
      });

    //reload the page to get new list
    this.router.navigateByUrl('/main');
  }

  deleteUnit(unit: PasswordUnitModel): void {
    this.unitService.deleteUniT(unit).subscribe();

    //reload the page to get new list
    location.reload();

  }

  generatePassword() {
    const length = 12;
    const charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    let password = "";
    for (let i = 0, n = charset.length; i < length; ++i) {
      password += charset.charAt(Math.floor(Math.random() * n));
    }
    //this.newUnit.password = password;
    this.password = password;


  }
}
