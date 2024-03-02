using Pokemon.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

namespace Pokemon.Pages
{
    /// <summary>
    /// Interaction logic for Pokedex.xaml
    /// </summary>
    public partial class Pokedex : Page
    {
        private readonly string pokemonFilePath = "caught_pokemons.json";
        private List<pokemon> caughtPokemons = new List<pokemon>();
        public Pokedex()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearCaughtPokemons();LoadCaughtPokemons();
            MessageBox.Show("Caught Pokémon data cleared successfully.");
            
        }

        private void ClearCaughtPokemons()
        {
            if (File.Exists(pokemonFilePath))
            {
                File.Delete(pokemonFilePath);
                caughtPokemons.Clear();
            }
        }
        public async void LoadCaughtPokemons()
        {
            if (File.Exists(pokemonFilePath))
            {
                string json = File.ReadAllText(pokemonFilePath);
                caughtPokemons = JsonSerializer.Deserialize<List<pokemon>>(json);
            }
            IList<pokemon> firstGenPokemons = await ApiPokemons.GetPokemons(new HttpClient());
            firstGenPokemons = firstGenPokemons.OrderBy(p => p.id).ToList();

            // Populate the ListView with first-generation Pokémon
            lsvPokemons.Items.Clear();
            foreach (var pokemon in firstGenPokemons)
            {
                var listItem = new PokemonListItem
                {
                    ColorItem = Brushes.Gray,
                    Naam = pokemon.id + ") "+ pokemon.name,
                    Sprite = pokemon.sprites.front_default
                };

                // Check if this Pokémon is caught
                if (caughtPokemons.Any(p => p.name == pokemon.name))
                {
                    listItem.ColorItem = GetColorForType(pokemon.types[0].type.name);
                    var groupedPokemons = caughtPokemons.GroupBy(p => p.name);
                    foreach (var group in groupedPokemons)
                    {
                        listItem.Naam += ($" - Caught {group.Count()} times");

                    }
                }

                lsvPokemons.Items.Add(listItem);
            }
        }

        private SolidColorBrush GetColorForType(string typeName)
        {
            switch (typeName)
            {
                case "normal":
                    return ConvertColorToBrush(168, 167, 122);
                case "fire":
                    return ConvertColorToBrush(238, 129, 48);
                case "water":
                    return ConvertColorToBrush(99, 144, 240);
                case "electric":
                    return ConvertColorToBrush(247, 208, 44);
                case "grass":
                    return ConvertColorToBrush(122, 199, 76);
                case "ice":
                    return ConvertColorToBrush(150, 217, 214);
                case "fighting":
                    return ConvertColorToBrush(194, 46, 40);
                case "poison":
                    return ConvertColorToBrush(163, 62, 161);
                case "ground":
                    return ConvertColorToBrush(226, 191, 101);
                case "flying":
                    return ConvertColorToBrush(169, 143, 243);
                case "psychic":
                    return ConvertColorToBrush(249, 85, 135);
                case "bug":
                    return ConvertColorToBrush(166, 185, 26);
                case "rock":
                    return ConvertColorToBrush(182, 161, 54);
                case "ghost":
                    return ConvertColorToBrush(115, 87, 151);
                case "dragon":
                    return ConvertColorToBrush(111, 53, 252);
                case "dark":
                    return ConvertColorToBrush(112, 87, 70);
                case "steel":
                    return ConvertColorToBrush(183, 183, 206);
                case "fairy":
                    return ConvertColorToBrush(214, 133, 173);
                default:
                    return Brushes.Gray; // Default color
            }
        }

        private SolidColorBrush ConvertColorToBrush(byte r, byte g, byte b)
        {
            return new SolidColorBrush(Color.FromArgb(255, r, g, b));
        }

        public class PokemonListItem
        {
            public SolidColorBrush ColorItem { get; set; }
            public string Sprite { get; set; }
            public string Naam { get; set; }
        }
    }
}
