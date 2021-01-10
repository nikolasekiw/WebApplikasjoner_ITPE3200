import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; //måtte legge til denne sånn at løsningen skulle fungere
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule //måtte legge til denne for at løsningen skulle fungere
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
