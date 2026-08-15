using System.Windows.Controls;
using Note_Board.Helpers;

namespace Note_Board.Views;
public partial class Checklist : UserControl
{
    public Checklist()
    {
        InitializeComponent();
        Loaded += (s, e) => Drag.EnableDrag(this, (Parent as Canvas)!);
    }
}