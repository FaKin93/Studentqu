//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Studentqu
{
    using System;
    using System.Collections.Generic;
    
    public partial class test_reports
    {
        public int test_id { get; set; }
        public Nullable<System.DateTime> test_date { get; set; }
        public Nullable<int> student_id { get; set; }
        public Nullable<int> question_id { get; set; }
        public Nullable<int> answer { get; set; }
        public Nullable<int> test_duration_minutes { get; set; }
        public Nullable<int> total_questions { get; set; }
        public Nullable<int> correct_answers { get; set; }
        public Nullable<int> student_grade { get; set; }
    
        public virtual questions questions { get; set; }
        public virtual students students { get; set; }
    }
}
