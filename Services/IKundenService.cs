using MiniERP.Models;
using System.Collections.Generic;

namespace MiniERP.Services;

public interface IKundenService
{
    List<Kunde> AlleKunden();
    void KundeHinzufuegen(Kunde kunde);
    Kunde? KundeSuchen(int id);
    bool KundeLoeschen(int id);
    bool KundeAktualisieren(int id, string? neuerName, string? neueEmail);
}
