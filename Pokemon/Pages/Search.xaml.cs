using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

namespace Pokemon.Pages
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        Random rnd = new Random();
        private readonly string pokemonFilePath = "caught_pokemons.json";
        private List<pokemon> caughtPokemons = new List<pokemon>();
        private pokemon pokemon;
        int catchAttempts = 0;
        public Search()
        {
            InitializeComponent();
        }

        public void refresh()
        {
            caughtPokemons.Clear();
            catchAttempts = 0;
            txbInfo.Text = txbSearch.Text = "";
            imgSprite.Source = null;
            LoadCaughtPokemons();
        }

        private void LoadCaughtPokemons()
        {
            if (File.Exists(pokemonFilePath))
            {
                string json = File.ReadAllText(pokemonFilePath);
                caughtPokemons = JsonSerializer.Deserialize<List<pokemon>>(json);
            }
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string naam = txbSearch.Text;
            pokemon = await ApiPokemons.getpokemon(new HttpClient(), naam);
            print();
        }

        private async void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            pokemon = await ApiPokemons.getpokemon(new HttpClient(), rnd.Next(152).ToString());
            print();
        }
        private void print()
        {
            string data;
            if (pokemon != null)
            {
                data = "Name: " + pokemon.name + "\n";
                data += "height: " + pokemon.height.ToString() + "\n";
                data += "weight: " + pokemon.weight.ToString() + "\n";
                data += "base exprience: " + pokemon.base_experience.ToString() + "\n";
                imgSprite.Source = new BitmapImage(new Uri(pokemon.sprites.front_default));
                foreach (var Type in pokemon.types)
                    data += "Type: " + Type.type.name + "\n";
                foreach (var stat in pokemon.stats)
                    data += stat.stat.name + ": " + stat.base_stat + " " + "\n";
            }
            else
                data = "Not Found";
            txbInfo.Text = data;
        }
        private void btnCatch_Click(object sender, RoutedEventArgs e)
        {
            double catchProbability = 0.3;

            Random random = new Random();
            double randomNumber = random.NextDouble();

            while (catchAttempts < 3)
            {
                if(randomNumber < catchProbability)
                {
                    caughtPokemons.Add(pokemon);
                    SaveCaughtPokemons();
                    MessageBox.Show("You caught " + pokemon.name + "!");
                    refresh();
                    return;
                }
                catchAttempts++;
            }
            MessageBox.Show("You failed to catch " + pokemon.name + ".");
            catchAttempts = 0;
        }

        private void SaveCaughtPokemons()
        {
            string json = JsonSerializer.Serialize(caughtPokemons);
            File.WriteAllText(pokemonFilePath, json);
        }
    }
}
