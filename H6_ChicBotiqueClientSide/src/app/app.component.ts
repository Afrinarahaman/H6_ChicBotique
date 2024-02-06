
import { Component ,OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor( private route: ActivatedRoute) {}

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
