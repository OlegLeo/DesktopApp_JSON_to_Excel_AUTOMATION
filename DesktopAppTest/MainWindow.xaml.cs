using System;

using System.Windows;


namespace DesktopAppTest
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


        String usernameData, passwordData;


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            usernameData = username.Text;
            passwordData = password.Password.ToString();


            // This is just for an exemple, obvioslly this project doesnt have a authentication planned yet.
            if (usernameData == "admin" && passwordData == "test")
            {
                MessageBox.Show("Autenticação Bem-Sucedida!");

                //Main.Content = new List();
                Window1 subWindow = new Window1();
                subWindow.Show();

                Close();
                //Hide();
            }
            else if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Password)) // in case the fields are not filled
                {
                MessageBox.Show("Por favor, preencha os dados de autenticação!");
            }
            else
            {
                MessageBox.Show("Dados incorretos ou conta não existente."); // in case the authentication is wrong
                username.Text= String.Empty;
                password.Password = String.Empty;
            }

        }




    }
}
