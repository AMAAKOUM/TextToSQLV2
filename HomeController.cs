using Microsoft.AspNetCore.Mvc;
using TextToSql2.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace TextToSql2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View(new DataModel());
        }

        [HttpPost]
        public IActionResult TranslateAndQuery(string usertext)
        {
            var model = new DataModel { UserQuery = usertext };
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            // Ajout de la focntion conditionnelle pour eviter le CS8600
            if(string.IsNullOrEmpty(connectionString) )
            {
                throw new InvalidOperationException("la chaine de connextion 'DefaultConnection' est manquante dans le appsettings.json");
            }

            string sqlQuery = "";
            usertext = usertext.ToLower();

            if (usertext.Contains("clients"))
            {
                sqlQuery = "SELECT * FROM Clients";
            }
            else if (usertext.Contains("produits") && usertext.Contains("prix superieur à"))
            {
                var match = Regex.Match(usertext, @"\d+");
                if (match.Success)
                {
                    sqlQuery = $"SELECT * FROM Produits WHERE Prix > {match.Success}";
                }else
                {
                    sqlQuery = "SELECT * FROM Produits";
                }
            }
            else
            {
                sqlQuery = "SELECT -- cette requête est incomprehensible -- AS Error";
            }
            model.SqlQuery = sqlQuery;
            if(string.IsNullOrEmpty(sqlQuery) || sqlQuery.Contains("Erreur"))
            {
                return View("Index", model);
            }


            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sqlQuery, connection))
                    {
                        var dataAdapter = new SqlDataAdapter(command);
                        dataAdapter.Fill(model.Results); 
                    }                    

                }catch( Exception ex )
                {
                    model.Results.Columns.Add("Erreur");
                    model.Results.Rows.Add($"Une erreur est survenue : {ex.Message}");
                }
            }
            return View("Index", model);
        }


        

    }
}
