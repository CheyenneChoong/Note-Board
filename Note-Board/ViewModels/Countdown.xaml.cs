using System.Windows.Controls;
using Note_Board.Helpers;

namespace Note_Board.Views;
public partial class Countdown : UserControl
{
    public Countdown()
    {
        InitializeComponent();
        Loaded += (s, e) => Drag.EnableDrag(this, (Parent as Canvas)!);
    }
}