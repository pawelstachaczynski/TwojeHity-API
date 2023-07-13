import { Injectable } from "@angular/core";
import { AppConfig } from "../app-config/app.config";
import { HttpClient } from "@angular/common/http";
import { AlertService } from "./app-services/alert.service";
import { Router } from "@angular/router";
import { BehaviorSubject } from "rxjs";
import { User } from "../models/user.model";
import { RegisterUser } from "../models/register-user";
import { Login } from "../models/login.model";
import { AuthToken } from "../models/auth-token";

@Injectable()
export class AuthService{
    public user: BehaviorSubject<User> = new BehaviorSubject<User>(null);
    private serverUrl: string = AppConfig.APP_URL;
    private apiUrl: string = `${this.serverUrl}api/account/`;
    private tokenExpirationTimer: any;

    constructor(private http: HttpClient, private alertService: AlertService, private router: Router) {
        
    }

    signup(newUser: RegisterUser) {
    return this.http.post<RegisterUser>(`${this.apiUrl}register`, newUser)
    }

    logIn(login: Login)
    {
       return this.http.post<AuthToken>(`${this.apiUrl}login`, login);
    }

    public handleAuthentication(login: string, userId: string, token: string, tokenExpirationDate: Date){
        const user = new User(login, userId, token, tokenExpirationDate);
        this.user.next(user);
        const expirationDuration = new Date(tokenExpirationDate).getTime() - new Date().getTime();
        this.autoLogOut(expirationDuration);
        localStorage.setItem("userData", JSON.stringify(user));

    }

    autoLogOut(expirationDuration : number) {
        this.tokenExpirationTimer = setTimeout(() => {
            this.logout();
        }, expirationDuration)
    }

    logout() {
        this.user.next(null);
        this.alertService.showInfo("Wylogowano");
        this.router.navigate(['']);
        localStorage.removeItem('userData');
        if(this.tokenExpirationTimer) {
            clearTimeout(this.tokenExpirationTimer)
        }
        this.tokenExpirationTimer = null;
    }
}