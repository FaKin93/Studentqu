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

namespace Studentqu.Pages
{
    /// <summary>
    /// Логика взаимодействия для QuestionPage.xaml
    /// </summary>
    public partial class QuestionPage : Page
    {
        public QuestionPage()
        {
            InitializeComponent();
            var currentStudents = Entities.GetContext().questions.ToList();
            DataGridQuestion.ItemsSource = currentStudents;
            UpdateUsers();
        }
        private void UpdateUsers()
        {
            //загружаем всех пользователей в список
            var currentStudents = Entities.GetContext().questions.ToList();

            //осуществляем поиск по Ф.И.О. без учета регистра букв
            currentStudents = currentStudents.Where(x => x.question_type.ToLower().Contains(typeques.Text.ToLower())).ToList();

            currentStudents = currentStudents.Where(x => x.question_text.ToLower().Contains(textques.Text.ToLower())).ToList();

            DataGridQuestion.ItemsSource = currentStudents;

        }
        private void DataGridUser_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridQuestion.ItemsSource = Entities.GetContext().questions.ToList();
            }

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QuesAddPage(null, false));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DataGridQuestion.SelectedItems.Cast<questions>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {usersForRemoving.Count()} элементов?", "Внимание",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Entities.GetContext().questions.RemoveRange(usersForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DataGridQuestion.ItemsSource = Entities.GetContext().questions.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }


        }


        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.QuesAddPage((sender as Button).DataContext as questions, true));
        }

        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void type_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }
    }
}
