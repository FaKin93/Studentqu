using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        private List<string> que;
        private List<string> fir;
        private List<string> sec;
        private List<string> thi;
        private List<string> fou;
        private List<int?> allcorrect;
        private int? correct;
        private List<int> randomls = new List<int>();
        private int studentId;
        private int questionId;
        private int answerfir;
        private int correct_answers = 0;
        private int grade = 0;

        public TestPage(int idStudent)
        {
            InitializeComponent();
            studentId = idStudent;
            using (var db = new Entities())
            {
                que = db.questions.Select(x => x.question_text).ToList();
                fir = db.questions.Select(x => x.answer_option_1).ToList();
                sec = db.questions.Select(x => x.answer_option_2).ToList();
                thi = db.questions.Select(x => x.answer_option_3).ToList();
                fou = db.questions.Select(x => x.answer_option_4).ToList();
                allcorrect = db.questions.Select(x => x.correct_answer).ToList();
            }
            Random index = new Random();
            int num = index.Next(que.Count);
            randomls.Add(num);
            CreateQuestion(num);
            questionId = num;

        }
        private void CreateQuestion(int index)
        {
            first.IsChecked = false;
            second.IsChecked = false;
            third.IsChecked = false;
            fourth.IsChecked = false;
            question.Text = que.ElementAt(index);
            txfirst.Text = fir.ElementAt(index);
            txsecond.Text = sec.ElementAt(index);
            txthird.Text = thi.ElementAt(index);
            txfourth.Text = fou.ElementAt(index);
            correct = allcorrect.ElementAt(index);
        }
        private void FindCorrect(int answer)
        {
            if (answer == correct) 
            {
                correct_answers += 1;
                grade += 1;
            }
        }
        private void SetNewData(Random index)
        {

            if (randomls.Count < 5)
            {
                int num = index.Next(que.Count);
                if (randomls.Contains(num))
                {
                    num = index.Next(que.Count);
                    randomls.Add(num);
                    CreateQuestion(num);
                }
                else
                {
                    randomls.Add(num);
                    CreateQuestion(num);
                }
            }
            else 
            {
                randomls = new List<int>();
                NavigationService.Navigate(new FinalPage(studentId, questionId, answerfir, 30, 5, correct_answers, grade));
            }

        }

        private void first_Checked(object sender, RoutedEventArgs e)
        {
            if (randomls.Count == 1)
            {
                answerfir = 1;
            }
            FindCorrect(1);
            Random index = new Random();
            SetNewData(index);
            
        }

        private void second_Checked(object sender, RoutedEventArgs e)
        {
            if (randomls.Count == 1)
            {
                answerfir = 2;
            }
            FindCorrect(2);
            Random index = new Random();
            SetNewData(index);
        }

        private void third_Checked(object sender, RoutedEventArgs e)
        {
            if (randomls.Count == 1)
            {
                answerfir = 3;
            }
            FindCorrect(3);
            Random index = new Random();
            SetNewData(index);
        }

        private void fourth_Checked(object sender, RoutedEventArgs e)
        {
            if (randomls.Count == 1)
            {
                answerfir = 4;
            }
            FindCorrect(4);
            Random index = new Random();
            SetNewData(index);
        }
    }
}
