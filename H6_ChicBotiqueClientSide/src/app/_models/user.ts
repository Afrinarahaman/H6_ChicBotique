import { Role } from "./role";

import { ShippingDetails } from "./shippingdetails";

export interface User {

    id: number;
    email: string;
    firstName: string;
    lastName: string;
    password?:string;
    address: string;
    city: string;
    postalcode: string;
    country: string;
    telephone: string;
    role: Role;
    accountInfo?: any;
    homeaddress?: any;
    token?: string;
    shippingDetails?: ShippingDetails; // Optional shipping details
  }

