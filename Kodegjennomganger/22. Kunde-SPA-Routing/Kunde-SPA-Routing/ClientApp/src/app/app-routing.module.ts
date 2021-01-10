import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Lagre} from './lagre/lagre'; //det er de tre som varierer i view-et under menyen.
import { Liste } from './liste/liste';
import { Endre } from './endre/endre';

//Menyen er en egen komponent, også er det liste, lagre og endre som bytter plass avhengig av hvilken routing vi velger (routerLink)

const appRoots: Routes = [ //her definerer vi alle routingene våre, definert i dette konstante arrayet av type Routes.
  { path: 'liste', component: Liste }, //arrayet inneholder fire objekter.
  { path: 'lagre', component: Lagre }, //her skal komponenten lagre brukes, og likt på de tilsvarende.
  { path: 'endre/:id', component: Endre, }, //denne er litt spesiell fordi vi må overføre id-en når vi trykker på endre i lista så skal komponenten endre kjøres.
  { path: '', redirectTo: '/liste', pathMatch: 'full' }
  //dette er default-path, når vi starter applikasjonen så starter localhost og ikke noe mer, derfør ønsker vi å redirecte til liste
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoots) //må importere RuterModule.forRoots og ha konstantet/arrayet som parameter
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { } //kaller den for AppRoutingModule
