using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aurora.Components
{
    public class UI
    {
        Rectangle bounds = new Rectangle();
        Size Size;
        Point Location;

        private Graphics graphics;
        public UI(Graphics graphics)
        {
            this.graphics = graphics;

            PreLoad(0);
            Open();
        }

        private void PreLoad(int ScreenID)
        {
            var screen = Screen.AllScreens[ScreenID];
            bounds = screen.Bounds;

            int w, h;
            w = bounds.Width;
            h = bounds.Height;

            Size = new Size(w, h);
        }

        private bool isActive;
        Region myRegion;
        List<FSControl> controls = new List<FSControl>();

        public void Open()
        {
            isActive = !isActive;

            if (isActive)
            {
                var button = new Button()
                {
                    Location = new Point(0, 0),
                    Size = new Size(200, 100),
                    Text = "AuRora"
                }.FastControl();

                AddVisualObjects(button);
            }
            else Clear();
        }

        public void AddVisualObjects(params FSControl[] controls) 
        {
            foreach (var control in controls)
            {
                control.Visible = false;
                this.controls.Add(control);
            }    
        }
        public void RemoveVisualObjects(params FSControl[] controls) => this.controls.RemoveAll(T => controls.Contains(T));

        public void Clear() => controls.Clear();

        public void Render()
        {
            var gFrame = new GraphicsPath();
            var tControls = controls.ToArray();
            int count = tControls.Length;

            if (count != 0)
            {
                var m = tControls.Select(T => T.Location);
                var w = m.Where(T => T.X < 0 || T.Y < 0);

                RectangleF[] rectangleF = new RectangleF[count];

                if (w == null || w.Count() == 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (tControls[i].fType == FSControl.FigureType.Circle) gFrame.AddEllipse(tControls[i].Rectangle);
                        else if (tControls[i].fType == FSControl.FigureType.Square) gFrame.AddRectangle(tControls[i].Rectangle);

                        rectangleF[i] = tControls[i].Rectangle;
                    }
                }
                else
                {
                    var minX = w.Min(T => T.X);
                    var minY = w.Min(T => T.Y);

                    if (minX < 0) minX = -minX;
                    else minX = 0;

                    if (minY < 0) minY = -minY;
                    else minY = 0;

                    for (int i = 0; i < count; i++)
                    {
                        var control = controls[i];
                        var location = new Point(control.Location.X + minX, control.Location.Y + minY);
                        var size = control.Size;

                        control.Rectangle = new RectangleF(location, size);
                        control.Location = location;

                        if (control.fType == FSControl.FigureType.Circle) gFrame.AddEllipse(control.Rectangle);
                        else if (control.fType == FSControl.FigureType.Square) gFrame.AddRectangle(control.Rectangle);
                    }
                }

                Size = new Size((int)rectangleF.Max(T => T.X + T.Width),
                                (int)rectangleF.Max(T => T.Y + T.Height));

                Location = new Point(bounds.Width - Size.Width, 0);

                foreach (var c in controls)
                    c.Show();
            }
            else
            {
                Size = new Size();
                Location = new Point();
            }

            myRegion = new Region(gFrame);

            if (count != 0)
                foreach (var control in controls)
                    control.Visible = true;

        }
    }
}
