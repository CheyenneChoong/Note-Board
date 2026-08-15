using System.Windows.Controls;
using Note_Board.Helpers;

namespace Note_Board.Views;
public partial class Note : UserControl
{
    public Note()
    {
        InitializeComponent();
        Loaded += (s, e) => Drag.EnableDrag(this, (Parent as Canvas)!);
    }
}