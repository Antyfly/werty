using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using werty.Model;

namespace werty.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MaterialList : Window
    {
        private const int ItemsPerPage = 15;

        private int CurrentPage = 0;
        public MaterialList()
        {
            InitializeComponent();

            var materialTypeList = DataBaseHelper.GetMaterialsType();
            materialTypeList.Insert(0, new MaterialType { Title = "Все"});
            FilterComboBox.ItemsSource = materialTypeList;
            MaterialListView.ItemsSource = DataBaseHelper.GetMaterials();
           
        }

        public void UpdateMaterialList()
        {
            if (SearchTextBox is null || SortComboBox is null || FilterComboBox is null)
                return;
            
            var materials = DataBaseHelper.GetMaterials();

            var allMaterialCount = materials.Count;

            if (SearchTextBox.Text.Length != 0)
            {
                materials = materials.Where(m => m.Title.Contains(SearchTextBox.Text) || m.Description?.Contains(SearchTextBox.Text) == true).ToList();
            }
            switch(((ComboBoxItem)SortComboBox.SelectedItem).Content.ToString())
            {
                case "Наименование по возростанию":
                    materials = materials.OrderBy(m => m.Title).ToList();
                    break;
                case "Наименование по убыванию":
                    materials = materials.OrderByDescending(m => m.Title).ToList();
                    break;
                case "Остаток по возрастанию":
                    materials = materials.OrderBy(m => m.CountInStock).ToList();
                    break;
                case "Остаток по убыванию":
                    materials = materials.OrderByDescending(m => m.CountInStock).ToList();
                    break;
                case "Стоимость по возрастанию":
                    materials = materials.OrderBy(m => m.Cost).ToList();
                    break;
                case "Стоимость по убыванию":
                    materials = materials.OrderByDescending(m => m.Cost).ToList();
                    break;
            }


            if (((MaterialType)FilterComboBox.SelectedItem).Title != "Все")
            {
                materials = materials.Where(m=> m.MaterialType == (MaterialType)FilterComboBox.SelectedItem).ToList();
            }

            var fillteredMaterialsCount = materials.Count;
            MaterialCountTextBlock.Text = $"Выведено {fillteredMaterialsCount} из {allMaterialCount}";

            MaterialListView.ItemsSource = materials;

            GenerateButton();

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMaterialList();
        }

        private void SortTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaterialList();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaterialList();
        }

        private void MaterialListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView materialListView = sender as ListView;

            if (materialListView.SelectedItems.Count == 0)
            {
                ChangeMinCountButton.Visibility = Visibility.Hidden;
            }
            else
            {
                ChangeMinCountButton.Visibility= Visibility.Visible;
            }
        }

        private void ChangeMinCountButton_Click(object sender, RoutedEventArgs e)
        {
            new ChangeMinCountWindow(MaterialListView.SelectedItems.Cast<Material>().ToList()).ShowDialog();
            UpdateMaterialList();
        }

        private void GenerateButton()
        {

            NumberButtonStackPanel.Children.Clear();
            int pageCount = Convert.ToInt32(Math.Floor((double)MaterialListView.Items.Count / ItemsPerPage));
            var materials = DataBaseHelper.GetMaterials();
            for (int i = 0; i < 5; i++)
            {
                if (ItemsPerPage * i > materials.Count)
                    continue;

                Button newButton = new Button()
                    {
                    Content = i+1,
                    Background = Brushes.Transparent,
                    Height = 25
                };
                newButton.Click += NewButton_Click;

                NumberButtonStackPanel.Children.Add(newButton);
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
