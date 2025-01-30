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
    
    public partial class FinalPage : Page
    {
        private int stuid;
        private int queid;
        private int ansid;
        private int dur;
        private int total;
        private int correct;
        private int grade;
        public FinalPage(int studentid, int questionid, int answerid, int duration, int total, int correct, int grade)
        {
            InitializeComponent();
            Grade_stu.Text = Convert.ToString(grade);
            Count.Text = Convert.ToString(correct);
            stuid = studentid;
            queid = questionid;
            ansid = answerid;
            dur = duration;
            this.total = total;
            this.correct = correct;
            this.grade = grade;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Now;
            Entities db = new Entities();
            test_reports userObject = new test_reports
            {
                test_date = today,
                student_id = stuid,
                question_id = queid,
                answer = ansid,
                test_duration_minutes = dur,
                total_questions = total,
                correct_answers = correct,
                student_grade = grade
            };
            db.test_reports.Add(userObject);
            db.SaveChanges();
            MessageBox.Show("результат сохранён");
            NavigationService.Navigate(new StudentPage());
        }
    }
}
