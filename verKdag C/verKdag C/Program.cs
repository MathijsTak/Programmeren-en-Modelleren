using System.Globalization;

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

double H = 127.0 / 200.0 * 359.0;
double S = 1;
double L = 0.5;
double C = (1 - Math.Abs(2 * L - 1)) * S;
double X = C * (1 - Math.Abs((H / 60) % 2 - 1));
double M = L - C / 2;
double Rtemp = 0, Gtemp = 0, Btemp = 0;
int r, g, b;

if (0 <= H && H < 60)
{
    Rtemp = (C + M) * 255;
    Gtemp = (X + M) * 255;
    Btemp = (0 + M) * 255;
}
else if (60 <= H && H < 120)
{
    Rtemp = (X + M) * 255;
    Gtemp = (C + M) * 255;
    Btemp = (0 + M) * 255;
}
else if (120 <= H && H < 180)
{
    Rtemp = (0 + M) * 255;
    Gtemp = (C + M) * 255;
    Btemp = (X + M) * 255;
}
else if (180 <= H && H < 240)
{
    Rtemp = (0 + M) * 255;
    Gtemp = (X + M) * 255;
    Btemp = (C + M) * 255;
}
else if (240 <= H && H < 300)
{
    Rtemp = (X + M) * 255;
    Gtemp = (0 + M) * 255;
    Rtemp = (C + M) * 255;
}
else if (300 <= H && H < 360)
{
    Rtemp = (C + M) * 255;
    Gtemp = (0 + M) * 255;
    Rtemp = (X + M) * 255;
}

r = Convert.ToInt32(Rtemp);
g = Convert.ToInt32(Gtemp);
b = Convert.ToInt32(Btemp);

Console.WriteLine(r);
Console.WriteLine(g);
Console.WriteLine(b);

Console.ReadLine();