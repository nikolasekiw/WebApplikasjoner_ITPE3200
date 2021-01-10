import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms'; //denne må legges inn fra forms
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ReactiveFormsModule
    //må også legge inn ReactiveFormsModule inn i imports her.  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
