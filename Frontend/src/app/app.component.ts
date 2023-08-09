import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private http:HttpClient) {
    this.title = window.location.origin + '/api/SpaDir';
    http.get<string>(this.title).subscribe((data) => {
      this.title = "dadsdsa";
      this.staticRoot = data.toString();
    });

  }

  title = 'Frontend';
  staticRoot = "root";
}
