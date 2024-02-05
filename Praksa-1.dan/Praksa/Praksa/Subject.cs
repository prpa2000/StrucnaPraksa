using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa
{
    public class Subject
    {
        private string subjectname;
        private string subjectid;
        private Professor subjectprofessor;

       

        public Subject(string subjectname, string subjectid, Professor subjectprofessor)
        {
            this.subjectname = subjectname;
            this.subjectid = subjectid;
            this.subjectprofessor = subjectprofessor;
        }

        public string SubjectName
        {
            get { return subjectname; }
            set { subjectname = value; }
        }

        public string SubjectId
        {
            get { return subjectid; }
            set { subjectid = value; }
        }

        public Professor SubjectProfessor
        {
            get { return subjectprofessor; }
            set { subjectprofessor = value; }
        }

        public string GetSubjectDescription()
        {
            return $"Subject: {subjectname}, SubjectID: {subjectid}, Professor: {subjectprofessor.FirstName} {subjectprofessor.LastName}";
        }
    }
}
