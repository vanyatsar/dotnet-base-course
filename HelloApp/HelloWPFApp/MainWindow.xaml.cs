using HelloLibrary;
using System.Windows;

namespace HelloWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void showInputButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Greetings.GetCurrentTimeGreetings(inputTextBox.Text));
        }
    }
}
