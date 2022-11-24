using System.Windows.Forms;
using System.Drawing;

Form scherm = new Form();
scherm.Text = "Mandelbrot";
scherm.BackColor = Color.LightCyan;
scherm.ClientSize = new Size(600, 800);

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

xLabel.Location = new Point(10, 10);
xLabel.Text = "midden x:";
xLabel.Size = new Size(200, 50);
xLabel.Font = new Font("Arial", 20, FontStyle.Regular);

xTextBox.Location = new Point(220, 10);
xTextBox.Size = new Size(300, 50);
xTextBox.Font = new Font("Arial", 20, FontStyle.Regular);

yLabel.Location = new Point(10, 60);
yLabel.Text = "midden y:";
yLabel.Size = new Size(200, 50);
yLabel.Font = new Font("Arial", 20, FontStyle.Regular);

yTextBox.Location = new Point(220, 60);
yTextBox.Size = new Size(300, 50);
yTextBox.Font = new Font("Arial", 20, FontStyle.Regular);

schaalLabel.Location = new Point(10, 110);
schaalLabel.Text = "schaal:";
schaalLabel.Size = new Size(200, 50);
schaalLabel.Font = new Font("Arial", 20, FontStyle.Regular);

schaalTextBox.Location = new Point(220, 110);
schaalTextBox.Size = new Size(300, 50);
schaalTextBox.Font = new Font("Arial", 20, FontStyle.Regular);

maxLabel.Location = new Point(10, 160);
maxLabel.Text = "max aantal:";
maxLabel.Size = new Size(200, 50);
maxLabel.Font = new Font("Arial", 20, FontStyle.Regular);

maxTextBox.Location = new Point(220, 160);
maxTextBox.Size = new Size(150, 50);
maxTextBox.Font = new Font("Arial", 20, FontStyle.Regular);

goButton.Location = new Point(380, 160);
goButton.Size = new Size(139, 37);
goButton.Text = "Go!";
goButton.Font = new Font("Arial", 20, FontStyle.Regular);

grid.Location = new Point(100, 325);
grid.Size = new Size(400, 400);
grid.BackColor = Color.White;
grid.Image = plaatje;

pic1Button.Location = new Point(65, 210);
pic1Button.Size = new Size(150, 40);
pic1Button.Text = "Leaf";
pic1Button.Font = new Font("Arial", 20, FontStyle.Regular);

pic2Button.Location = new Point(225, 210);
pic2Button.Size = new Size(150, 40);
pic2Button.Text = "Swirl";
pic2Button.Font = new Font("Arial", 20, FontStyle.Regular);

pic3Button.Location = new Point(385, 210);
pic3Button.Size = new Size(150, 40);
pic3Button.Text = "Eye";
pic3Button.Font = new Font("Arial", 20, FontStyle.Regular);

int berekenMandelgetal(double x, double y, int maxAantal)
{
    double a = 0;
    double b = 0;
    int mandelgetal = 0;
    double lengte = 0;

    while (lengte <= 2 && mandelgetal < maxAantal)
    {
        double tempA = a * a - b * b + x;
        b = 2 * a * b + y;
        a = tempA;

        lengte = Math.Sqrt(a * a + b * b);

        mandelgetal++;
    }

    return mandelgetal;
}

void go(object o, EventArgs e)
{
    double middenX = Double.Parse(xTextBox.Text.Replace(".", ","));
    double middenY = Double.Parse(yTextBox.Text.Replace(".", ","));
    double schaal = Double.Parse(schaalTextBox.Text.Replace(".", ","));
    int maxAantal = int.Parse(maxTextBox.Text.Replace(".", ","));
    Dictionary<int, int[]> colors = new Dictionary<int, int[]>();

    for (int x=0; x<=399; x++)
    {
        for (int y=0; y<=399; y++) 
        {
            int mandelgetal = berekenMandelgetal((x - 199) * schaal + middenX, (y - 199) * schaal - middenY, maxAantal);
            if (mandelgetal % 2 != 0)
            {
                plaatje.SetPixel(x, y, Color.Black);
            }
            else
            {
                int r, g, b;
                if (colors.ContainsKey(mandelgetal))
                {
                    r = colors[mandelgetal][0];
                    b = colors[mandelgetal][1];
                    g = colors[mandelgetal][2];
                }
                else
                {   
                    double H = Convert.ToDouble(mandelgetal)/ Convert.ToDouble(maxAantal) * 359;
                    double S = 1;
                    double L = 0.5;
                    double C = (1 - Math.Abs(2 * L - 1)) * S;
                    double X = C * (1 - Math.Abs((H / 60) % 2 - 1));
                    double M = L - C / 2;
                    double Rtemp = 0, Gtemp = 0, Btemp = 0;

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
                    colors.Add(mandelgetal, new[] { r, g, b });
                }

                plaatje.SetPixel(x, y, Color.FromArgb(r, g, b));
            }
            grid.Invalidate();
        }
    }
}

void muisKlik(object o, MouseEventArgs ea)
{
    double middenX = Double.Parse(xTextBox.Text.Replace(".", ","));
    double middenY = Double.Parse(yTextBox.Text.Replace(".", ","));
    double schaal = Double.Parse(schaalTextBox.Text.Replace(".", ","));

    double x = (ea.X - 199) * schaal + middenX;
    double y = (199 - ea.Y) * schaal + middenY;

    if (ea.Button == MouseButtons.Left)
    {
        schaal = schaal / 2;
    } 
    else if (ea.Button == MouseButtons.Right)
    {
        schaal = schaal * 2;
    }

    xTextBox.Text = x.ToString().Replace(",", ".");
    yTextBox.Text = y.ToString().Replace(",", ".");
    schaalTextBox.Text = schaal.ToString().Replace(",", ".");

    go(o, ea);
}

void pic1(object o, EventArgs e)
{
    xTextBox.Text = "-1.158984375";
    yTextBox.Text = "0.2849951171875";
    schaalTextBox.Text = "4.8828125E-06";
    maxTextBox.Text = "150";

    go(0, e);
}
void pic2(object o, EventArgs e)
{
    xTextBox.Text = "0.10681732177734377";
    yTextBox.Text = "0.6373706054687501";
    schaalTextBox.Text = "1.220703125E-06";
    maxTextBox.Text = "200";

    go(0, e);
}
void pic3(object o, EventArgs e)
{
    xTextBox.Text = "-1.249838485717773";
    yTextBox.Text = "0.04097122192382814";
    schaalTextBox.Text = "1.220703125E-06";
    maxTextBox.Text = "600";

    go(0, e);
}

goButton.Click += go;
pic1Button.Click += pic1;
pic2Button.Click += pic2;
pic3Button.Click += pic3;
grid.MouseClick += muisKlik;


Application.Run(scherm);