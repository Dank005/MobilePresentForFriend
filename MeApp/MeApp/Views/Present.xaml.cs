using MeApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Present : ContentPage
    {
        SqlConnection sqlConnection;

        public Present()
        {
            InitializeComponent();
            string connectionString = $"Data Source=sql.bsite.net\\MSSQL2016; Initial Catalog=danka0060_SampleDB; User ID=danka0060_SampleDB; Password=danka0060";
            sqlConnection = new SqlConnection(connectionString);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                await App.Current.MainPage.DisplayAlert("Alert", "Success", "Ok");
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async void getButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Office> myOffice = new List<Office>();
                sqlConnection.Open();
                string queryString = "Select * from dbo.People";
                SqlCommand cmd = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    myOffice.Add(new Office
                    {
                        FirstName = reader["First Name"].ToString(),
                        LastName = reader["Last Name"].ToString(),
                        Department = reader["Department"].ToString()
                    });
                }
                reader.Close();
                sqlConnection.Close();
                MyListView.ItemsSource = myOffice;
            }

            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }

        private void postButton_Clicked(object sender, EventArgs e)
        {

        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {

        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}