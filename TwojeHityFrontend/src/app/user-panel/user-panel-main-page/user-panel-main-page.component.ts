
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ConfigStore } from 'src/app/app-config/config-store';
import { AuthInfo } from 'src/app/models/auth-info.model';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-user-panel-main-page',
  templateUrl: './user-panel-main-page.component.html',
  styleUrls: ['./user-panel-main-page.component.scss']
})
export class UserPanelMainPageComponent implements OnInit, OnDestroy{
  public authInfo : AuthInfo;
  public userSub : Subscription;

  
constructor(private authService: AuthService, private configStore: ConfigStore) {}

ngOnInit(): void {
  this.userSub =  this.authService.user.subscribe(user => {
      this.authInfo = new AuthInfo(user);
    })
}

ngOnDestroy(): void {
  
}

logout() {
  this.authService.logout();
}
}



