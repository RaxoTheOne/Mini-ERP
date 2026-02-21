using Xunit;
using MiniERP.Services;
using MiniERP.Models;
using System.IO;

namespace MiniERP.Tests;

public class KundenServiceTests : IDisposable
{
    private const string TestDatei = "kunden_test.json";
    private readonly KundenService _service;

    // ==============================
    // SETUP (läuft vor JEDEM Test)
    // ==============================
    public KundenServiceTests()
    {
        if (File.Exists(TestDatei))
            File.Delete(TestDatei);

        _service = new KundenService(TestDatei);
    }

    // ==============================
    // CLEANUP (läuft nach JEDEM Test)
    // ==============================
    public void Dispose()
    {
        if (File.Exists(TestDatei))
            File.Delete(TestDatei);
    }

    // ==============================
    // TESTS
    // ==============================

    [Fact]
    public void KundeHinzufuegen_FuegtKundeHinzu()
    {
        var kunde = new Kunde(1, "Max Mustermann", "max@test.de");

        _service.KundeHinzufuegen(kunde);

        var alle = _service.AlleKunden();
        Assert.Single(alle);
        Assert.Equal("Max Mustermann", alle[0].Name);
    }

    [Fact]
    public void KundeHinzufuegen_DoppelteId_WirftException()
    {
        _service.KundeHinzufuegen(new Kunde(1, "Max", "max@test.de"));

        Assert.Throws<InvalidOperationException>(() =>
            _service.KundeHinzufuegen(new Kunde(1, "Erika", "erika@test.de"))
        );
    }

    [Fact]
    public void KundeLoeschen_VorhandenerKunde_GibtTrueZurueck()
    {
        _service.KundeHinzufuegen(new Kunde(1, "Max", "max@test.de"));

        bool result = _service.KundeLoeschen(1);

        Assert.True(result);
        Assert.Empty(_service.AlleKunden());
    }

    [Fact]
    public void KundeLoeschen_NichtVorhandenerKunde_GibtFalseZurueck()
    {
        bool result = _service.KundeLoeschen(999);

        Assert.False(result);
    }

    [Fact]
    public void KundeSuchen_VorhandenerKunde_WirdZurueckgegeben()
    {
        _service.KundeHinzufuegen(new Kunde(1, "Max", "max@test.de"));

        var result = _service.KundeSuchen(1);

        Assert.NotNull(result);
        Assert.Equal("Max", result!.Name);
    }

    [Fact]
    public void KundeSuchen_NichtVorhandenerKunde_GibtNullZurueck()
    {
        var result = _service.KundeSuchen(999);

        Assert.Null(result);
    }

    [Fact]
    public void KundeAktualisieren_VorhandenerKunde_AktualisiertDaten()
    {
        _service.KundeHinzufuegen(new Kunde(1, "Max", "max@test.de"));

        bool result = _service.KundeAktualisieren(
            1,
            "Max Mustermann",
            "max.mustermann@test.de"
        );

        Assert.True(result);

        var aktualisiert = _service.KundeSuchen(1);
        Assert.Equal("Max Mustermann", aktualisiert!.Name);
        Assert.Equal("max.mustermann@test.de", aktualisiert.Email);
    }

    [Fact]
    public void KundeAktualisieren_NichtVorhandenerKunde_GibtFalseZurueck()
    {
        bool result = _service.KundeAktualisieren(
            999,
            "Neu",
            "neu@test.de"
        );

        Assert.False(result);
    }
}
