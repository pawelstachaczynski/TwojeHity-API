import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfig } from "../app-config/app.config";
import { AlertService } from "../services/app-services/alert.service";
import { AuthService } from "../services/auth.service";
import { AddSong } from "./addSong.model";
import { AddSongWithFavorite } from "./addSongWithFavorite.model";
import { getFavorites } from "./getFavorites.model";
import { Song } from "./song.model";
import { User } from "./user.model";

@Injectable()
export class SongService{

    private serverUrl: string = AppConfig.APP_URL;
    private apiUrl: string = `${this.serverUrl}api/song/`;

    constructor(private httpClient: HttpClient, private alertService: AlertService, private authService: AuthService)
    {

    }

    getAll() {
        return this.httpClient.get<Song[]>(`${this.apiUrl}`)
    }

    addNew(song: AddSong) {

        return this.httpClient.post<Song>(`${this.apiUrl}`,song)
    }

    addNewWithFavorite(song: AddSongWithFavorite) {

        return this.httpClient.post<Song>(`${this.apiUrl}new-with-favorite`,song)
    }

    getAllFavorites(userId) {
    
        return this.httpClient.get<Song[]>(`${this.apiUrl}your-favorite/`+userId)
    }

    deleteSong(songId)
    {
        return this.httpClient.post<boolean>(`${this.apiUrl}your-favorite-delete/`, songId)
    }
}