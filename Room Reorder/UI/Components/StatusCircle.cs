using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Room_Reorder.UI
{
    public class StatusCircle : Control
    {
        private bool isGreen = true;

        [Category("Appearance")]
        [Description("True = Green, False = Red")]
        public bool IsGreen
        {
            get => isGreen;
            set
            {
                isGreen = value;
                Invalidate(); // redraw
            }
        }

        public StatusCircle()
        {
            this.Size = new Size(40, 40);
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// paint the circle
        /// </summary>
        /// <param name="e">The paint event arguments.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Color color = IsGreen ? Color.LimeGreen : Color.Red;
            using (Brush b = new SolidBrush(color))
            {
                e.Graphics.FillEllipse(b, 0, 0, Width - 1, Height - 1);
            }
        }
    }
}
