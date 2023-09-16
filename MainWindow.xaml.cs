using System.Collections.Generic;
using System.Data.OleDb;
using System;
using System.Windows;

namespace profiles2
{
    public partial class MainWindow : Window
    {
        string connectionString; 
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel(new DefaultDialogService(), new XmlFileService()); 

            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=countriesDB.accdb"; 
        }

        // действия со странами
        public List<string> ReadCountries(string connectionString) // чтение стран из БД
        {
            string queryString = "SELECT Country FROM countries"; 
            List<string> countries = new List<string>(); 
            using (OleDbConnection connection = new OleDbConnection(connectionString)) 
            {
                try 
                {
                    OleDbCommand command = new OleDbCommand(queryString, connection); 
                    connection.Open(); 
                    OleDbDataReader reader = command.ExecuteReader(); 
                    while (reader.Read()) 
                    {
                        countries.Add(reader.GetString(0)); 
                    }
                    reader.Close(); 
                    connection.Close(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message); 
                }
            }
            return countries; 
        }
        public void SaveCountry(string connectionString, string country) // сохранение страны в БД
        {
            string queryString = "INSERT INTO countries(Country) VALUES ('" + country + "')"; 
            using (OleDbConnection connection = new OleDbConnection(connectionString)) 
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(queryString, connection); 
                    connection.Open(); 
                    command.ExecuteNonQuery(); 
                    connection.Close(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message); 
                }
            }
        }

        private void combo_countries_Loaded(object sender, RoutedEventArgs e) // обработчик события загрузки combobox-а
        {
            country.ItemsSource = ReadCountries(connectionString); 
        }
        private void button_country_save_Click(object sender, RoutedEventArgs e) // обработчик события клика на кнопку "Сохранить страну"
        {
            if (textbox_new_country.Text == "") 
            {
                MessageBox.Show("Введите название страны");
            }
            else 
            {
                SaveCountry(connectionString, textbox_new_country.Text); 
                textbox_new_country.Text = ""; 
                MessageBox.Show("Страна сохранена");
                country.ItemsSource = ReadCountries(connectionString); 
            }
        }

    }
}