import { Component } from '@angular/core';

@Component({
  //selector: 'app-root', -denne gjør ikke noe da det er routing som gjelder
  templateUrl: './lagre.component.html' //denne html-en her skal da erstatte app-nav-meny i app-component.ts   
})
export class LagreComponent {
  
}

/**
 * Lagre-komponenten gjør ikke noe. Den bruker bare lagre-component.html
 * og denne selector som vi ha rsett på før gir ikke noe mening her når vi skal
 * bruke routing fordi vi skal se på hvordan disse blir rendret etterpå?
**/
