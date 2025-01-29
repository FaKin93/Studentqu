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
    /// Логика взаимодействия для QuesAddPage.xaml
    /// </summary>
    public partial class QuesAddPage : Page
    {
        private questions _currentQuestion = new questions();
        private bool redact;
        public QuesAddPage(questions selectedQuestion, bool flag)
        {
            InitializeComponent();
            if (selectedQuestion != null)
            {
                _currentQuestion = selectedQuestion;
            }
            DataContext = _currentQuestion;
            var context = Entities.GetContext();
            this.redact = flag;
        }


        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (redact == false)
            {
                if (string.IsNullOrEmpty(TextBoxtype.Text) || string.IsNullOrEmpty(TextBoxtext.Text) || string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text) || string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox4.Text) || string.IsNullOrEmpty(TextBoxCorrect.Text))
                {
                    MessageBox.Show("Заполните все вышеуказанные поля!");
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите Добавить эти данные?", "Подтвержение закрытия", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Entities db = new Entities();
                    questions questionObject = new questions
                    {
                        question_type = TextBoxtype.Text,
                        question_text = TextBoxtext.Text,
                        answer_option_1 = TextBox1.Text,
                        answer_option_2 = TextBox2.Text,
                        answer_option_3 = TextBox3.Text,
                        answer_option_4 = TextBox4.Text,
                        correct_answer = int.Parse(TextBoxCorrect.Text)


                    };
                    db.questions.Add(questionObject);
                    db.SaveChanges();
                    MessageBox.Show("Вопрос Добавлен");
                    NavigationService.GoBack();

                }
            }
            else
            {
                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(_currentQuestion.question_type))
                    errors.AppendLine("Укажите тип вопроса!");
                if (string.IsNullOrWhiteSpace(_currentQuestion.question_text))
                    errors.AppendLine("Укажите текст вопроса!");
                if (string.IsNullOrWhiteSpace(_currentQuestion.answer_option_1))
                    errors.AppendLine("Укажите первый ответ!");
                if (string.IsNullOrWhiteSpace(_currentQuestion.answer_option_2))
                    errors.AppendLine("Укажите второй ответ!");
                if (string.IsNullOrWhiteSpace(_currentQuestion.answer_option_3))
                    errors.AppendLine("Укажите третий ответ!");
                if (string.IsNullOrWhiteSpace(_currentQuestion.answer_option_4))
                    errors.AppendLine("Укажите четвёртый ответ!");
                if (string.IsNullOrWhiteSpace(Convert.ToString(_currentQuestion.correct_answer)))
                    errors.AppendLine("Укажите правильный номер ответа!");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                //Добавляем в объект students новую запись
                if (_currentQuestion.question_id == 0)
                {
                    Entities.GetContext().questions.Add(_currentQuestion);
                }
                try
                {
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }



        }
    }
}
