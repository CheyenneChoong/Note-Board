using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Note_Board.Helpers;
public static class Drag
{
    public static void EnableDrag(UIElement card, Canvas parent)
    {
        Point? clickPosition = null;
        card.MouseDown += (s, e) =>
        {
            clickPosition = e.GetPosition(card);
            card.CaptureMouse();
        };

        card.MouseMove += (s, e) =>
        {
            if (clickPosition != null)
            {
                var position = e.GetPosition(parent);
                Canvas.SetLeft(card, position.X - clickPosition.Value.X);
                Canvas.SetTop(card, position.Y - clickPosition.Value.Y);
            }
        };

        card.MouseUp += (s, e) =>
        {
            clickPosition = null;
            card.ReleaseMouseCapture();
        };
    }
}