import { Component, OnInit } from '@angular/core';
import { Category, Product } from 'src/app/_models/product';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/_services/cart.service';
import { ProductService } from 'src/app/_services/product.service';
import { WishlistService } from 'src/app/_services/wishlist.service';
import { WishlistItem } from 'src/app/_models/wishlistItem';
import { Observable, firstValueFrom, of, throwError } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { CartItem } from 'src/app/_models/cartItem';



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
  public totalItem:number=0;
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
  /*addToCart(product:Product)
  {

    
    this.cartService.addToBasket({
      productId:product.id,
      productTitle:product.title,
      productPrice:product.price,
      productImage:product.image,
      quantity: this.quantity+1
      

    });
    
    window.location.reload();

    
   // this.totalItem = this.cartService.getBasket().length;
  }*/
  async addToCart(product: Product): Promise<any> {
    try {
      const availableStock = await firstValueFrom(this.productService.getAvailableStock(product.id));
      if (product.stock <= availableStock) {
        const item: CartItem = {
        
          productId: product.id,
          productTitle: product.title,
          productPrice: product.price,
          productImage: product.image,
          quantity: this.quantity+1
        };
        console.log("AvailableStock",availableStock);
        this.cartService.addToBasket(item);
      } else {
         alert('Not enough stock');
      }
    } catch (error) {
      console.error('Error adding to cart:', error);
      // Handle error as needed
    }
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



