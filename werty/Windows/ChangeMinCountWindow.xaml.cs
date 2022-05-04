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
using System.Windows.Shapes;
using werty.Model;

namespace werty.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeMinCountWindow.xaml
    /// </summary>
    public partial class ChangeMinCountWindow : Window
    {
        private List<Material> _MaterialLists;

        public ChangeMinCountWindow(List<Material> materials)
        {
            InitializeComponent();
            _MaterialLists = materials;
            NewCountTextBox.Text = _MaterialLists.Max(m => m.MinCount).ToString();
        }

        private void ChangeCountButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewCountTextBox.Text.Length == 0)
            {
                MessageBox.Show("Необходимо ввести значение",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var newCount = NewCountTextBox.Text;
            int intNewCount = 0;
            try
            {
                intNewCount = Convert.ToInt32(newCount);
            }
            catch (Exception)
            {

                MessageBox.Show("Значение должно быть целым",
                     "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (intNewCount<0)
            {
                MessageBox.Show("Значение должно быть не меньше нуля",
                     "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            foreach(var material in _MaterialLists)
            {
                material.MinCount = Convert.ToInt32(NewCountTextBox.Text);
                DataBaseHelper.SaveChanges();
            }
            MessageBox.Show("Минимальное количество учпешно изменено");

            this.Close();
        }
    }
}
