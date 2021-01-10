import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-meny', //denne kommer i app-nav-meny fordi den har den selectoren 
  templateUrl: './nav-meny.component.html' //denne html-en skal erstatte app-nav-meny i app-component
})
export class NavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}