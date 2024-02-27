using System;
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
        public static async Task<generation1> GetPokemons(HttpClient client)
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

        public static async Task<generation1> getpokemon(HttpClient client, string api)
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
    }
}
