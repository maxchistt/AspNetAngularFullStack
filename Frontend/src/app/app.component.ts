import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  forecasts:WeatherForecast[]=[];
  forecastJSON:string = 'fffforecast...';

  title = 'Frontend';
  staticRoot = "root";

  constructor(private http:HttpClient) {
    http.get('/api/weatherForecast').subscribe((data) => {
      this.forecastJSON = JSON.stringify(data,null,3);
      this.forecasts = data as WeatherForecast[];
    });
  }

}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
