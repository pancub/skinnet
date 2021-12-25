import { Component ,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Iproduct } from './shared/models/product';
import { IPagination } from './shared/models/pagination';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  title = 'Skinet';
  
  products : Iproduct[];

  constructor(private basketService : BasketService)
  {

  }
  ngOnInit(): void {

    const basketId = localStorage.getItem('basket_id');

    if(basketId)
    {
      this.basketService.getBasket(basketId).subscribe(
        () => 
        {
          console.log('initialized basket');
        },
        error =>
        {
          console.log(error);
        }
      );

    }
    /*
    this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe( (response : IPagination) => {
      this.products = response.data;
    } , error =>
    {
      console.log(error);
    }
    )
    */
  }
}
