

export class AddSong{
    id: number;
   // rank: number;
    title: string;
    artist: string;
    album: string;
    year: number;

    constructor(//rank: number = null,
        title: string = null, artist: string = null, album: string = null, year: number = null) {
       // this.rank = rank;
        this.title = title;
        this.artist = artist;
        this.album = album;
        this.year = year;
    }
}