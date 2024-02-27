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
using static System.Net.WebRequestMethods;

namespace Pokemon
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

        static async Task<generation1> getpokemons(HttpClient client)
        {
            string api = "https://pokeapi.co/api/v2/generation/generation-i";
            try
            {
                var json = await client.GetStringAsync(api);
                var pokemons = JsonSerializer.Deserialize<generation1>(json);
                return pokemons;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        static async Task<generation1> getpokemon(HttpClient client, string api)
        {

            try
            {
                var json = await client.GetStringAsync(api);
                var pokemons = JsonSerializer.Deserialize<generation1>(json);
                return pokemons;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private async void getPokemon()
        {
            using HttpClient client = new();
            var pokemons = await getpokemons(client);
        }
    }
}