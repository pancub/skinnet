import { Component, OnInit } from '@angular/core';
import { Iproduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product : Iproduct;

  constructor(private shopService : ShopService,private activeRoute : ActivatedRoute,
    private bcService :BreadcrumbService ) { 
      this.bcService.set('@productDetails','');
    }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct()
  {
    this.shopService.getProduct(+this.activeRoute.snapshot.paramMap.get('id')).subscribe(
      product => 
      {
        this.product = product;
        this.bcService.set('@productDetails',product.name);
      },
      error =>
      {
        console.log(error);
      }

    )
  }
}
