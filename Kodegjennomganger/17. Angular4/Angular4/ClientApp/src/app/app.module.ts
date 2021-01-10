import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

/**
 * Her må vi legge til en ny modul: HttpClientModule. Den må da også inn under imports. 
**/

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    HttpClientModule
    //må også inn her 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
