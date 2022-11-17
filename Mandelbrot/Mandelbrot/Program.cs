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
TextBox xTextBox = new TextBox();
TextBox yTextBox = new TextBox();
TextBox schaalTextBox = new TextBox();
TextBox maxTextBox = new TextBox();
Button goButton = new Button();

scherm.Controls.Add(xLabel);
scherm.Controls.Add(yLabel);
scherm.Controls.Add(schaalLabel);
scherm.Controls.Add(maxLabel);
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

void bereken(object o, EventArgs e)
{
    goButton.Text = "";
}

goButton.Click += bereken;

Application.Run(scherm);