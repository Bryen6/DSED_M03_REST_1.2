using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DSED_M03_REST02

{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var municipaliteClient = new MunicipaliteClient(new System.Net.Http.HttpClient());

            var municipalites = await municipaliteClient.GetAllAsync();
            Console.WriteLine("Liste des municipalités :");
            foreach (var municipalite in municipalites)
            {
                Console.WriteLine($"{municipalite.NomMunicipalite}");
            }

            await Console.Out.WriteLineAsync("");
            Console.Out.WriteLineAsync("------------------------------------------------------------------");
            Console.Out.WriteLineAsync("");

            var municipaliteAModifier = municipalites.FirstOrDefault(m => m.NomMunicipalite.Equals("Québec"));
            if (municipaliteAModifier is not null)
            {
                municipaliteAModifier.NomMunicipalite = "Quebecq";
                await municipaliteClient.PutAsync(municipaliteAModifier.MunicipaliteID, municipaliteAModifier);
                Console.WriteLine("Nom de la municipalité de Québec modifié avec succès.");

                var quebecModifie = await municipaliteClient.GetAsync(municipaliteAModifier.MunicipaliteID);
                await Console.Out.WriteLineAsync(quebecModifie.NomMunicipalite);
            }
            else
            {
                Console.WriteLine("Municipalité de Québec non trouvée.");
            }

            
        }
    }
}