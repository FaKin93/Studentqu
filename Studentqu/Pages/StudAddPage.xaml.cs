﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для StudAddPage.xaml
    /// </summary>
    public partial class StudAddPage : Page
    {
        private students _currentStudent = new students();
        private bool redact;
        public StudAddPage(students selectedStudent, bool flag)
        {
            InitializeComponent();
            if (selectedStudent != null)
            {
                _currentStudent = selectedStudent;
            }
            DataContext = _currentStudent;
            var context = Entities.GetContext();
            this.redact = flag;
        }


        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (redact == false)
            {
                if (string.IsNullOrEmpty(TextBoxFIO.Text) || string.IsNullOrEmpty(TextBoxGroup.Text))
                {
                    MessageBox.Show("Заполните все вышеуказанные поля!");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены что хотите Добавить эти данные?", "Подтвержение закрытия", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Entities db = new Entities();
                    students studentObject = new students
                    {
                        full_name = TextBoxFIO.Text,
                        group_number = TextBoxGroup.Text


                    };
                    db.students.Add(studentObject);
                    db.SaveChanges();
                    MessageBox.Show("Студент Добавлен");
                    NavigationService.GoBack();

                }
            }
            else
            {
                StringBuilder errors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(_currentStudent.group_number))
                    errors.AppendLine("Укажите группу!");
                if (string.IsNullOrWhiteSpace(_currentStudent.full_name))
                    errors.AppendLine("Укажите полное имя!");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                //Добавляем в объект students новую запись
                if (_currentStudent.student_id == 0)
                {
                    Entities.GetContext().students.Add(_currentStudent);
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
