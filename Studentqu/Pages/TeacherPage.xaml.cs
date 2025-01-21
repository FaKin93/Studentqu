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
    /// Логика взаимодействия для TeacherPage.xaml
    /// </summary>
    public partial class TeacherPage : Page
    {
        public TeacherPage()
        {
            InitializeComponent();
        }

        private void question_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QuestionPage());
        }

        private void test_request_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestPage());
        }

        private void students_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentsthPage());
        }
    }
}
