using System.Windows;

namespace Fit_Track_App.Pages
{
    public partial class EmailCodeWindow : Window
    {
        public EmailCodeWindow(string code)
        {
            InitializeComponent();
            CodeText.Text = code;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
