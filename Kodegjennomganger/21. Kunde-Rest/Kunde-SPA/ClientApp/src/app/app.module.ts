import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { SPA } from './spa'; //importerer SPA-komponenten
@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  declarations: [SPA],
  bootstrap: [SPA] //bootstrapper SPA, det er bare den komponenten vi kjører
})
export class AppModule { }
