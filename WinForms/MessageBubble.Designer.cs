using System.Drawing;

namespace Forms1
{
    public partial class MessageBubbleControl : UserControl
    {
        public MessageBubbleControl()
        {
            InitializeComponent();
            this.ResizeRedraw = true; 
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "MessageBubbleControl";
            this.Size = new System.Drawing.Size(150, 100);
            this.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1); 
            int borderRadius = 20;

            using (Pen pen = new Pen(Color.DarkOrange, 2)) 
            {
                g.DrawRoundedRectangle(pen, rect, borderRadius);
            }
        }
    }
}
