KursVerwaltung – Frontend (WPF)

Dieses Frontend ist eine C#-WPF-Anwendung zur Verwaltung von Kursen und Räumen.
Es bietet eine grafische Benutzeroberfläche zur Anzeige, Erstellung und Löschung von Räumen und Kursen, die über den DbService mit der Datenbank kommuniziert.

Hauptfenster
MainWindow

Zeigt alle verfügbaren Räume in einer DataGrid-Tabelle.
Ermöglicht das Hinzufügen neuer Räume (NewRoom-Dialog).
Ermöglicht das Hinzufügen neuer Kurse (NewCourse-Dialog).
Öffnet bei Auswahl eines Raums das Fenster mit den zugehörigen Kursen (CourseListWindow).
Räume können direkt über die Oberfläche mit den zugehörigen Kursen gelöscht werden.

Kursverwaltung
CourseListWindow

Zeigt alle Kurse eines ausgewählten Raums.
Kurse können einzeln gelöscht werden.
Anzeige von Kursdetails: Name, Dauer, Preis, Buchungsstatus.

NewCourse

Eingabemaske zum Anlegen eines neuen Kurses.
Validiert Pflichtfelder (Titel, Dauer, Preis, Raum).
Übergibt die eingegebenen Werte an den DbService.

Raumverwaltung
NewRoom

Eingabemaske zum Anlegen eines neuen Raums.
Validiert Pflichtfelder (Name, Größe, Kapazität).
Übergibt die Eingaben an den DbService.

Technische Hinweise

Framework: WPF (.NET Framework, C#)
Datenbindung: ObservableCollection<T> für automatische UI-Aktualisierung.
Interaktion: Buttons, DataGrid, Dialogfenster.
Validierung: Einfache Eingabeprüfungen über MessageBox.

Aufbau (Frontend-Dateien)
Datei	Funktion
MainWindow.xaml(.cs)	Hauptoberfläche mit Raumübersicht und Navigationslogik
CourseListWindow.xaml(.cs)	Anzeige und Verwaltung der Kurse eines Raums
NewCourse.xaml(.cs)	Dialogfenster zum Erstellen eines neuen Kurses
NewRoom.xaml(.cs)	Dialogfenster zum Erstellen eines neuen Raums