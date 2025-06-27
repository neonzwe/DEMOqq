using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MosaicApp
{
    public partial class Window1 : Window
    {
        private zitraksEntities db = new zitraksEntities();
        private List<Материалы> материалыList; // Сохраняем полный список для сброса поиска

        public Window1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            материалыList = db.Материалы.ToList();
            dataGrid.ItemsSource = материалыList;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            db.Материалы.Add(new Материалы());
            db.SaveChanges();
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Материалы item)
            {
                db.Материалы.Remove(item);
                db.SaveChanges();
                LoadData();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            LoadData();
            MessageBox.Show("Изменения сохранены!");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // --- Функция поиска ---
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchBox.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                dataGrid.ItemsSource = материалыList;
                return;
            }

            // Пример поиска по полю "Название" (замени на нужное поле!)
            var results = материалыList
                .Where(m => (m.Название != null && m.Название.ToLower().Contains(searchText)))
                .ToList();

            dataGrid.ItemsSource = results;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "";
            dataGrid.ItemsSource = материалыList;
        }
    }
}