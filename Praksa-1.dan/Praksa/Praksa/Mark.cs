using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa
{
    public class Mark
    {
        private Student student;
        private Subject subject;
        private int mark;

        
        public Mark(Student student, Subject subject, int mark)
        {
            this.student = student;
            this.subject = subject;
            this.mark = mark;
        }

       public Student GetStudent
        {
            get { return student; }
            set { student = value; }
        }

        public Subject GetSubject
        {
            get { return subject; }
            set { subject = value; }
        }

        public int GetMark
        {
            get { return mark; }
            set { mark = value; }
        }

        public string GetMarkDescription ()
        {
            return $"Student: {student.FirstName} {student.LastName}, has final mark: {mark} in {subject.SubjectName}";
        }

    }
}
