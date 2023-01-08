public class Scherm : Form
{   
    // Variabelen die nodig zijn om het scherm te laten werken
    public int grootteHokje;

    int breedte;
    int hokjes;

    // UI elementen, sommige zijn public omdat ze acties uitvoeren in of een gevolg zijn van de engine
    public Label scoreSpeler1;
    public Label scoreSpeler2;
    Label notificatie;
    Label label1;
    Label label2;
    public Label bord;
    public Label grootteBord;

    public Button nieuwSpel;
    public Button helpKnop;
    public Button verkleinBord;
    public Button vergrootBord;

    public RadioButton speler;
    public RadioButton computer;

    public int kleurSpeler1 = 0;
    public int kleurSpeler2 = 6;

    // Lijst met kleuren om de gebruiker een kleur te laten kiezen
    public List<(Color, string)> kleuren = new List<(Color, string)>()
    {
        (Color.Red, "Rood"),
        (Color.Yellow, "Geel"),
        (Color.DarkOrange, "Oranje"),
        (Color.DarkGreen, "Groen"),
        (Color.Blue, "Blauw"),
        (Color.DarkCyan, "Cyaan"),
        (Color.DarkViolet, "Violet"),
        (Color.DarkTurquoise, "Turquoise"),
        (Color.Brown, "Bruin"),
        (Color.Lime, "Limoen")
    };


    Graphics gr;
    public Scherm(int aantalHokjes) // Wordt uitgevoerd als het scherm wordt aangeroepen
    {
        // Eigenschappen van het scherm
        Text = "Reversi";
        ClientSize = new Size(800, 600);
        BackColor = Color.LightSeaGreen;
        MinimumSize = new Size(400, 300);
        MinimizeBox = false;

        // Maak een label bord en voeg deze toe aan de control
        bord = new Label(); Controls.Add(bord);

        // Maak het bord aan de hand van het aantal hokjes
        MaakBord(aantalHokjes);

        // Maak alle overige UI elementen aan
        scoreSpeler1 = new Label(); Controls.Add(scoreSpeler1);        
        scoreSpeler1.Text = "0";
        scoreSpeler1.TextAlign = ContentAlignment.MiddleCenter;

        scoreSpeler2 = new Label(); Controls.Add(scoreSpeler2);
        scoreSpeler2.Text = "0";
        scoreSpeler2.TextAlign = ContentAlignment.MiddleCenter;

        notificatie = new Label(); Controls.Add(notificatie);
        notificatie.Text = "Klik op je score om de kleur te veranderen";
        notificatie.TextAlign = ContentAlignment.MiddleCenter;

        label1 = new Label(); Controls.Add(label1);
        label1.Text = "Kies tegenstander:";

        speler = new RadioButton(); Controls.Add(speler);
        speler.Text = "Speler";
        speler.Select();

        computer = new RadioButton(); Controls.Add(computer);
        computer.Text = "Computer";

        label2 = new Label(); Controls.Add(label2);
        label2.Text = "Grootte bord:";
        

        verkleinBord = new Button(); Controls.Add(verkleinBord);
        verkleinBord.Text = "<";
        verkleinBord.BackColor = Color.LightGray;

        grootteBord = new Label(); Controls.Add(grootteBord);
        grootteBord.Text = "6 × 6";
        grootteBord.TextAlign = ContentAlignment.MiddleCenter;
        grootteBord.BackColor = Color.LightGray;

        vergrootBord = new Button(); Controls.Add(vergrootBord);
        vergrootBord.Text = ">";
        vergrootBord.BackColor = Color.LightGray;

        nieuwSpel = new Button(); Controls.Add(nieuwSpel);
        nieuwSpel.Text = "Nieuw spel";
        nieuwSpel.BackColor = Color.LightGray;

        helpKnop = new Button(); Controls.Add(helpKnop);
        helpKnop.Text = "Help";
        helpKnop.BackColor = Color.LightGray;

        // Update alle elementen
        UpdateElementen();
    }

    public void UpdateElementen() // Dit update alle elementen aan de hand van de grootte van het scherm
    {
        // Maak het bord
        MaakBord(hokjes);

        // Het volgende is allemaal op schaal. Ik heb een plaatje met de schaal voor elk element, maar staat niet in de code
        // Maak een plaatje voor de score van speler 1 en 2
        Bitmap speler1 = new Bitmap(bord.Width / 6, bord.Width / 6);
        Graphics grSpeler1 = Graphics.FromImage(speler1);
        grSpeler1.FillEllipse(new SolidBrush(kleuren[kleurSpeler1].Item1), 0, 0, bord.Width / 6, bord.Width / 6);

        int scoreBreedte = bord.Width / 6 + bord.Width / 60;

        scoreSpeler1.Location = new Point(bord.Location.X, bord.Location.Y - scoreBreedte);
        scoreSpeler1.Size = new Size(scoreBreedte, scoreBreedte);
        scoreSpeler1.Image = speler1;
        scoreSpeler1.Font = new Font("Arial", scoreSpeler1.Size.Height / 5 * 2, FontStyle.Bold);

        Bitmap speler2 = new Bitmap(bord.Width / 6, bord.Width / 6);
        Graphics grSpeler2 = Graphics.FromImage(speler2);
        grSpeler2.FillEllipse(new SolidBrush(kleuren[kleurSpeler2].Item1), 0, 0, bord.Width / 6, bord.Width / 6);

        scoreSpeler2.Location = new Point(bord.Location.X + bord.Width - scoreBreedte, bord.Location.Y - scoreBreedte);
        scoreSpeler2.Size = new Size(scoreBreedte, scoreBreedte);
        scoreSpeler2.Image = speler2;
        scoreSpeler2.Font = new Font("Arial", scoreSpeler2.Size.Height / 5 * 2, FontStyle.Bold);

        // De keuze tussen speler of computer
        label1.Location = new Point((this.Width - 39) / 4 * 3, (this.Height - 39) / 2 / 12);
        label1.AutoSize = true;

        // Dit is een manier om het lettertype zo te zetten dat het de volledige grootte van het element benut
        while (label1.Height > (this.Height - 39) / 2 / 8 || label1.Width > (this.Width - 39) / 4) label1.Font = new Font("Arial", label1.Font.Size - 1, FontStyle.Bold);
        while (label1.Height < (this.Height - 39) / 2 / 8 && label1.Width < (this.Width - 39) / 4) label1.Font = new Font("Arial", label1.Font.Size + 1, FontStyle.Bold);

        speler.Location = new Point((this.Width - 39) / 4 * 3 + label1.Width / 8, label1.Height + label1.Location.Y + (this.Height - 39) / 2 / 24);
        speler.AutoSize = true;

        while (speler.Height > (this.Height - 39) / 2 / 10 || speler.Width > label1.Width - label1.Width / 10 || speler.Font.Size + 1 > label1.Font.Size) speler.Font = new Font("Arial", speler.Font.Size - 1, FontStyle.Regular);
        while (speler.Height < (this.Height - 39) / 2 / 10 && speler.Width < label1.Width - label1.Width / 10 && speler.Font.Size + 1 < label1.Font.Size) speler.Font = new Font("Arial", speler.Font.Size + 1, FontStyle.Regular);

        computer.Location = new Point((this.Width - 39) / 4 * 3 + label1.Width / 8, speler.Height + speler.Location.Y + (this.Height - 39) / 2 / 48);
        computer.Font = speler.Font;
        computer.AutoSize = true;

        // Een label voor alle notificaties
        notificatie.Location = new Point(bord.Location.X + scoreBreedte, 0);
        notificatie.Size = new Size(bord.Width - 2 * scoreBreedte, (this.Height - 39) / 5);
        notificatie.Font = new Font("Arial", label1.Font.Size - 1, FontStyle.Bold);


        // De grootte van het bord
        label2.Location = new Point((this.Width - 39) / 4 * 3, computer.Height + computer.Location.Y + (this.Height - 39) / 2 / 24);
        label2.Font = label1.Font;
        label2.AutoSize = true;

        grootteBord.Location = new Point((this.Width - 39) / 4 * 3 + label2.Width / 8 + (this.Width - 39) / 24, label2.Height + label2.Location.Y + (this.Height - 39) / 48);
        grootteBord.AutoSize = true;

        while (grootteBord.Height > (this.Height - 39) / 2 / 10 || grootteBord.Font.Size + 1 > label2.Font.Size) grootteBord.Font = new Font("Arial", grootteBord.Font.Size - 1, FontStyle.Regular);
        while (grootteBord.Height < (this.Height - 39) / 2 / 10 && grootteBord.Font.Size + 1 < label2.Font.Size) grootteBord.Font = new Font("Arial", grootteBord.Font.Size + 1, FontStyle.Regular);

        int hoogte = grootteBord.Height;
        grootteBord.AutoSize = false;
        grootteBord.Size = new Size(label2.Width - label2.Width / 5 - label2.Width / 4, hoogte);

        verkleinBord.Location = new Point(grootteBord.Location.X - grootteBord.Height, grootteBord.Location.Y);
        verkleinBord.Font = grootteBord.Font;
        verkleinBord.Size = new Size(grootteBord.Height, grootteBord.Height);

        vergrootBord.Location = new Point(grootteBord.Location.X + grootteBord.Width, grootteBord.Location.Y);
        vergrootBord.Font = grootteBord.Font;
        vergrootBord.Size = new Size(grootteBord.Height, grootteBord.Height);

        // Button voor een nieuw spel
        nieuwSpel.Location = new Point((this.Width - 39) / 4 * 3, verkleinBord.Location.Y + verkleinBord.Height + (this.Height - 39) / 2 / 12);
        nieuwSpel.Font = label1.Font;
        nieuwSpel.Size = new Size((this.Width - 39) / 32 * 7, label1.Height);

        // Button voor hulp
        helpKnop.Location = new Point((this.Width - 39) / 4 * 3, nieuwSpel.Location.Y + nieuwSpel.Height + (this.Height - 39) / 2 / 12);
        helpKnop.Font = label1.Font;
        helpKnop.Size = new Size((this.Width - 39) / 32 * 7, label1.Height);
    }

    public void UpdateScore(int speler1, int speler2) // Update de UI met de parameters voor speler 1 en 2
    {
        scoreSpeler1.Text = speler1.ToString();
        scoreSpeler2.Text = speler2.ToString();
    }

    public void UpdateBeurt(int speler) // Update de UI aan de hand van de huidige speler aan de beurt
    {
        if (speler == 1) notificatie.Text = $"{kleuren[kleurSpeler1].Item2} is aan zet";
        else if (speler == 2) notificatie.Text = $"{kleuren[kleurSpeler2].Item2} is aan zet";
        
    }

    public void UpdateWinnaar(int speler) // Update de UI aan de hand van de speler die heeft gewonnen
    {
        if (speler == 1) notificatie.Text = $"{kleuren[kleurSpeler1].Item2} wint!!";
        if (speler == 2) notificatie.Text = $"{kleuren[kleurSpeler2].Item2} wint!!";
        if (speler == 0) notificatie.Text = "Remise";
    }

    public void MaakBord(int aantalHokjes) // Maak het bord met hokjes aan
    {
        // Dit zorgt dat alle functies in Scherm deze hokjes kunnen gebruiken zonder parameter
        hokjes = aantalHokjes;

        // Dit is de maximale hoogte en breedte van het spel. Dit geld voor alleen het bord en er komen nog marges bij
        int hoogteSpel = (this.Height - 39) / 10 * 7;
        int breedteSpel = (this.Width - 39) / 2;

        // De maximale breedte / hoogte is het kleinste getal tussen hoogte- en breedteSpel
        int maxBreedteBord;

        if (hoogteSpel < breedteSpel) maxBreedteBord = hoogteSpel;
        else maxBreedteBord = breedteSpel;

        // Bereken de grootte van het hokje en de breedte van het bord. Dit zorgt dat er geen out of index ontstaat.
        for (int i = maxBreedteBord; i * hokjes > maxBreedteBord; i--)
        {
            grootteHokje = i - 1;
            breedte = grootteHokje * hokjes + 2;
        }

        // Pas het bord aan
        bord.Size = new Size(breedte, breedte);
        bord.Location = new Point((this.Width - 39) / 8, (this.Height - 39) / 5);
        bord.BackColor = Color.LightGray;

        // Maak het plaatje met alle strepen die de hokjes vormen
        Bitmap plaatje = new Bitmap(breedte, breedte);
        gr = Graphics.FromImage(plaatje);

        for (int i = 1; i <= breedte; i += grootteHokje)
        {
            gr.DrawLine(new Pen(Color.Black, 2), 0, i, breedte, i);
        }

        for (int i = 1; i <= breedte; i += grootteHokje)
        {
            gr.DrawLine(new Pen(Color.Black, 2), i, 0, i, breedte);
        }

        bord.Image = plaatje;
    }

    public void NieuwSpel(int aantalHokjes, int startSpeler) // Start een nieuw spel
    {
        // Maak een leeg bord
        MaakBord(aantalHokjes);

        // Reset de notificatie text
        notificatie.Text = "";

        // Vul de middelste hokjes
        VulHokje(hokjes / 2 - 1, hokjes / 2 - 1, kleuren[kleurSpeler1].Item1);
        VulHokje(hokjes / 2, hokjes / 2 - 1, kleuren[kleurSpeler2].Item1);
        VulHokje(hokjes / 2 - 1, hokjes / 2, kleuren[kleurSpeler2].Item1);
        VulHokje(hokjes / 2, hokjes / 2, kleuren[kleurSpeler1].Item1);

        // Update de score en de speler die aan de beurt is
        UpdateScore(2, 2);
        UpdateBeurt(startSpeler);
    }

    public void VulHokje(int x, int y, Color kleur, int diameter = 0) // Functie om een gegeven hokje te vullen met een ellips met de gegeven kleur en met als optie de diameter van de ellips
    {
        // Als er geen diameter is gegeven dan wordt de diameter aan de hand van het hokje berekend
        if (diameter == 0) diameter = Convert.ToInt32(grootteHokje * 0.6);

        // Bereken het beginpunt van de ellips
        int xBegin = x * grootteHokje + grootteHokje / 2 - diameter / 2;
        int yBegin = y * grootteHokje + grootteHokje / 2 - diameter / 2;

        // Vul het hokje
        gr.FillEllipse(new SolidBrush(kleur), xBegin, yBegin, diameter, diameter);

        // Ververs het bord
        bord.Invalidate();
    }

    public void VeranderSteen(int x, int y, Color kleur) // Verander de steen naar de gegeven kleur
    {
        // De diameter hangt af van de grootte van het hokje
        int diameter = Convert.ToInt32(grootteHokje * 0.6);

        // De x en y coördinaat van het begin
        int xBegin = x * grootteHokje + grootteHokje / 2 - diameter / 2;
        int yBegin = y * grootteHokje + grootteHokje / 2 - diameter / 2;

        // Speel een animatie voor het omdraaien van de stenen
        int l = 1;

        for (double k = -4; k < 5; k++)
        {
            if (k != 0)
            {
                // Pauzeer om het een niet te snelle animatie te maken
                Thread.Sleep(50);

                // Maak het vakje leeg
                gr.FillEllipse(new SolidBrush(Color.LightGray), xBegin, yBegin, diameter, diameter);

                // Bereken de diameter met k voor het omdraaien van de steen
                int nieuwDiameter = Convert.ToInt32(Math.Abs(diameter * k / 4));

                // Bereken de breedte van het rechter gedeelte van de ellips met l 
                int breedteRechts = Convert.ToInt32(nieuwDiameter * l / 10);

                // Maak de plaatjes links en rechts
                Bitmap links = new Bitmap(nieuwDiameter - breedteRechts, diameter);
                Graphics grLinks = Graphics.FromImage(links);

                Bitmap rechts = new Bitmap(breedteRechts, diameter);
                Graphics grRechts = Graphics.FromImage(rechts);

                // Maak het rechter en linker gedeelte van de ellips
                grRechts.FillEllipse(new SolidBrush(kleur), breedteRechts - nieuwDiameter, 0, nieuwDiameter, diameter);

                if (kleur == kleuren[kleurSpeler2].Item1) grLinks.FillEllipse(new SolidBrush(kleuren[kleurSpeler1].Item1), 0, 0, nieuwDiameter, diameter);
                else grLinks.FillEllipse(new SolidBrush(kleuren[kleurSpeler2].Item1), 0, 0, nieuwDiameter, diameter);

                // Teken het het linker en rechter plaatje in de hokje
                gr.DrawImage(links, new Point(xBegin + (diameter - nieuwDiameter) / 2, yBegin));
                gr.DrawImage(rechts, new Point(xBegin + (diameter - nieuwDiameter) / 2 + links.Width, yBegin));

                // Ververs het bord
                bord.Refresh();
            }

            l++;
        }

        // Vul de gehele ellips met de nieuwe kleur
        gr.FillEllipse(new SolidBrush(kleur), xBegin, yBegin, diameter, diameter);

        // Ververs het bord
        bord.Refresh();
    }

    public void TekenMogelijkeZetten(List<(int, int)> zetten) // Teken de mogelijke zetten
    {
        // De diameter afhankelijk van de grootte van het hokje
        int diameter = Convert.ToInt32(grootteHokje * 0.4);

        // Bereken de x en y coördinaten van het beginpunt en teken een ellips zonder vulling
        foreach ((int, int) punt in zetten)
        {
            int xBegin = punt.Item1 * grootteHokje + grootteHokje / 2 - diameter / 2;
            int yBegin = punt.Item2 * grootteHokje + grootteHokje / 2 - diameter / 2;
            
            gr.DrawEllipse(new Pen(Color.Black, 2), xBegin, yBegin, diameter, diameter);
        }

        // Ververs het bord
        bord.Invalidate();
    }

    public void VerwijderMogelijkeZetten(List<(int, int)> zetten) // Verwijder de mogelijke zetten
    {
        // De diameter afhankelijk van de grootte van het hokje
        int diameter = Convert.ToInt32(grootteHokje * 0.4);

        // Bereken de x en y coördinaten van het beginpunt en verwijder de ellips
        foreach ((int, int) punt in zetten)
        {
            int xBegin = punt.Item1 * grootteHokje + grootteHokje / 2 - diameter / 2;
            int yBegin = punt.Item2 * grootteHokje + grootteHokje / 2 - diameter / 2;

            gr.DrawEllipse(new Pen(Color.LightGray, 2), xBegin, yBegin, diameter, diameter);
        }

        // Ververs het bord
        bord.Invalidate();
    }
}
