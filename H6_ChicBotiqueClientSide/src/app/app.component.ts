
import { Component ,OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Guid } from 'guid-typescript';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  public visitorGuid : Guid;
  
  constructor( private route: ActivatedRoute, private cookieService:CookieService) {
    this. visitorGuid = Guid.create();

    this.cookieService.set('VisitorID', (this. visitorGuid).toString(), 30);
    const cookieValue = this.cookieService.get('VisitorID');
    console.log('Cookie value:VisitorID::::',  cookieValue);
  }

  // Determine if the current route is the frontpage route
  isFrontpageRoute(): boolean {
    // Check if the first child route exists and its routeConfig is not null
    return this.route.snapshot.firstChild?.routeConfig?.path === '';
  }

  isAdminpageRoute(): boolean {
    // Check if the first child route exists and its routeConfig is not null
    return this.route.snapshot.firstChild?.routeConfig?.path === 'admin-panel';
  }
  title(title: any) {
    throw new Error('Method not implemented.');
  }
}
