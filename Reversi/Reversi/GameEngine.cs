public class Engine
{
    // Variabelen van de engine
    public bool computer;

    public int speler = 0;
    public int hokjes = 6;
    public int[,] bord;
    public int[,] tempBord;

    public List<(int, int)> mogelijkeZetten = new List<(int, int)>();

    // De richtingen waarin gecontroleerd wordt of er een mogelijke zet is of welke stenen ingesloten zijn
    List<(int, int)> richtingen = new List<(int, int)>()
    {
        (1,0), (1,1), (0,1), (-1,1), (-1,0), (-1,-1), (0, -1), (1, -1)
    };

    public Engine() // Maakt de engine en het bord
    {
        bord = new int[hokjes, hokjes];
    }

    private int AndereSpeler() => speler == 1 ? 2 : 1; // Verkorte code om de andere speler te krijgen

    public void StartSpel(bool isTegenstanderComputer) // Start het spel
    {
        // Reset het spel
        bord = new int[hokjes, hokjes];

        for (int i = 0; i < hokjes; i++)
        {
            for (int j = 0; j < hokjes; j++)
            {
                bord[i, j] = 0;
            }
        }

        speler = 0;

        // Zet de stenen in het midden van het bord
        bord[bord.GetLength(0) / 2 - 1, bord.GetLength(0) / 2 - 1] = 1;
        bord[bord.GetLength(0) / 2, bord.GetLength(0) / 2 - 1] = 2;
        bord[bord.GetLength(0) / 2 - 1, bord.GetLength(0) / 2] = 2;
        bord[bord.GetLength(0) / 2, bord.GetLength(0) / 2] = 1;

        // Verander de status van de computer
        computer = isTegenstanderComputer;

        // Kies pseudo-random speler
        Random rnd = new Random();
        speler = rnd.Next(1, 3);

        updateMogelijkeZetten();
    }

    public List<(int, int)> PositiesSpeler(int speler) // Haal alle posities op van de gegeven speler
    {
        List<(int, int)> posities = new List<(int, int)>();

        for (int x = 0; x < bord.GetLength(1); x++)
        {
            for (int y = 0; y < bord.GetLength(0); y++)
            {
                if (bord[x, y] == speler)
                {
                    posities.Add((x, y));
                }
            }
        }
        return posities;
    }

    public void updateMogelijkeZetten() // Update de mogelijke zetten
    {
        mogelijkeZetten = new List<(int, int)>();

        // Voor elke positie in alle posities van de speler controleer of het een mogelijke zet is
        foreach ((int x, int y) in PositiesSpeler(speler))
        {
            foreach ((int, int) richting in richtingen)
            {
                // Bereken de afstand tot de rand zodat er geen out of index plaatsvind.
                int afstandRandX;
                int afstandRandY;

                if (richting.Item1 < 0) afstandRandX = x + 1;
                else if (richting.Item1 == 0) afstandRandX = bord.GetLength(1);
                else afstandRandX = bord.GetLength(1) - x;

                if (richting.Item2 < 0) afstandRandY = y + 1;
                else if (richting.Item2 == 0) afstandRandY = bord.GetLength(0);
                else afstandRandY = bord.GetLength(0) - y;

                for (int i = 1; i < afstandRandX && i < afstandRandY; i++)
                {
                    // Als de huidige coördinaat plus de richting niet een andere speler is dan is er in die richting geen mogelijke zet vanaf die steen
                    // Als dat niet is dan als voor het eerste lege vakje een steen van de andere speler is dan is het een geldige zet
                    if (bord[x + richting.Item1, y + richting.Item2] != AndereSpeler()) break;
                    else if (bord[x + i * richting.Item1, y + i * richting.Item2] == 0 && bord[x + (i - 1) * richting.Item1, y + (i - 1) * richting.Item2] == AndereSpeler())
                    {
                        // Als de mogelijkeZetten al deze zet bevat voeg deze dan niet toe
                        if (!mogelijkeZetten.Contains((x + i * richting.Item1, y + i * richting.Item2))) mogelijkeZetten.Add((x + i * richting.Item1, y + i * richting.Item2));

                        // Stop de for loop, want er zal geen mogelijke zet meer zijn op dit pad vanaf de steen met het x en y coördinaat
                        break;
                    }
                }
            }
        }
                   
    }

    public List<(int x, int y)> IngeslotenStenen(int x, int y) // Geef een lijst met alle ingelosten stenen vanaf een bepaalde steen
    {
        List<(int x, int y)> ingeslotenStenen = new List<(int, int)>();

        // Voor alle richtingen 
        foreach ((int, int) richting in richtingen)
        {
            // Bereken de afstand tot de rand
            int afstandRandX;
            int afstandRandY;

            if (richting.Item1 < 0) afstandRandX = x + 1;
            else if (richting.Item1 == 0) afstandRandX = bord.GetLength(1);
            else afstandRandX = bord.GetLength(1) - x;

            if (richting.Item2 < 0) afstandRandY = y + 1;
            else if (richting.Item2 == 0) afstandRandY = bord.GetLength(0);
            else afstandRandY = bord.GetLength(0) - y;

            for (int i = 1; i < afstandRandX && i < afstandRandY; i++)
            {
                // Als de steen plus de richting de steen van de speler is of als er in de lijn vanaf de steen een leeg vakje is dan stop de for loop
                // Als dit niet is dan als de steen plus een verlengde van de richting de speler is en de steen daarvoor een steen van de andere speler is dan zijn de stenen daartussen ingesloten
                if (bord[x + richting.Item1, y + richting.Item2] == speler || bord[x + i * richting.Item1, y + i * richting.Item2] == 0) break;
                else if (bord[x + i * richting.Item1, y + i * richting.Item2] == speler && bord[x + (i - 1) * richting.Item1, y + (i - 1) * richting.Item2] == AndereSpeler())
                {
                    // Voor elke ingesloten steen voeg deze aan de lijst toe als deze nog niet in de lijst zit
                    for (int k = 1; k < i; k++)
                    {
                        if (ingeslotenStenen.Contains((x + k * richting.Item1, y + k * richting.Item2)) == false) ingeslotenStenen.Add((x + k * richting.Item1, y + k * richting.Item2));                                    
                    }

                    // Stop de loop want er zijn geen stenen meer die zijn ingesloten hierna
                    break;
                }
            }
        }

        return ingeslotenStenen;
    }

    public int AantalStenen(int speler) // Haal het aantal stenen van de speler op
    {
        int aantalStenen = 0;
        
        foreach (int steen in bord)
        {
            if (steen == speler) aantalStenen++;
        }

        return aantalStenen;
    }

    public (int, int) ZetComputer() // Bepaal de zet van de computer. Dit is de zet met de meeste ingesloten stenen
    {
        int aantalStenen = 0;
        List<(int, int)> mogelijkeZettenComputer = new List<(int, int)>();

        foreach ((int, int) i in mogelijkeZetten)
        {
            // Als de zet de meeste ingesloten stenen geeft dan verander het aantal stenen naar deze waarde en wis de mogelijke zetten van de computer
            if (aantalStenen < IngeslotenStenen(i.Item1, i.Item2).Count)
            {
                aantalStenen = IngeslotenStenen(i.Item1, i.Item2).Count;

                mogelijkeZettenComputer.Clear();
            }

            // Als het aantal ingesloten stenen gelijk is voet dan deze steen toe aan de mogelijke zetten van de computer
            if (aantalStenen == IngeslotenStenen(i.Item1, i.Item2).Count)
            {
                mogelijkeZettenComputer.Add(i);
            }
        }

        // Als er meerdere mogelijke zetten zijn neem dan een pseudo-random zet
        Random random = new Random();
        int index = random.Next(mogelijkeZettenComputer.Count);

        return mogelijkeZettenComputer[index];
    }
}