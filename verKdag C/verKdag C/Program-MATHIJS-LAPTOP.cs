using System.Globalization;

// Verkrijg de huidige datum en Cultureinfo / de manier waarop de datum is geschreven.
CultureInfo provider = CultureInfo.InvariantCulture;
DateTime vandaag = DateTime.Now;

// Maak een variabele verKdag en leeftijd
string verKdag = null;
int leeftijd = -1; // leeftijd is -1 omdat je anders één jaar te oud bent. 

// Vraag de verjaardag van de user en zet dit om naar DateTime
Console.WriteLine("Wanneer ben je jarig? (dd-mm-yyyy)?");
string input = Console.ReadLine();
DateTime verjaardag = DateTime.ParseExact(input, "dd-MM-yyyy", provider);

// Voeg constant 1000 dagen toe aan de verjaardag en check of de datum > of == is aan vandaag.
while (verKdag == null)
{
    leeftijd++;
    DateTime datum = verjaardag.AddDays(leeftijd * 1000);
    if (datum > vandaag)
    {
        var verschil = datum - vandaag;
        verKdag = $"over {verschil.Days} dagen, op {datum.ToString("dd-MM-yyyy", provider)}";
    }
    if (datum == vandaag)
    {
        verKdag = "vandaag";
    }
}

// Output de leeftijd en verKdag.
Console.WriteLine($"Je hebt al {leeftijd} verKdag(en) gehad en je volgende verKdag is {verKdag}.");