using MiniERP.Models;
using MiniERP.Services;

class Program
{
    static void Main()
    {
        IKundenService service = new KundenService();
        bool beenden = false;

        while (!beenden)
        {
            Console.WriteLine("\n==== Mini ERP ====");
            Console.WriteLine("1 - Kunden anzeigen");
            Console.WriteLine("2 - Kunde hinzufügen");
            Console.WriteLine("3 - Kunde suchen");
            Console.WriteLine("4 - Kunde löschen");
            Console.WriteLine("5 - Kunde bearbeiten");
            Console.WriteLine("6 - Beenden");
            Console.Write("Auswahl: ");

            string eingabe = Console.ReadLine() ?? "";

            try
            {
                switch (eingabe)
                {
                    case "1":
                        KundenAnzeigen(service);
                        break;
                    case "2":
                        KundeHinzufuegen(service);
                        break;
                    case "3":
                        KundeSuchen(service);
                        break;
                    case "4":
                        KundeLoeschen(service);
                        break;
                    case "5":
                        KundeBearbeiten(service);
                        break;
                    case "6":
                        beenden = true;
                        break;
                    default:
                        Console.WriteLine("Ungültige Auswahl!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }
    }

    static void KundenAnzeigen(IKundenService service)
    {
        var kunden = service.AlleKunden();
        if (kunden.Count == 0)
        {
            Console.WriteLine("Keine Kunden vorhanden.");
            return;
        }
        foreach (var k in kunden) k.Anzeigen();
    }

    static void KundeHinzufuegen(IKundenService service)
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Ungültige ID"); return; }

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        var email = Console.ReadLine() ?? "";

        service.KundeHinzufuegen(new Kunde(id, name, email));
        Console.WriteLine("Kunde hinzugefügt.");
    }

    static void KundeSuchen(IKundenService service)
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Ungültige ID"); return; }

        var kunde = service.KundeSuchen(id);
        if (kunde == null) Console.WriteLine("Nicht gefunden.");
        else kunde.Anzeigen();
    }

    static void KundeLoeschen(IKundenService service)
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Ungültige ID"); return; }

        if (service.KundeLoeschen(id)) Console.WriteLine("Kunde gelöscht.");
        else Console.WriteLine("Kunde nicht gefunden.");
    }

    static void KundeBearbeiten(IKundenService service)
    {
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Ungültige ID"); return; }

        var kunde = service.KundeSuchen(id);
        if (kunde == null) { Console.WriteLine("Nicht gefunden."); return; }

        Console.WriteLine("Aktueller Name: " + kunde.Name);
        Console.Write("Neuer Name (leer = unverändert): ");
        string? neuerName = Console.ReadLine();

        Console.WriteLine("Aktuelle Email: " + kunde.Email);
        Console.Write("Neue Email (leer = unverändert): ");
        string? neueEmail = Console.ReadLine();

        if (service.KundeAktualisieren(id, neuerName, neueEmail))
            Console.WriteLine("Kunde aktualisiert.");
        else
            Console.WriteLine("Fehler beim Aktualisieren.");
    }
}
