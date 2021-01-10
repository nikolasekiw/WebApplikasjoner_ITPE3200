import { Component } from '@angular/core';

@Component({
  selector: 'app-root', //denne gjør ikke annet enn å erstatte app-root i index med app.component.html
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'Kunde SPA - Routing';
}
