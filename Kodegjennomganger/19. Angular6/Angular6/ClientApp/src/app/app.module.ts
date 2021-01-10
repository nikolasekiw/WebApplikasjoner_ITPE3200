import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component'; //må importere alle disse  
import { LagreComponent } from './lagre/lagre.component';
import { ListeComponent } from './liste/liste.component';
import { NavMenuComponent } from './nav-meny/nav-meny.component';
import { AppRoutingModule } from './app-routing.module'; //det er den som heter app-routing-module.ts

@NgModule({
  declarations: [ //tilsvarende her  
    AppComponent,
    LagreComponent,
    ListeComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule //og AppRoutingModule her
  ],
  providers: [],
  bootstrap: [AppComponent] //bootstrapper AppComponent (app.component.ts)
})
export class AppModule { }
