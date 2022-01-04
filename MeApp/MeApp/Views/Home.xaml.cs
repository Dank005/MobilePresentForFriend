using MeApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        SqlConnection sqlConnection;
        public Home()
        {
            InitializeComponent();
            ShowPresentListAsync();
        }

        private async Task ShowPresentListAsync()
        {
            string connectionString = $"Data Source=sql.bsite.net\\MSSQL2016; Initial Catalog=danka0060_SampleDB; User ID=danka0060_SampleDB; Password=danka0060";
            sqlConnection = new SqlConnection(connectionString);

            try
            {
                List<Present> presents = new List<Present>();
                sqlConnection.Open();
                string queryString = "Select * from dbo.Presents";
                SqlCommand cmd = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    presents.Add(new Present
                    {
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Link = reader["Link"].ToString(),
                        Price = (float)reader["Price"],
                        DataFiles = (byte[])reader["DataFiles"]
                    });
                }
                reader.Close();
                sqlConnection.Close();
                presentList.ItemsSource = presents;
            }

            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }
    }
}