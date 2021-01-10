import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Kundespm } from "../Kundespm";
import {NgbActiveModal, NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {Modal} from "../Modal/modal";

@Component({
  templateUrl: "KundeSpm.html"
})

export class KundeSpm {
  skjema: FormGroup;

  validering = {
    Id: [""],
    fornavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ. \-]{2,30}")])
    ],
    etternavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ. \-]{2,30}")])
    ],
    epost: [
      null, Validators.compose([Validators.required, Validators.pattern("[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}")])
    ],
    nyttSporsmal: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ.,?! \-]{2,200}")])
    ]
  }

  constructor(private http: HttpClient, private fb: FormBuilder, private router: Router, private modalService: NgbModal) {
    this.skjema = fb.group(this.validering);
  }

  LagreSpm(){
    const lagretSpm = new Kundespm();
      lagretSpm.fornavn = this.skjema.value.fornavn;
      lagretSpm.etternavn = this.skjema.value.etternavn;
      lagretSpm.epost = this.skjema.value.epost;
      lagretSpm.nyttSporsmal = this.skjema.value.nyttSporsmal;

      this.http.post("api/kundeService", lagretSpm)
        .subscribe(retur => {
          this.router.navigate(['/NyeSpm']);
          }
      );
    }

    visModal() {
      const modalRef = this.modalService.open(Modal, {
        backdrop: 'static',
        keyboard: false
      });
      modalRef.componentInstance.fornavn = this.skjema.value.fornavn;
      modalRef.componentInstance.etternavn = this.skjema.value.etternavn;
      modalRef.componentInstance.epost = this.skjema.value.epost;
      modalRef.componentInstance.nyttSporsmal = this.skjema.value.nyttSporsmal;

      modalRef.result.then(retur => {
        this.LagreSpm();
      }).catch(error => {console.log(error);});
    }
}
