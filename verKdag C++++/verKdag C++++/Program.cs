using System.Globalization;

// Verkrijg de huidige datum en wis de uren, minuten en seconden
CultureInfo provider = CultureInfo.InvariantCulture;
string vandaagString = DateTime.Now.ToString("dd-MM-yyyy", provider);
DateTime vandaag = DateTime.ParseExact(vandaagString, "dd-MM-yyyy", provider);

// Maak een variabele verKdag en leeftijd
string verKdag = null;
int leeftijd = 0;

// Vraag de verjaardag van de user en zet dit om naar DateTime
Console.WriteLine("Wanneer ben je jarig? (dd-mm-YYYY)?");
string input = Console.ReadLine();
DateTime verjaardag = DateTime.ParseExact(input, "dd-MM-yyyy", provider);

void getVerKdag (DateTime verjaardag, DateTime vandaag)
{
    int leeftijd = 0;
    string verKdag = null;

    while (verKdag == null)
    {
        leeftijd++;
        DateTime datum = verjaardag.AddDays(leeftijd * 1000);
        if (datum > vandaag)
        {
            TimeSpan verschil = datum - vandaag;
            verKdag = $"over {verschil.Days} dagen, op {datum.ToString("dd-MM-yyyy", provider)}";
        }
        else if (datum == vandaag)
        {
            verKdag = "vandaag";
        }
    }
}


//
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

Console.WriteLine($"Je hebt al {leeftijd} verKdag(en) gehad en je volgende verKdag is {verKdag}.");