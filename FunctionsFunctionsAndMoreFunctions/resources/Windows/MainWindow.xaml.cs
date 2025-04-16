using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.ViewModels;
using System.Windows;

namespace FunctionsFunctionsAndMoreFunctions
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
