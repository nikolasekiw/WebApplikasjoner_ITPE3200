import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  templateUrl: 'sletteModal.html'
})
export class Modal {
  constructor(public modal: NgbActiveModal) { }
}

/**
 * Her har vi bare klassen modal og kobler til slettModal.html, det er selve modalen
 **/
