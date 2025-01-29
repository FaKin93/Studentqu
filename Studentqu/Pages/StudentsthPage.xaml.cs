using System;
using System.Collections.Generic;
using System.Data;
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

namespace Studentqu.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentsthPage.xaml
    /// </summary>
    public partial class StudentsthPage : Page
    {

        public StudentsthPage()
        {
            InitializeComponent();
            var currentStudents = Entities.GetContext().students.ToList();
            DataGridStudent.ItemsSource = currentStudents;
            UpdateUsers();
        }
        private void UpdateUsers()
        {
            //загружаем всех пользователей в список
            var currentStudents = Entities.GetContext().students.ToList();

            //осуществляем поиск по Ф.И.О. без учета регистра букв
            currentStudents = currentStudents.Where(x => x.full_name.ToLower().Contains(FIO.Text.ToLower())).ToList();

            currentStudents = currentStudents.Where(x => x.group_number.ToLower().Contains(number.Text.ToLower())).ToList();

            DataGridStudent.ItemsSource = currentStudents;

        }
        private void DataGridUser_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridStudent.ItemsSource = Entities.GetContext().students.ToList();
            }

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudAddPage(null, false));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DataGridStudent.SelectedItems.Cast<students>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {usersForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entities.GetContext().students.RemoveRange(usersForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridStudent.ItemsSource = Entities.GetContext().students.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }


        }


        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.StudAddPage((sender as Button).DataContext as students, true));
        }

        private void number_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void FIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }
    }
}
