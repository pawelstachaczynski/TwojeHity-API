import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { AddNewSongComponent } from './user-panel/add-new-song/add-new-song.component';
import { BrowseAllComponent } from './user-panel/browse-all/browse-all.component';
import { LoginComponent } from './user-panel/login/login.component';
import { RegisterComponent } from './user-panel/register/register.component';
import { UserPanelMainPageComponent } from './user-panel/user-panel-main-page/user-panel-main-page.component';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { YourFavoriteComponent } from './user-panel/your-favorite/your-favorite.component';

const routes: Routes = [
  {
    path: '', component: UserPanelComponent, children: [
      { path: '', component: UserPanelMainPageComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'all', component: BrowseAllComponent, canActivate: [AuthGuard] },
      { path: 'add-new-song', component: AddNewSongComponent, canActivate: [AuthGuard]},
      {
        path: 'your-favorite', component: YourFavoriteComponent, canActivate: [AuthGuard], children: [
          { path: ':id', component: YourFavoriteComponent }
        ]
      }
    ]
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
