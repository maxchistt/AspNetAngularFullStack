import { Component , isDevMode } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private http:HttpClient) {
    http.get<string>('/api/weatherForecast').subscribe((data) => {
      this.forecast = data;
    });

  }

  developmentUrl:string="http://localhost:5000";

  GetBaseUrl():string {
    return isDevMode() ? this.developmentUrl : window.location.origin;
  }

  forecast = 'forecast...';
  title = 'Frontend';
  staticRoot = "root";
}
