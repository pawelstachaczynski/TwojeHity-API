import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ConfigStore } from 'src/app/app-config/config-store';
import { Song } from 'src/app/models/song.model';
import { SongService } from 'src/app/models/song.service';
import { process } from "@progress/kendo-data-query";
import { CancelEvent, DataBindingDirective, EditEvent, EditService, GridComponent, GridDataResult, GroupableSettings, PageChangeEvent, RemoveEvent, SaveEvent } from '@progress/kendo-angular-grid';
@Component({
  selector: 'app-browse-all',
  templateUrl: './browse-all.component.html',
  styleUrls: ['./browse-all.component.scss']
})
export class BrowseAllComponent implements OnInit, OnDestroy {
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
  constructor(private configStore: ConfigStore, private songService: SongService) {}
  //https://www.telerik.com/kendo-angular-ui/components/grid/editing/inline-editing/

  ngOnDestroy(): void {}
  async ngOnInit(): Promise<void> {
    this.configStore.startLoadingPanel();
    await this.getAll();
    await this.loadData();
    this.configStore.stopLoadingPanel();
  }

  private async getAll(){
    this.loadingPanelVisible = true;
    this.songs = await lastValueFrom(this.songService.getAll());
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
    this.editService.newDataItem() 
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

  public removeHandler(args: RemoveEvent): void {
    // remove the current dataItem from the current data source
    // in this example, the dataItem is `editService`
    this.editService.remove(args.dataItem);
  }
  
  
  public editHandler(args: EditEvent): void {
    this.closeEditor(args.sender);
    this.editedRowIndex = args.rowIndex;
    this.editedProduct = Object.assign({}, args.dataItem);
    args.sender.editRow(args.rowIndex);

  }
 
}

