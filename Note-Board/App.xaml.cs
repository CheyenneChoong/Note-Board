using System.Windows;
using Note_Board.Models;

namespace Note_Board;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        new Setup();
        base.OnStartup(e);
    }
}

