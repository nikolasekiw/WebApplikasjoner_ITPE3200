import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'; //må ha med denne
// installer ved å stå på Client mappen og kjør 'npm install --save @ng-bootstrap/ng-bootstrap@5' (med angular v8)

import { Modal } from './modal'; //dette er selve modalen

@NgModule({ //deklarerer og importerer den 
  declarations: [
    AppComponent,
    Modal
  ],
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule //importerer NgbModule 
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [Modal] // merk!    
})
export class AppModule { }
