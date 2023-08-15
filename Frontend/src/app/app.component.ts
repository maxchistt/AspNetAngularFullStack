import { Component , isDevMode } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private http:HttpClient) {
    this.title = /*this.GetBaseUrl() +*/ '/api/SpaDir';
    http.get<string>(this.title).subscribe((data) => {
      this.title = "dadsdsa";
      this.staticRoot = data.toString();
    });

  }

  developmentUrl:string="http://localhost:5000";

  GetBaseUrl():string {
    return isDevMode() ? this.developmentUrl : window.location.origin;
  }

  title = 'Frontend';
  staticRoot = "root";
}
