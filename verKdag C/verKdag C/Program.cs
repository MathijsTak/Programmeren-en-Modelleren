﻿using System.Globalization;

// Verkrijg de huidige datum en wis de uren, minuten en seconden
CultureInfo provider = CultureInfo.InvariantCulture;
DateTime vandaag = DateTime.Now;

// Maak een variabele verKdag en leeftijd
string verKdag = null;
int leeftijd = 0;

// Vraag de verjaardag van de user en zet dit om naar DateTime
Console.WriteLine("Wanneer ben je jarig? (dd-mm-yyyy)?");
string input = Console.ReadLine();
DateTime verjaardag = DateTime.ParseExact(input, "dd-MM-yyyy", provider);

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
string cijfer = "0,931E-8";
Console.WriteLine(Double.Parse(cijfer));
Console.ReadLine();