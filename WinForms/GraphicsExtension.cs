using System.Drawing;
using System.Drawing.Drawing2D;

public static class GraphicsExtensions
{
    public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle rect, int radius)
    {
        int diameter = radius * 2;
        Size size = new Size(diameter, diameter);
        Rectangle arc = new Rectangle(rect.Location, size);
        GraphicsPath path = new GraphicsPath();

        if (radius == 0)
        {
            g.DrawRectangle(pen, rect);
            return;
        }

        // Top left arc
        path.AddArc(arc, 180, 90);

        // Top right arc
        arc.X = rect.Right - diameter;
        path.AddArc(arc, 270, 90);

        // Bottom right arc
        arc.Y = rect.Bottom - diameter;
        path.AddArc(arc, 0, 90);

        // Bottom left arc
        arc.X = rect.Left;
        path.AddArc(arc, 90, 90);

        path.CloseFigure();
        g.DrawPath(pen, path);
        path.Dispose();
    }
}