import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { lastValueFrom, Subscription } from 'rxjs';
import { ConfigStore } from 'src/app/app-config/config-store';
import { Song } from 'src/app/models/song.model';
import { SongService } from 'src/app/models/song.service';
import { process } from "@progress/kendo-data-query";
import { CancelEvent, DataBindingDirective, EditEvent, EditService, GridComponent, GridDataResult, GroupableSettings, PageChangeEvent, RemoveEvent, SaveEvent } from '@progress/kendo-angular-grid';
import { AuthService } from 'src/app/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { WindowService } from 'src/app/services/window.service';
import { AlertService } from 'src/app/services/app-services/alert.service';
@Component({
  selector: 'app-your-favorite',
  templateUrl: './your-favorite.component.html',
  styleUrls: ['./your-favorite.component.scss']
})
export class YourFavoriteComponent  implements OnInit, OnDestroy {
  @ViewChild(DataBindingDirective) dataBinding: DataBindingDirective;
  public skip = 0;
  public pageSize = 15;
  public min: number = 0;
  public gridViewSearch: any[];
  public loadingPanelVisible = false;
  public songs: Song[];
  public gridView: GridDataResult;
  public groupable: GroupableSettings;
  private editService: EditService;
  private editedRowIndex: number;
  private editedProduct: Song;
  public deleteModalOpened = false;
  public isCreateModalOpened = false;
  private response;
  private categoryDeleteListener: Subscription;
  private windowCloseListener: Subscription;
  private categorySaveListener: Subscription;

  userId: number;

  constructor(private alertService: AlertService ,private router: Router, private route: ActivatedRoute, private configStore: ConfigStore, private songService: SongService, private authService: AuthService, private windowService: WindowService) {
    this.addListeners();
  }

  ngOnDestroy(): void { }
  
  async ngOnInit(): Promise<void> {
    this.route.params.subscribe(async params => {
        const userData = JSON.parse(localStorage.getItem('userData'));
        this.userId = userData.id;
        this.configStore.startLoadingPanel();
        await this.getAllFavorites();
        await this.loadData();
        this.configStore.stopLoadingPanel();

    })
  }
  private async addListeners(){
    this.windowCloseListener = this.windowService.openWindow.subscribe((value) => {
      this.isCreateModalOpened = value;
      this.deleteModalOpened = value;
    })
    this.categoryDeleteListener = this.windowService.deleteObject.subscribe(async object => {

    })
  }

  private async removeListeners(){
    this.windowCloseListener.unsubscribe();
    this.categorySaveListener.unsubscribe();
    this.categoryDeleteListener.unsubscribe();
  }

  private async getAllFavorites(){
    this.loadingPanelVisible = true;
    this.songs = await lastValueFrom(this.songService.getAllFavorites(this.userId));
    this.loadingPanelVisible = false;
  }
  
  private async loadData() : Promise<void> {
this.gridView = {
  data: this.songs.slice(this.skip, this.skip + this.pageSize),
  total: this.songs.length,
}

this.groupable = {
  showFooter: true,
  emptyText: "Przeciągnij tutaj kolumnę i upuść aby pogrupować"
}

let event: any = { target: { value: ""}};
this.onFilter(event);
  }


  public onFilter(event: any): void {
    this.gridViewSearch = process(this.songs, {
      filter: {
        logic: "or",
        filters: [
          {
            field: "title",
            operator: "contains",
            value: event.target.value,
          },
          {
            field: "artist",
            operator: "contains",
            value: event.target.value,
          },
          {
            field: "album",
            operator: "contains",
            value: event.target.value,
          },
          {
            field: "year",
            operator: "contains",
            value: event.target.value,
          }
        ],
      },
    }).data;

    this.dataBinding.skip = 0;
  }
  public cancelHandler(args: CancelEvent): void {
    // call the helper method
    this.closeEditor(args.sender, args.rowIndex);
  }
  
  private closeEditor(
    grid: GridComponent,
    rowIndex = this.editedRowIndex
  ): void {
    // close the editor
    grid.closeRow(rowIndex);
    // revert the data item to original state
    this.editService.newDataItem() //.resetItem(this.editedProduct);
    // reset the helpers
    this.editedRowIndex = undefined;
    this.editedProduct = undefined;
  }
  
  public saveHandler(args: SaveEvent): void {
    // update the data source
    this.editService.save(args.dataItem);

    // close the editor, that is, revert the row back into view mode
    args.sender.closeRow(args.rowIndex);

    this.editedRowIndex = undefined;
    this.editedProduct = undefined;
  }

  
 public async removeHandler({dataItem}): Promise<void> {
    // remove the current dataItem from the current data source
    // in this example, the dataItem is `editService`
   this.configStore.startLoadingPanel();
    this.songs = dataItem;
    this.response = await lastValueFrom(this.songService.deleteSong(this.songs['id']));
   this.alertService.showSuccess("Usunięto pomyślnie!");
  await this.getAllFavorites();
   await this.loadData();
   this.configStore.stopLoadingPanel();
  }
  
  
  public editHandler(args: EditEvent): void {
    this.editedRowIndex = args.rowIndex;
    this.editedProduct = Object.assign({}, args.dataItem);
    args.sender.editRow(args.rowIndex);

  }
 
}

