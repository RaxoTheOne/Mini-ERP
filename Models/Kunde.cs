namespace MiniERP.Models;

public class Kunde
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Kunde(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public void Anzeigen()
    {
        Console.WriteLine($"ID: {Id} | Name: {Name} | Email: {Email}");
    }
}
