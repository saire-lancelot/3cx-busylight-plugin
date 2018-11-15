# Busylight Integration in 3CX Client (Windows)
V: 1.0 / 15.11.2018

Informationen:
---------------------------------------------------------------------
Dise DLL-Files integrieren das Busylight von Kuando in den 3CX-Client für Windows.
Weiter Informationen zum Produkt: https://www.busylight.com/

Einrichtung:
---------------------------------------------------------------------
3CX-Client beenden

Files von "3cx-busylight-plugin/bin/Release/" 
in "C:\ProgramData\3CXPhone for Windows\PhoneApp kopieren."

Wenn keine anderen Plugins installiert sind, kann die Datei "MyPhoneCRMIntegration.dll" ersetzt werden. 

3CX-Client wieder starten

Aktives Farbprofil:
---------------------------------------------------------------------
- Das Busylight ist nur aktiv, wenn der 3CX Client gestartet wurde
- Grün: Wenn der Mitarbeiter auf "Verfügbar" ist und kein Anruf aktiv ist
- Rot: Wenn der Mitarbeiter auf "Abwesend" oder "Nicht stören" ist
- Gelb blinkend: Wenn der Mitarbeiter eine Nummer wählt oder gerade angerufen wird
- Rot: Wenn der Mitarbeiter aktiv in einem Gespräch ist
- Blau blinkend: Wenn der Mitarbeiter aktiv in einem Gespräch und das Mikrofon stumm geschalten ist.

Weitere Farbwünsche oder Änderungen nehme ich gerne entgegen (Das Ding kann jede RGB-Farbe halbwegs darstellen)!

Lizenz Informationen:
---------------------------------------------------------------------
Ursprung: Bart Hamblok 2017 (https://github.com/Dcnigma/Bizylight-3XC-integration)
Änderungen: Lars Wolf, Mario Hiltbrunner

In Visual Studio 2017 Enterprise entwickelt
GNU General Public License 2.0
