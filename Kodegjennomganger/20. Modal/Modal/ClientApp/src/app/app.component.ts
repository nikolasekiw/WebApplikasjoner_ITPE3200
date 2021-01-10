import { Component} from '@angular/core';
import { NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { Modal } from './modal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html' //her har vi bare den knappen hvor vi kaller funksjonen visModal() 
})
export class AppComponent {
  
  constructor(private modalService: NgbModal) { } //initierer en modelService av type NgbModal

  visModal() {
    //modalService er den vi definerte over mens Modal er den som ligger i modal.ts
    const modalRef = this.modalService.open(Modal, {
      backdrop: 'static',
      // betyr at man ikke kan klikke vekk modalen ved å trykke andre steder

      keyboard: false
      // betyr at man ikke kan klikke vekk modalen med ESC    
    });

    //Vi setter den her, navn kommer fra modal.html der det står "navn". Så setter vi det til Per her.
    //Det er måten å legge på variabler inn i modalen og vise. 
    modalRef.componentInstance.navn = "Per Hansen";

    //Ved retur tar vi modalRef.result.then så kommer returen, altså det vi valgte å skrive inn i modal.html
    //altså enten "Lukk klikk" eller "Slett klikk". Det kommer da tilbake i retur-kallet.
    //Det er en måte på å finne ut av hva man trykket på. Om det var slett-knappen eller lukk-knappen.   
    modalRef.result.then(retur => {
      console.log('Lukket med:'+ retur);
    });
  }
}
