using System.Windows.Forms;
using System.Drawing;
using Mandelbrot;
using System.Globalization;

// Maak het scherm aan
Form scherm = new Form();
scherm.Text = "Mandelbrot";
scherm.BackColor = Color.LightCyan;
scherm.ClientSize = new Size(1200, 800);

// Definiëer de items in het scherm
Label xLabel = new Label();
Label yLabel = new Label();
Label schaalLabel = new Label();
Label maxLabel = new Label();
Label grid = new Label();
TextBox xTextBox = new TextBox();
TextBox yTextBox = new TextBox();
TextBox schaalTextBox = new TextBox();
TextBox maxTextBox = new TextBox();
Button goButton = new Button();
Bitmap plaatje = new Bitmap(400, 400);
Button pic1Button = new Button();
Button pic2Button = new Button();
Button pic3Button = new Button();
Label hueLabel = new Label();
TrackBar hueTrackBar = new TrackBar();
TrackBar saturationTrackBar = new TrackBar();
Label saturationLabel = new Label();
TrackBar brightnessTrackBar = new TrackBar();
Label brightnessLabel = new Label();


// Voeg de items aan het scherm toe
scherm.Controls.Add(xLabel);
scherm.Controls.Add(yLabel);
scherm.Controls.Add(schaalLabel);
scherm.Controls.Add(maxLabel);
scherm.Controls.Add(grid);
scherm.Controls.Add(xTextBox);
scherm.Controls.Add(yTextBox);
scherm.Controls.Add(schaalTextBox);
scherm.Controls.Add(maxTextBox);
scherm.Controls.Add(goButton);
scherm.Controls.Add(pic1Button);
scherm.Controls.Add(pic2Button);
scherm.Controls.Add(pic3Button);
scherm.Controls.Add(hueLabel);
scherm.Controls.Add(hueTrackBar);
scherm.Controls.Add(saturationLabel);
scherm.Controls.Add(saturationTrackBar);
scherm.Controls.Add(brightnessLabel);
scherm.Controls.Add(brightnessTrackBar);

// Voor elk item in het scherm geef de eigenschappen
xLabel.Location = new Point(10, 10);
xLabel.Text = "midden x:";
xLabel.Size = new Size(200, 50);
xLabel.Font = new Font("Arial", 20, FontStyle.Regular);

xTextBox.Location = new Point(220, 10);
xTextBox.Size = new Size(300, 50);
xTextBox.Font = new Font("Arial", 20, FontStyle.Regular);
xTextBox.Text = "0";

yLabel.Location = new Point(10, 60);
yLabel.Text = "midden y:";
yLabel.Size = new Size(200, 50);
yLabel.Font = new Font("Arial", 20, FontStyle.Regular);

yTextBox.Location = new Point(220, 60);
yTextBox.Size = new Size(300, 50);
yTextBox.Font = new Font("Arial", 20, FontStyle.Regular);
yTextBox.Text = "0";

schaalLabel.Location = new Point(10, 110);
schaalLabel.Text = "schaal:";
schaalLabel.Size = new Size(200, 50);
schaalLabel.Font = new Font("Arial", 20, FontStyle.Regular);

schaalTextBox.Location = new Point(220, 110);
schaalTextBox.Size = new Size(300, 50);
schaalTextBox.Font = new Font("Arial", 20, FontStyle.Regular);
schaalTextBox.Text = 0.01.ToString(CultureInfo.InvariantCulture);

maxLabel.Location = new Point(10, 160);
maxLabel.Text = "max aantal:";
maxLabel.Size = new Size(200, 50);
maxLabel.Font = new Font("Arial", 20, FontStyle.Regular);

maxTextBox.Location = new Point(220, 160);
maxTextBox.Size = new Size(150, 50);
maxTextBox.Font = new Font("Arial", 20, FontStyle.Regular);
maxTextBox.Text = "200";

goButton.Location = new Point(380, 160);
goButton.Size = new Size(139, 37);
goButton.Text = "Go!";
goButton.Font = new Font("Arial", 20, FontStyle.Regular);

grid.Location = new Point(100, 325);
grid.Size = new Size(400, 400);
grid.BackColor = Color.White;
grid.Image = plaatje;

pic1Button.Location = new Point(665, 10);
pic1Button.Size = new Size(150, 40);
pic1Button.Text = "Leaf";
pic1Button.Font = new Font("Arial", 20, FontStyle.Regular);

pic2Button.Location = new Point(825, 10);
pic2Button.Size = new Size(150, 40);
pic2Button.Text = "Swirl";
pic2Button.Font = new Font("Arial", 20, FontStyle.Regular);

pic3Button.Location = new Point(985, 10);
pic3Button.Size = new Size(150, 40);
pic3Button.Text = "Eye";
pic3Button.Font = new Font("Arial", 20, FontStyle.Regular);

hueLabel.Location = new Point(650, 60);
hueLabel.Size = new Size(500, 20);
hueLabel.Text = "Hue: 1/10";
hueLabel.Font = new Font("Arial", 10, FontStyle.Regular);

hueTrackBar.Location = new Point(650, 80);
hueTrackBar.Size = new Size(500, 40);
hueTrackBar.Minimum = 1;
hueTrackBar.Maximum = 10;
hueTrackBar.Value = 1;
hueTrackBar.Name = "hue";

saturationLabel.Location = new Point(650, 130);
saturationLabel.Size = new Size(500, 20);
saturationLabel.Text = "Saturation: 100/100";
saturationLabel.Font = new Font("Arial", 10, FontStyle.Regular);

saturationTrackBar.Location = new Point(650, 150);
saturationTrackBar.Size = new Size(500, 40);
saturationTrackBar.Minimum = 0;
saturationTrackBar.Maximum = 100;
saturationTrackBar.Value = 100;
saturationTrackBar.Name = "saturation";

brightnessLabel.Location = new Point(650, 200);
brightnessLabel.Size = new Size(500, 20);
brightnessLabel.Text = "Brightness: 50/100";
brightnessLabel.Font = new Font("Arial", 10, FontStyle.Regular);

brightnessTrackBar.Location = new Point(650, 220);
brightnessTrackBar.Size = new Size(500, 40);
brightnessTrackBar.Minimum = 0;
brightnessTrackBar.Maximum = 100;
brightnessTrackBar.Value = 50;
brightnessTrackBar.Name = "brightness";

// Globale variabelen voor de input van de gebruiker. Dit is handig als de gebruiker wil inzoemen en hij of zij per ongeluk de waardes had veranderd.
double middenX = 0, middenY = 0, schaal = 0.01;
int maxAantal = 200;

// Alle mogelijke inputs van de user
List<Char> mogelijkeInput = new List<Char>
{
    '0',
    '1',
    '2',
    '3',
    '4',
    '5',
    '6',
    '7',
    '8',
    '9',
    ',',
    '.',
    '-',
    'E'
};

int berekenMandelgetal(double x, double y, int maxAantal) // Functie om het mandelgetal te berekenen
{
    double a = 0;
    double b = 0;
    int mandelgetal = 0;
    double lengte = 0;

    while (lengte <= 2 && mandelgetal < maxAantal) // Zolang de lengte kleiner of gelijk is aan 2 dan voer de berekening uit voor het punt (a, b)
    {
        double tempA = a * a - b * b + x; // Maak een tijdelijke a aan zodat deze niet veranderd voordat b berekend wordt.
        b = 2 * a * b + y;
        a = tempA;

        lengte = Math.Sqrt(a * a + b * b);

        mandelgetal++;
    }

    return mandelgetal;
}

bool controleerInput(string input) // Deze functie controleerd of de gegeven input in het goede format is. 
{
    if (input == "") return false;

    int kommaPunt = 0;
    int aantalE = 0;
    for (int i=0; i < input.Length; i++)
    {
        char letter = input[i];
        if (letter == '-' && i != 0 && i != 1)
        {
            if (input[i - 1] == 'E')
            {
                if (input[i - 2] == ',' || input[i - 2] == '.') return false;
            }
            else return false;
        }

        if (letter == 'E')
        {
            if (input.EndsWith("E")) return false;
            else if (input[i + 1] != '-') return false;
        }

        if (input.EndsWith('-') || input.EndsWith(".") || input.EndsWith(",")) return false;

        if (mogelijkeInput.Contains(letter)) 
        {
            if (letter == ',' || letter == '.') kommaPunt++;
            if (letter == 'E') aantalE++;
        }
        else return false;
    }
    
    if (kommaPunt > 1 || aantalE > 1)
    {
        return false;
    }
    else
    {
        return true;
    }
}

void go(object o, EventArgs e) // Deze functie maakt de mandelbrot. 
{
    if (controleerInput(xTextBox.Text) && controleerInput(yTextBox.Text) && controleerInput(schaalTextBox.Text) && controleerInput(maxTextBox.Text))
    {
        // Als de gebruiker een . gebruikt in plaats van een komma dan moet deze omgezet worden naar een komma en daarna naar een double of int.
        middenX = Double.Parse(xTextBox.Text, CultureInfo.InvariantCulture);
        middenY = Double.Parse(yTextBox.Text, CultureInfo.InvariantCulture);
        schaal = Double.Parse(schaalTextBox.Text, CultureInfo.InvariantCulture);
        maxAantal = int.Parse(maxTextBox.Text, CultureInfo.InvariantCulture);



        // voor elke x en y waarde van het plaatje bereken het mandelgetal voor dat punt (x, y)
        for (int x = 0; x <= 399; x++)
        {
            for (int y = 0; y <= 399; y++)
            {
                // Om het mandelgetal te berekenen moet de x en y waarden omgezet worden naar de waarde in het figuur
                double mandelgetal = berekenMandelgetal((x - 199) * schaal + middenX, (y - 199) * schaal - middenY, maxAantal);

                // Kleur van de pixel instellen
                double hue = mandelgetal / Convert.ToDouble(maxAantal * hueTrackBar.Value) * 360;
                double saturation = Convert.ToDouble(saturationTrackBar.Value) / 100;
                double brightness = Convert.ToDouble(brightnessTrackBar.Value) / 100;

                var (r, g, b) = KleurOmzetten.hslNaarRgb(hue, saturation, brightness);

                plaatje.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
        }
        // Plaatje herladen
        grid.Invalidate();
    }
}

void muisKlik(object o, MouseEventArgs ea) // Deze functie zoemt in en uit
{
    middenX = (ea.X - 199) * schaal + middenX;
    middenY = (199 - ea.Y) * schaal + middenY;

    if (ea.Button == MouseButtons.Left) // Zoemt in
    {
        schaal = schaal / 2;
    } 
    else if (ea.Button == MouseButtons.Right) // Zoemt uit
    {
        schaal = schaal * 2;
    }

    xTextBox.Text = middenX.ToString(CultureInfo.InvariantCulture);
    yTextBox.Text = middenY.ToString(CultureInfo.InvariantCulture);
    schaalTextBox.Text = schaal.ToString(CultureInfo.InvariantCulture);

    go(o, ea);
}

void trackBarVeranderd (object o, EventArgs e) // Update de waarde die boven de trackbar staat
{
    string trackBar = ((TrackBar)o).Name;
    int waarde = ((TrackBar)o).Value;

    if (trackBar == "hue") hueLabel.Text = $"Hue: {waarde}/10";
    else if (trackBar == "saturation") saturationLabel.Text = $"Saturation: {waarde}/100";
    else if (trackBar == "brightness") brightnessLabel.Text = $"Brightness: {waarde}/100";
}

void textBoxVeranderd (object o, EventArgs e) // Maakt de textbox rood als er een verkeerde input is en wit als de input goed is.
{
    if (controleerInput(((TextBox)o).Text) == false) ((TextBox)o).BackColor = Color.Red;
    else ((TextBox)o).BackColor = Color.White;
}

// De functies die de preset toepassen
void pic1(object o, EventArgs e)
{
    hueTrackBar.Value = 1;
    saturationTrackBar.Value = 100;
    brightnessTrackBar.Value = 50;

    xTextBox.Text = "-1.158984375";
    yTextBox.Text = "0.2849951171875";
    schaalTextBox.Text = "4.8828125E-06";
    maxTextBox.Text = "150";

    go(o, e);
}
void pic2(object o, EventArgs e)
{
    hueTrackBar.Value = 1;
    saturationTrackBar.Value = 100;
    brightnessTrackBar.Value = 50;

    xTextBox.Text = "0.10681732177734377";
    yTextBox.Text = "0.6373706054687501";
    schaalTextBox.Text = "1.220703125E-06";
    maxTextBox.Text = "200";

    go(o, e);
}
void pic3(object o, EventArgs e)
{
    hueTrackBar.Value = 1;
    saturationTrackBar.Value = 100;
    brightnessTrackBar.Value = 50;

    xTextBox.Text = "-1.249838485717773";
    yTextBox.Text = "0.04097122192382814";
    schaalTextBox.Text = "1.220703125E-06";
    maxTextBox.Text = "600";

    go(o, e);
}

// Functies aanroepen als er een event gebeurd.
xTextBox.KeyUp += textBoxVeranderd;
yTextBox.KeyUp += textBoxVeranderd;
schaalTextBox.KeyUp += textBoxVeranderd;
maxTextBox.KeyUp += textBoxVeranderd;

hueTrackBar.ValueChanged += trackBarVeranderd;
saturationTrackBar.ValueChanged += trackBarVeranderd;
brightnessTrackBar.ValueChanged += trackBarVeranderd;

goButton.Click += go;
pic1Button.Click += pic1;
pic2Button.Click += pic2;
pic3Button.Click += pic3;
grid.MouseClick += muisKlik;

go(null, null);

Application.Run(scherm);