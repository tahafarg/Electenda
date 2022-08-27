import { Component, OnInit } from '@angular/core';
import { FavoriteAddViewModel } from '../models/Favourite';
import { FavouriteService } from '../Services/FavouriteServices';


@Component({
  selector: 'app-favourite',
  templateUrl: './favourite.component.html',
  styleUrls: ['./favourite.component.css']
})
export class FavouriteComponent implements OnInit {

  constructor(private favSer:FavouriteService) { }
  loading:boolean=true;
  favs:FavoriteAddViewModel[]=[]
  ngOnInit(): void {

    this.loading = true
    var uid = localStorage.getItem('userId')??"";

//    this.favSer.getFavs(uid).subscribe(res=>this.favs = res.data as FavoriteAddViewModel);

    this.favSer.getFavs(uid).subscribe(res=>{
      this.favs = res.data as FavoriteAddViewModel[]
       console.log(res.data)

    });
    
  }
Del(id:number)
{
  this.favSer.deleteFavs(id).subscribe();
  window.location.reload()
}
}
