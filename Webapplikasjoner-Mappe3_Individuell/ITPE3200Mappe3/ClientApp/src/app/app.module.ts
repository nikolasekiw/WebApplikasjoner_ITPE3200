import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import {RouterModule} from "@angular/router";
import {FAQ} from "./FAQ/FAQ";
import {KundeSpm} from "./KundeSpm/KundeSpm";
import {Meny} from "./Meny/meny";
import {HttpClientModule} from "@angular/common/http";
import {AppRoutingModule} from "./app-routing.module";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from "@angular/forms";
import {Modal} from "./Modal/modal";
import {NyeSpm} from "./NyeSpm/NyeSpm";

@NgModule({
  declarations: [
    AppComponent,
    FAQ,
    KundeSpm,
    NyeSpm,
    Modal,
    Meny
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    RouterModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [Modal]
})
export class AppModule { }
