using System.Windows.Forms;
using System.Drawing;

Color.

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

grid.Location = new Point(100, 300);
grid.Size = new Size(400, 400);
grid.BackColor = Color.White;

int berekenMandelgetal(double x, double y, int maxAantal)
{
    double a = 0;
    double b = 0;
    int mandelgetal = 0;
    double lengte = 0;

    while (lengte <= 2 && mandelgetal < maxAantal)
    {
        double newA = a * a - b * b + x;
        double newB = 2 * a * b + y;

        lengte = Math.Sqrt(newA * newA + newB * newB);

        mandelgetal++;
        a = newA;
        b = newB;
    }

    return mandelgetal;
}

void go(object o, EventArgs e)
{
    double middenX = Double.Parse(xTextBox.Text.Replace(".", ","));
    double middenY = Double.Parse(yTextBox.Text.Replace(".", ","));
    double schaal = Double.Parse(schaalTextBox.Text.Replace(".", ","));
    int maxAantal = int.Parse(maxTextBox.Text.Replace(".", ","));
    Bitmap plaatje = new Bitmap(400, 400);
    grid.Image = plaatje;

    for (int x=0; x<=399; x++)
    {
        for (int y=0; y<=399; y++) 
        {
            int mandelgetal = berekenMandelgetal((x - 199) * schaal + middenX, (y - 199) * schaal - middenY, maxAantal);
            if (mandelgetal % 2 == 0)
            {
                plaatje.SetPixel(x, y, Color.Black);
            }
            else
            {
                int b = (255 * (mandelgetal / maxAantal) - 255) * -1;
                int g = (255 * (b / 255) - 255) * -1;
                int r = (255 * (g / 255) - 255) * -1;

                plaatje.SetPixel(x, y, );
            }
        }
    }
    grid.Invalidate();
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

goButton.Click += go;
grid.MouseClick += muisKlik;

Application.Run(scherm);