using System.Media;

(int, int) oudHokje = (0,0); // Variabele die gebruikt wordt om het vorige vakje te deselecteren

// Maak de gameEngine en het scherm. De gameEngine wordt gebruikt om gegevens van het spel op te slaan
// en het scherm wordt gebruikt om de user informatie te geven over het spel. (UI)
Engine gameEngine = new Engine();
Scherm scherm = new Scherm(gameEngine.hokjes);

// De buttons en acties die een gebruiker kan uitvoeren.
scherm.nieuwSpel.Click += StartSpel;
scherm.helpKnop.Click += Help;
scherm.verkleinBord.Click += VerkleinBord;
scherm.vergrootBord.Click += VergrootBord;
scherm.scoreSpeler1.Click += VeranderKleur;
scherm.scoreSpeler2.Click += VeranderKleur;
scherm.bord.MouseClick += Klik;
scherm.bord.MouseMove += BeweegMuis;
scherm.Resize += UpdateBord;

// Start de applicatie
Application.Run(scherm);

void StartSpel(object sender, EventArgs e) // Start het spel
{
    // Start het spel in de engine en op het scherm. Dit update ook gelijk de mogelijke zetten, score en beurt.
    gameEngine.StartSpel(scherm.computer.Checked);
    scherm.NieuwSpel(gameEngine.hokjes, gameEngine.speler);

    // Doe de zet van de computer
    Computer();
}

void VerkleinBord(object sender, EventArgs ea) // Verkleind het bord tot en met 4
{
    if (gameEngine.hokjes != 4)
    {
        gameEngine.hokjes -= 2;
        scherm.grootteBord.Text = $"{gameEngine.hokjes} × {gameEngine.hokjes}";
    }
}

void VergrootBord(object sender, EventArgs ea) // vergroot het bord tot en met 20
{
    if (gameEngine.hokjes != 20)
    {
        gameEngine.hokjes += 2;
        scherm.grootteBord.Text = $"{gameEngine.hokjes} × {gameEngine.hokjes}";
    }
}

void VeranderKleur(object sender, EventArgs ea) // Veranderd de kleur van de speler
{
    if (sender == scherm.scoreSpeler1)
    {
        if (scherm.kleurSpeler1 == scherm.kleuren.Count() - 1) scherm.kleurSpeler1 = 0;
        else scherm.kleurSpeler1++;

        if (scherm.kleurSpeler1 == scherm.kleurSpeler2)
        {
            if (scherm.kleurSpeler1 == scherm.kleuren.Count() - 1) scherm.kleurSpeler1 = 0;
            else scherm.kleurSpeler1++;
        }
    }
    else
    {
        if (scherm.kleurSpeler2 == scherm.kleuren.Count() - 1) scherm.kleurSpeler2 = 0;
        else scherm.kleurSpeler2++;

        if (scherm.kleurSpeler2 == scherm.kleurSpeler1)
        {
            if (scherm.kleurSpeler2 == scherm.kleuren.Count() - 1) scherm.kleurSpeler2 = 0;
            else scherm.kleurSpeler2++;
        }
    }

    // Update het bord
    UpdateBord();
}

void UpdateBord(object sender = null, EventArgs ea = null) // Op moment dat het scherm een ander formaat krijgt worden alle elementen geupdate
{
    // Herbereken alle plekken en grote van de elementen
    scherm.UpdateElementen();
    scherm.UpdateBeurt(gameEngine.speler);
    SpelVoorbij();

    // Voor alle posities die al een steen hebben plaats deze steen terug voor player 1 en 2
    foreach ((int x, int y) in gameEngine.PositiesSpeler(1))
    {
        scherm.VulHokje(x, y, scherm.kleuren[scherm.kleurSpeler1].Item1);
    }

    foreach ((int x, int y) in gameEngine.PositiesSpeler(2))
    {
        scherm.VulHokje(x, y, scherm.kleuren[scherm.kleurSpeler2].Item1);
    }
}

void Help(object sender, EventArgs ea) // Help de speler en tekent de mogelijke zetten
{
    scherm.TekenMogelijkeZetten(gameEngine.mogelijkeZetten);
}

void Computer() // Voert de zet uit van de computer
{
    // Controleer of de computer aan de beurt is
    if (gameEngine.computer && gameEngine.speler == 2 && gameEngine.mogelijkeZetten.Any())
    {
        // Ververs het scherm om te laten zien dat de computer aan de beurt is
        scherm.Refresh();

        // Wacht om te doen alsof de computer nadenkt
        Thread.Sleep(1000);

        // Haal de zet op van de computer die door de engine wordt berekend.
        (int, int) zet = gameEngine.ZetComputer();

        // Verwerk de zet
        scherm.VulHokje(zet.Item1, zet.Item2, scherm.kleuren[scherm.kleurSpeler2].Item1);
        gameEngine.bord[zet.Item1, zet.Item2] = 2;
        VeranderStenen(gameEngine.IngeslotenStenen(zet.Item1, zet.Item2));
        gameEngine.speler--;

        // Update de mogelijke zetten, score en wie er aan de beurt is
        gameEngine.updateMogelijkeZetten();
        scherm.UpdateScore(gameEngine.AantalStenen(1), gameEngine.AantalStenen(2));
        scherm.UpdateBeurt(gameEngine.speler);
    }
}

void Klik(object sender, MouseEventArgs mea) // Als de gebruiker klikt op het bord
{
    // Bereken de x en y coördinaten voor de engine
    int x = (mea.X - 2) / scherm.grootteHokje;
    int y = (mea.Y - 2) / scherm.grootteHokje;

    // Als het hokje leeg is en het een mogelijke zet is
    if (gameEngine.bord[x, y] == 0 && gameEngine.mogelijkeZetten.Contains((x, y)))
    {
        // Als de gebruiker hulp nodig had verwijder de hulp
        scherm.VerwijderMogelijkeZetten(gameEngine.mogelijkeZetten);

        // Verwerk de zet van speler 1 of speler 2
        if (gameEngine.speler == 1)
        {
            scherm.VulHokje(x, y, scherm.kleuren[scherm.kleurSpeler1].Item1);
            gameEngine.bord[x, y] = 1;
            VeranderStenen(gameEngine.IngeslotenStenen(x, y));
            gameEngine.speler++;
        }
        else
        {
            scherm.VulHokje(x, y, scherm.kleuren[scherm.kleurSpeler2].Item1);
            gameEngine.bord[x, y] = 2;
            VeranderStenen(gameEngine.IngeslotenStenen(x, y));
            gameEngine.speler--;
        }

        // Geeft aan dat er geen oudhokje meer is.
        oudHokje = (-1, -1);

        // Update de engine en het scherm
        gameEngine.updateMogelijkeZetten();
        scherm.UpdateScore(gameEngine.AantalStenen(1), gameEngine.AantalStenen(2));
        scherm.UpdateBeurt(gameEngine.speler);

        // Doe de zet van de computer
        Computer();

        // Controleer of het spel voorbij is
        SpelVoorbij();
    }
}

void VeranderStenen(List<(int x, int y)> stenen) // Veranderd stenen van kleur
{
    foreach ((int, int) steen in stenen)
    {
        // Verkrijg van welke speler de steen is
        int steenSpeler = gameEngine.bord[steen.Item1, steen.Item2];

        // Als de steen van speler 1 verander de steen naar speler 2 en vice versa
        if (steenSpeler == 1)
        {
            scherm.VeranderSteen(steen.Item1, steen.Item2, scherm.kleuren[scherm.kleurSpeler2].Item1);
            gameEngine.bord[steen.Item1, steen.Item2] = 2;
        }
        else if (steenSpeler == 2)
        {
            scherm.VeranderSteen(steen.Item1, steen.Item2, scherm.kleuren[scherm.kleurSpeler1].Item1);
            gameEngine.bord[steen.Item1, steen.Item2] = 1;
        }
    }
}

void BeweegMuis(object sender, MouseEventArgs mea) // Als de muis beweegt over het bord
{
    // Als er mogelijke zetten zijn
    if (gameEngine.mogelijkeZetten.Any())
    {
        // Bereken de positie voor de engine
        int x = (mea.X - 2) / scherm.grootteHokje;
        int y = (mea.Y - 2) / scherm.grootteHokje;

        // Als het oude hokje geen zet was vul het hokje met de achtergrondkleur.
        if (oudHokje.Item1 != -1)
        {
            scherm.VulHokje(oudHokje.Item1, oudHokje.Item2, Color.LightGray, Convert.ToInt32(scherm.grootteHokje * 0.3));
        }

        // Als het een leeg hokje is zet er een rondje van de huide speler in om aan te geven welk hokje de muisaanwijzer selecteerd.
        if (gameEngine.bord[x, y] == 0)
        {
            oudHokje = (x, y);

            Color kleur;

            if (gameEngine.speler == 1) kleur = scherm.kleuren[scherm.kleurSpeler1].Item1;
            else kleur = scherm.kleuren[scherm.kleurSpeler2].Item1;

            scherm.VulHokje(x, y, kleur, Convert.ToInt32(scherm.grootteHokje * 0.3));
        }
    }
}

void SpelVoorbij()
{
    // Controleer of het spel voorbij is
    if (!gameEngine.mogelijkeZetten.Any() && gameEngine.AantalStenen(1) != 0 && gameEngine.AantalStenen(2) != 0)
    {
        // Controleer wie er heeft gewonnen en update de winnaar
        if (gameEngine.AantalStenen(1) > gameEngine.AantalStenen(2))
        {
            scherm.UpdateWinnaar(1);
        }
        else if (gameEngine.AantalStenen(1) < gameEngine.AantalStenen(2))
        {
            scherm.UpdateWinnaar(2);
        }
        else if (gameEngine.AantalStenen(1) == gameEngine.AantalStenen(2))
        {
            scherm.UpdateWinnaar(0);
        }
    }
}
