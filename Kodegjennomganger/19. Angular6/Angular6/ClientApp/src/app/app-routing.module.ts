import { NgModule } from '@angular/core'; //importerer denne
import { RouterModule, Routes } from '@angular/router'; //importerer denne  
import { LagreComponent } from './lagre/lagre.component'; //importerer lagre-komponent
import { ListeComponent } from './liste/liste.component'; //importerer liste-komponent

//Vi kunne ha definert hele denne oppi @NgModules
const appRoots: Routes = [ //et array med tre objekter
  { path: 'liste', component: ListeComponent }, //når path oppgis som liste så skal liste-komponenten brukes
  { path:'lagre', component: LagreComponent }, //når path lagre så så skal lagre-komponenten brukes
  { path:'', redirectTo:'/liste', pathMatch:'full' } //det som skal skje default når ikke noe oppgitt/det som skal skje første gangen så får man liste
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoots) //denne tar et array med objekter som vi definerer over 
  ],
  exports: [
    RouterModule
    //og det er AppRoutingModule
  ]
})
export class AppRoutingModule { }
