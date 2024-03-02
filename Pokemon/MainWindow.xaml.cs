using System.Collections;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pokemon.Classes;
using Pokemon.Pages;
using static System.Net.WebRequestMethods;

namespace Pokemon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Search Search = new Search();
        Pokedex Pokedex = new Pokedex();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search.refresh();
            frmScherm.Content = Search;
        }

        private void btnPokedex_Click(object sender, RoutedEventArgs e)
        {
            frmScherm.Content = Pokedex;
            Pokedex.LoadCaughtPokemons();
        }
    }
}