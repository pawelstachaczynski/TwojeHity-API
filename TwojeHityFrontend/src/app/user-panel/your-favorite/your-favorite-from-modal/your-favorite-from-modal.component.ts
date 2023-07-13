import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Song } from 'src/app/models/song.model';
import { SongService } from 'src/app/models/song.service';
import { WindowService } from 'src/app/services/window.service';


@Component({
  selector: 'app-your-favorite-from-modal',
  templateUrl: './your-favorite-from-modal.component.html',
  styleUrls: ['./your-favorite-from-modal.component.scss']
})
export class YourFavoriteFromModalComponent implements OnInit {
  @Input() public opened: boolean;
  @Input() public isNew: boolean;
  @Input() public song: Song;
  public form: FormGroup = new FormGroup({
    id: new FormControl(),
    name: new FormControl()
  });

  constructor(private songService: SongService, private windowService: WindowService) { }

  ngOnInit(): void {

  }

  public close() {
    this.opened = false;
    this.windowService.closeWindow.next(false);
  }

  public open() {
    this.opened = true;
  }

  public async submit() {
    this.song = this.form.value;
    //await this.songService.createCategoryModalSaved.next(this.category);
    this.close();
  }
}
