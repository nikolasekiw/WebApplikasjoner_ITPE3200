import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {KundeSpm} from "./KundeSpm/KundeSpm";
import {FAQ} from "./FAQ/FAQ";
import {NyeSpm} from "./NyeSpm/NyeSpm";

const appRoots: Routes = [
  { path: 'KundeSpm', component: KundeSpm },
  { path: 'FAQ', component: FAQ },
  { path: 'NyeSpm', component: NyeSpm},
  { path: '', redirectTo: 'FAQ', pathMatch: 'full' }
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoots)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
