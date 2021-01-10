import { Component } from '@angular/core';

@Component({
  selector: 'app-root', //denne bruker en selector app-root, dvs. at denne komponenten her blir rendret inn i index-en (index.html)
  templateUrl: './app.component.html' //dette er app.component.html
})
export class AppComponent {
  title = 'app';
}
