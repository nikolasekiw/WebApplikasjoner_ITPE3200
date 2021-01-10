import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; //la til denne

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule
    /**
     * la til FormsModule her fordi vi bruker inputbokser.
     * Hvis vi glemmer å ta med denne forms-modulen, saver og går tilbake,
     * så får jeg bare opp "loading" også får jeg feil nede i konsollet.
     * Får opp feilen "Template parse error", "Cant bind to ngModel since it isn't a
     * known property of 'input" - Det er ikke noe syktansfeil, det er bare fordi
     * jeg ikke har med forms-module og det er ganske vanskelig å forstå at det er det
     * som er problemet. 
    **/

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
