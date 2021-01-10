import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

//Her må vi importere alle komponentene vi har med + en AppRoutingModule. Trenger ikke å kalle den det, men er greit
import { AppComponent } from './app.component';
import { Lagre } from './lagre/lagre';
import { Liste } from './liste/liste';
import { Endre } from './endre/endre';
import { Meny } from './meny/meny';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    Lagre, //må ha med alle disse komponentene her også
    Liste,
    Endre,
    Meny
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule //må ha med routingen her
  ],
  providers: [],
  bootstrap: [AppComponent] //app.components.ts er bootstrappet
})
export class AppModule { }
