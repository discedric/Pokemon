using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokemon.Classes
{
    internal class ApiPokemons
    {
        public static async Task<IList<pokemon>> GetPokemons(HttpClient client)
        {
            string api = "https://pokeapi.co/api/v2/generation/generation-i";
            try
            {
                var json = await client.GetStringAsync(api);
                var pokemons = JsonSerializer.Deserialize<GenerationI>(json);
                var tasks = pokemons.pokemon_species.Select(async pokemon =>
                {
                    return await getpokemon(client, pokemon.name);
                });
                var pokemonList = await Task.WhenAll(tasks);
                return pokemonList.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<pokemon> getpokemon(HttpClient client, string Naam)
        {
            try
            {
                Naam = Naam.ToLower().Trim();
                var json = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{Naam}");
                if(json =="Not Found")
                {
                    return null;
                }
                var pokemon = JsonSerializer.Deserialize<pokemon>(json);
                return pokemon;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
