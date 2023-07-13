import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { ConfigStore } from 'src/app/app-config/config-store';
import { Login } from 'src/app/models/login.model';
import { Song } from 'src/app/models/song.model';
import { AlertService } from 'src/app/services/app-services/alert.service';
import { AuthService } from 'src/app/services/auth.service';
import { SongService } from 'src/app/models/song.service';
import { AddSong } from 'src/app/models/addSong.model';
import { Favorites } from 'src/app/models/favorites.model';
import { AddSongWithFavorite } from 'src/app/models/addSongWithFavorite.model';
import { AuthToken } from 'src/app/models/auth-token';

@Component({
  selector: 'app-add-new-song',
  templateUrl: './add-new-song.component.html',
  styleUrls: ['./add-new-song.component.scss']
})
export class AddNewSongComponent {
  isFavorite: string = "false";
  addToFavorite: Favorites;
  userId: number;
  private response;
/*   public newSongForm = new FormGroup( {
    title: new FormControl('',
    [
      Validators.required, 
      Validators.minLength(1), 
    ]),
    artist: new FormControl('', 
    [
      Validators.required, 
      Validators.minLength(1), 
    ]),
    album: new FormControl('', 
    [
      Validators.required, 
      Validators.minLength(1), 
    ]),
    year:  new FormControl<number>(2000),
    isFavorite: new FormControl('option2') //.setValue("false", {emitEvent: true})

  }); */

  public newSongForm : FormGroup = this.formBuilder.group ( {
    title: new FormControl('',
    [
      Validators.required, 
      Validators.minLength(1), 
    ]),
    artist: new FormControl('', 
    [
      Validators.required, 
      Validators.minLength(1), 
    ]),
    album: new FormControl('', 
    [
      Validators.required, 
      Validators.minLength(1), 
    ]),
    year:  new FormControl<number>(2000),
    isFavorite: new FormControl('option2') //.setValue("false", {emitEvent: true})

  });


  handleFavoriteChange(event: any) {
    this.isFavorite = event.target.value
return  event.target.value
   // event.target.value(console.log(this.value))
 }

  private song: AddSong
  private songFav: AddSongWithFavorite

   constructor(private formBuilder: FormBuilder, private router : Router, private authService : AuthService,  private songService: SongService, private configStore: ConfigStore,  private alertService: AlertService ) {}
 
  ngOnInit(): void {
    this.isFavorite = "false";
    this.newSongForm.setValue({
      isFavorite: "false",
      title: '',
      artist: '',
      album: '',
      year: 2023
    });
   
    const userData = JSON.parse(localStorage.getItem('userData'));
     this.userId = userData.id;
    
   }

   validateForm(): boolean {
    let form = document.querySelector('form') as HTMLFormElement;
    let inputs = form.getElementsByTagName('input');
    let isOk = true;

    for(let i = 0; i < inputs.length; i++)
    {
      inputs.item(i)?.dispatchEvent(new FocusEvent('focusout')); //wysyła zdarzenie do wykonania na elemencie
      if(inputs.item(i)?.className.includes('invalid'))
      {
        return isOk = false;
      }
    }

    return isOk = true;
    }
  async SaveNewSong() {
    if(!this.validateForm())
    {
      this.alertService.showError("Wprowadź dane we wszystkie pola")
      return;
    }
   this.addToFavorite = {
      UserId: this.userId
   }
    this.configStore.startLoadingPanel();
   if (this.isFavorite == "true")
  {
    this.songFav = new AddSongWithFavorite(this.newSongForm.value.title, this.newSongForm.value.artist, this.newSongForm.value.album, this.newSongForm.value.year, this.addToFavorite)
    this.response =  await lastValueFrom(this.songService.addNewWithFavorite(this.songFav))
     this.configStore.stopLoadingPanel();
     this.alertService.showSuccess("Dodano pomyślnie!");
     this.alertService.showSuccess("Dodano do ulubionych!");
   } 
     if (this.isFavorite.valueOf() == "false")
     {
      this.song = new AddSong(this.newSongForm.value.title, this.newSongForm.value.artist, this.newSongForm.value.album, this.newSongForm.value.year)
      
      this.response =  await lastValueFrom(this.songService.addNew(this.song))
       this.configStore.stopLoadingPanel();
       this.alertService.showSuccess("Dodano pomyślnie!");
      
     }

   }
   
}
