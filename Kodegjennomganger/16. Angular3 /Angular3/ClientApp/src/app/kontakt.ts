export class kontakt {
  public utData: string;

  constructor(navn: string, telefon: string) {
    this.utData = navn + "   " + telefon;
  }
}

/**
 * Definerte også denne. En klasse hvor jeg definerer attributten utData
 * av typen string, så har jeg konstruktør med navn og tlf, disse blir konkatinert.
 * Så ser på app.component.html
**/