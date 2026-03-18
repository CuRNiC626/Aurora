using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Speech.Synthesis;

using GPT = LostTech.TensorFlow.GPT;

namespace Aurora
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            PreLoad(0);
            MainMenu();


            Render();
        }

        Rectangle bounds = new Rectangle();
        SpeechSynthesizer synth;

        public void PreLoad(int ScreenID)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;

            var screen = Screen.AllScreens[ScreenID];

            bounds = screen.Bounds;
            int w, h;

            w = bounds.Width;
            h = bounds.Height;

            graphics = CreateGraphics();
            Size = new Size(w, h);

            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();

            Speak();
        }

        List<FSControl> controls = new List<FSControl>();


        private bool isActive = false;

        private void MainMenu()
        {
            isActive = !isActive;

            myPath = new GraphicsPath(FillMode.Winding);

            if (isActive)
            {
                var button = new Button()
                {
                    Location = new Point(0, 0),
                    Size = new Size(200, 100),
                    Text = "AuRora"
                }.FastControl();

                controls.Add(button);

                LoadGPT();

                AddVisualObjects(controls.ToArray());
            }
            else
            {
                controls.Clear();
                Render();
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E) MainMenu();
        }

        Graphics graphics = null;


        public void AddVisualObjects(params FSControl[] controls)
        {
            var m = controls.Select(T => T.Location);
            var w = m.Where(T => T.X < 0 || T.Y < 0);


            RectangleF[] rectangleF;
            if (w == null || w.Count() == 0)
            {
                foreach (var c in controls)
                {
                    if (c.fType == FSControl.FigureType.Circle)
                    {
                        myPath.AddEllipse(c.Rectangle);
                    }
                    else if(c.fType == FSControl.FigureType.Square)
                    {
                        myPath.AddRectangle(c.Rectangle);
                    }
                }

                rectangleF = controls.Select(T => new RectangleF(T.Location.X, T.Location.Y, T.Size.Width, T.Size.Height)).ToArray();
            }
            else
            {
                var minX = w.Min(T => T.X);
                var minY = w.Min(T => T.Y);

                if (minX < 0) minX = -minX;
                else minX = 0;

                if (minY < 0) minY = -minY;
                else minY = 0;

                var temp = new List<RectangleF>();
                for (int i = 0; i < controls.Length; i++)
                {
                    var control = controls[i];
                    var location = control.Location;
                    var size = control.Size;

                    location = new Point(location.X + minX, location.Y + minY);


                    var rect = new RectangleF(location, size);
                    temp.Add(rect);

                    control.Location = location;
                }

                rectangleF = temp.ToArray();
            }


            

            Size = new Size(
                (int)rectangleF.Max(T => T.X + T.Width),
                (int)rectangleF.Max(T => T.Y + T.Height)
                );

            Location = new Point(
                bounds.Width - Size.Width,
                0
                );

            Controls.Clear();
            Controls.AddRange(controls.Select(T => T.control).ToArray());

            foreach (var c in controls)
            {
                c.Visible = false;
                c.Show();
            }


            Render();

            foreach (var c in controls)
                c.Visible = true;
        }

        GraphicsPath myPath = new GraphicsPath();
        public void Render()
        {
            Region myRegion = new Region(myPath);
            Region = myRegion;

            if (controls.Count == 0)
            {
                Size = new Size();
                Location = new Point();
                return;
            }

            


            graphics.Save();
            graphics.Flush();
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        public Prompt Speak()
        {

            return synth.SpeakAsync("Hello World");
        }

        public void LoadGPT()
        {
            
        }
    }

    public static class FastStyle
    {
        public static Color bColor = Color.DarkKhaki;

        public static FSControl FastControl(this Control control)
        {
            if (control is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                control = button;
            }

            control.ForeColor = Color.Black;
            control.AutoSize = false;
            control.BackColor = bColor;

            //Microsoft YaHei UI; 11,25pt; style=Bold
            control.Font = new Font("Microsoft YaHei ", 11.25f, FontStyle.Bold);

            return new FSControl(control);
        }

        public static FSControl Circle(this FSControl control)
        {
            GraphicsPath myPath = new GraphicsPath();
            var rect = new Rectangle(2, 2, control.Width - 3, control.Height - 3);

            control.Rectangle = rect;
            myPath.AddEllipse(rect);

            Region myRegion = new Region(myPath);
            control.Region = myRegion;
            control.fType = FSControl.FigureType.Circle;
            return control;
        }
    }

    public class FSControl
    {
        public FigureType fType;
        public Control control;

        public int Width => control.Width;
        public int Height => control.Width;

        public Region Region
        {
            get => control.Region;
            set => control.Region = value;
        }

        public Point Location
        {
            get => control.Location;
            set => control.Location = value;
        }

        public Size Size
        {
            get => control.Size;
            set => control.Size = value;
        }

        public bool Visible
        {
            get => control.Visible;
            set => control.Visible = value;
        }

        private RectangleF rect;
        private bool isRect;
        public RectangleF Rectangle
        {
            get
            {
                if (isRect == false) return control.ClientRectangle;
                else return rect;
            }
            set
            {
                rect = value;
                isRect = true;
            }
        }

        public void Show() => control.Show();

        public FSControl(Control control)
        {
            this.control = control;
            fType = FigureType.Square;
        }

        public enum FigureType
        {
            Circle,
            Square
        }
    }
}
