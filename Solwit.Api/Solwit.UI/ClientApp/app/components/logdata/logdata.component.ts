import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'logdata',
  templateUrl: './logdata.component.html'
})
export class LogDataComponent {
  public logs: Logs[];

  constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
    http.get('http://localhost:58568/api/log').subscribe(result => {
      this.logs = result.json() as Logs[];
    }, error => console.error(error));
  }
}

interface Logs {
  value: string;
}
