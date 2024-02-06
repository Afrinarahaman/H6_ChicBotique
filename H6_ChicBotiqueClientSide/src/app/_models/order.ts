
import { CartItem } from "./cartItem";
import { ShippingDetails } from "./shippingdetails";


export interface Order {

    id?: number;
    accountId: string;
    orderDate?: Date;
    amount: number;
    transactionId: string;
    status: string;
    paymentMethod:string;
    shippingDetails: ShippingDetails;

    orderDetails: CartItem[];


}
