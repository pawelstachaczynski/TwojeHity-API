import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { LoginComponent } from './user-panel/login/login.component';
import { UserNavigationComponent } from './user-panel/user-navigation/user-navigation.component';
import { UserPanelMainPageComponent } from './user-panel/user-panel-main-page/user-panel-main-page.component';
import { SpinnerComponent } from './shared/components/spinner/spinner.component';
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { IndicatorsModule } from "@progress/kendo-angular-indicators";
import { AuthService } from './services/auth.service';
import { AlertService } from './services/app-services/alert.service';
import { HttpClient, HttpClientModule, HttpHandler, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ConfigStore } from './app-config/config-store';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpErrorInterceptorService } from './services/interceptors/http-error-interceptor.service';
import { BrowseAllComponent } from './user-panel/browse-all/browse-all.component'; 
import { SongService } from './models/song.service';
import { AddNewSongComponent } from './user-panel/add-new-song/add-new-song.component';
import { YourFavoriteComponent } from './user-panel/your-favorite/your-favorite.component';
import { RegisterComponent } from './user-panel/register/register.component';
import { YourFavoriteFromModalComponent } from './user-panel/your-favorite/your-favorite-from-modal/your-favorite-from-modal.component';
import { WindowService } from './services/window.service';
import { AuthGuard } from './guards/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    UserPanelComponent,
    LoginComponent,
    UserNavigationComponent,
    UserPanelMainPageComponent,
    SpinnerComponent,
    BrowseAllComponent,
    AddNewSongComponent,
    YourFavoriteComponent,
    RegisterComponent,
    YourFavoriteFromModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    GridModule,
    BrowserAnimationsModule,
    IndicatorsModule,
    ReactiveFormsModule,
    ButtonsModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
    HttpClientModule
  ],
  providers: [WindowService, AuthGuard, AuthService, AlertService, HttpClient, ConfigStore, SongService, {provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptorService, multi:true}],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
