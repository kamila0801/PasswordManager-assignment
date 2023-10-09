import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {PasswordUnitModel} from "../../auth/Model/PasswordUnitModel";

@Injectable({
  providedIn: 'root'
})
export class PasswordManagerService {
  private apiUrl = 'https://localhost:5028/api';


  constructor(private http: HttpClient) {}

  getUnits(unit: PasswordUnitModel): Observable<PasswordUnitModel[]> { // masterpassword username
    const dto = {password: unit.masterPassword, username: unit.usernameAuth}
    const url = `${this.apiUrl}/PasswordManager/GetAllPasswordUnits`;
    console.log("get", unit);
    return this.http.post<PasswordUnitModel[]>(url, dto);
  }


  createUnit(unit: PasswordUnitModel): Observable<PasswordUnitModel> { // masterpassword all
    const url = `${this.apiUrl}/PasswordManager`;
    console.log("unit", unit);
    return this.http.post<PasswordUnitModel>(url, unit);
  }

  updateUnit(unit: PasswordUnitModel): Observable<PasswordUnitModel> {  // masterpassword all
    const url = `${this.apiUrl}/edit`;
    return this.http.put<PasswordUnitModel>(url, unit);
  }

  deleteUniT(unit: PasswordUnitModel): Observable<any> { // id and username
    const url = `${this.apiUrl}/PasswordManager/DeletePasswordUnit`;
    unit.usernameAuth = localStorage.getItem('username') || 'none'
    console.log("get", unit);
    return this.http.post<any>(url, unit);
  }



  generatePassword(): string {
    const length = 12;
    const chars = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+{}[];:<>,.?/';
    let password = '';
    for (let i = 0; i < length; i++) {
      const randomIndex = Math.floor(Math.random() * chars.length);
      password += chars.charAt(randomIndex);
    }
    return password;
  }
}
