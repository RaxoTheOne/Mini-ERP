using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MiniERP.Models;

namespace MiniERP.Services
{
    public class KundenService : IKundenService
    {
        private readonly string _dateiPfad;
        private readonly Logger _logger;
        private List<Kunde> _kunden;

        // 🔹 Standard-Konstruktor (Produktivbetrieb)
        public KundenService() : this("kunden.json")
        {
        }

        // 🔹 Testbarer Konstruktor (Unit-Tests)
        public KundenService(string dateiPfad)
        {
            _dateiPfad = dateiPfad;
            _logger = new Logger();
            _kunden = Laden();
        }

        public List<Kunde> AlleKunden() => _kunden;

        public void KundeHinzufuegen(Kunde kunde)
        {
            if (_kunden.Any(k => k.Id == kunde.Id))
            {
                _logger.Error($"Kunde mit ID {kunde.Id} existiert bereits.");
                throw new InvalidOperationException("Kunde mit dieser ID existiert bereits.");
            }

            _kunden.Add(kunde);
            Speichern();
            _logger.Info($"Kunde hinzugefügt: ID {kunde.Id}");
        }

        public Kunde? KundeSuchen(int id)
        {
            return _kunden.FirstOrDefault(k => k.Id == id);
        }

        public bool KundeLoeschen(int id)
        {
            var kunde = KundeSuchen(id);
            if (kunde == null)
            {
                _logger.Error($"Löschversuch: Kunde {id} nicht gefunden");
                return false;
            }

            _kunden.Remove(kunde);
            Speichern();
            _logger.Info($"Kunde gelöscht: ID {id}");
            return true;
        }

        public bool KundeAktualisieren(int id, string? neuerName, string? neueEmail)
        {
            var kunde = KundeSuchen(id);
            if (kunde == null)
                return false;

            if (!string.IsNullOrWhiteSpace(neuerName))
                kunde.Name = neuerName;

            if (!string.IsNullOrWhiteSpace(neueEmail))
                kunde.Email = neueEmail;

            Speichern();
            _logger.Info($"Kunde aktualisiert: ID {id}");
            return true;
        }

        private List<Kunde> Laden()
        {
            try
            {
                if (!File.Exists(_dateiPfad))
                    return new List<Kunde>();

                string json = File.ReadAllText(_dateiPfad);
                return JsonSerializer.Deserialize<List<Kunde>>(json) ?? new List<Kunde>();
            }
            catch (Exception ex)
            {
                _logger.Error("Fehler beim Laden: " + ex.Message);
                return new List<Kunde>();
            }
        }

        private void Speichern()
        {
            try
            {
                var optionen = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_kunden, optionen);
                File.WriteAllText(_dateiPfad, json);
            }
            catch (Exception ex)
            {
                _logger.Error("Fehler beim Speichern: " + ex.Message);
            }
        }
    }
}
