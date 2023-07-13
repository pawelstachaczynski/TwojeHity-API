import { Favorites } from "./favorites.model";


export class AddSongWithFavorite{
    id: number;
    title: string;
    artist: string;
    album: string;
    year: number;
    favorites: Favorites

    constructor( title: string = null, artist: string = null, album: string = null, year: number = null, favorites: Favorites = null){
        this.title = title;
        this.artist = artist;
        this.album = album;
        this.year = year;
        this.favorites = favorites;
        
    }
}