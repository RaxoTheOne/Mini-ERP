using System;
using System.IO;

namespace MiniERP.Services;

public class Logger
{
    private readonly string _logDatei = "app.log";

    public void Info(string message)
    {
        Schreiben("INFO", message);
    }

    public void Error(string message)
    {
        Schreiben("ERROR", message);
    }

    private void Schreiben(string level, string message)
    {
        try
        {
            string zeile = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            File.AppendAllText(_logDatei, zeile + Environment.NewLine);
        }
        catch
        {
            // Logging darf niemals das Programm crashen
        }
    }
}
