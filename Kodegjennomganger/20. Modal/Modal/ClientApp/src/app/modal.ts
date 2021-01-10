import { Component} from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  templateUrl: 'modal.html'
})
export class Modal {
  constructor(public modal: NgbActiveModal) { }
  //Denne gjør ikke noe mer enn å initiere NgbActiveModal. Grunnen til det er at hvis
  //vi vil flytte noe data ut i modalen (modal.html) hvis vi vil flytte navnet ut.... gå i modal.html
}