import { Component, OnInit } from '@angular/core';
import { Category, Product } from '../_models/product';
import { ActivatedRoute } from '@angular/router';
import { CartService } from '../_services/cart.service';
import { ProductService } from '../_services/product.service';
import { WishlistService } from '../_services/wishlist.service';
import { WishlistItem } from '../_models/wishlistItem';


@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  productId: number =0;
  wishlist: number[] = [];
  category:Category ={ id: 0, categoryName: '' };
  public quantity:number=0;
  product:Product={id: 0, title:"", price:0, description:"",image:"", stock:0,categoryId:0, category:this.category }
  wishlistItem : WishlistItem= {productId: 0, productTitle:"",productImage:"",productDescription:"",productPrice:0}
  constructor(
    private productService:ProductService,
    private cartService:CartService,
    private route:ActivatedRoute,
     private wishlistService: WishlistService)
     { }

     addedToWishlist: boolean = false;

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.productId = +params['id'];
    });
    this.productService.getProductById(this.productId).subscribe(x=>
      { this.product=x,

      console.log(this.product.stock);
    });

  }
  addToCart(product:Product)
  {

    this.cartService.addToBasket({
      productId:product.id,
      productTitle:product.title,
      productPrice:product.price,
      productImage:product.image,
      quantity:this.quantity + 1,

    });
    window.location.reload();
  }

 handleAddtoWishlist(product:Product)
 {
    this.wishlistService.addToWishlist({
      productId: product.id,
      productTitle: product.title,
      productImage:product.image,
      productDescription:product.description,
      productPrice:product.price

    });
    //window.location.reload();
    this.addedToWishlist = true;
    console.log("Product Id: "+ this.productId + "  is added to wishlist");

  }



  handleRemovefromWishlist(){
    this.wishlistService.removeItemFromWishlist(this.productId);

    this.addedToWishlist= false;

  }


}

