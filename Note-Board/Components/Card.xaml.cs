using System.Windows;
using System.Windows.Controls;

namespace Note_Board.Components;
public partial class Card : UserControl
{
    public Card()
    {
        InitializeComponent();
        Loaded += (s, e) => EnableDrag(this, (Parent as Canvas)!);
    }

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