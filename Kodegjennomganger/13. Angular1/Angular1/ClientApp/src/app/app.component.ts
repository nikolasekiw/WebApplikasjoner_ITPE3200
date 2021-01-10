import { Component } from '@angular/core'; //importerer(i hele koden her) en komponent fra core, funksjonaliteten for en komponent.

@Component({
  /**
   * Dette er en dekoratør - @Component, den har en selector som heter app-root, dvs. at
   * denne komponenten, det som står inne i komponenten (helt til klammeparentes lukkes) skal
   * bli erstattet med den app-root componenten i index. Dvs. at app.component.html blir erstattet med
   * det som ligger i index.html
  **/
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  //AppComponent definerer app-rooten og i app.component.html så erstatter html-en det som ligger i index.html mellom <app-root> taggene.

  title = 'app';
}

/**
 * Hvis jeg da har app-root her, så er det denne komponenten her (app.component.ts (typescript))
 * som transpileres ned til en JS fil. Transpileres istedenfor kompileres fordi den oversetter
 * bare fra ett språk til et annet. Det er JS filen som kjøres i nettleseren, men vi skriver i typescipt
 * fordi det er et strengt typet språk som gjør at man kan finne feil lettere.
 *
 * main.ts er det som starter hele saken.
**/
