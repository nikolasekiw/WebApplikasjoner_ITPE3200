import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-meny',
  templateUrl: './meny.html'
})
export class Meny {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

/**
 * I denne applikasjonen er det flere komponenter enn i den forrige. En komponent er f.eks. endre, liste,
 * lagre og meny. Alle disse er komponenter
 **/
