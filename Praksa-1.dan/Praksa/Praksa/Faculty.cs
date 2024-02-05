using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa
{
    public class Faculty : IStudentInfo, ISubjectInfo, IMarkInfo
    {
        public Faculty() { }
        List<Student> students = new List<Student>(); 
        List<Subject> subjects = new List<Subject>();
        List<Mark> marks = new List<Mark>();    

        public void AddStudent(Student student)
        {
            students.Add(student);
        }
        public void RemoveStudent(int id)
        {
            students.RemoveAt(id);
        }
        public Student GetStudentByIndex(int index)
        {
            if (index >= 0 && index < students.Count)
            {
                return students[index];
            }
            else
            {
                Console.WriteLine("Invalid index.");
                return null;
            }
        }
        public Student EditStudentByIndex(int index, string newfirstname, string newlastname, string newjmbg, string address, int age)
        {
            if (index >= 0 && index < students.Count)
            {
                students[index].FirstName = newfirstname;
                students[index].LastName = newlastname;
                students[index].Jmbg = newjmbg;
                students[index].Address = address;
                students[index].Age = age;
                return students[index];
            }
            else
            {
                Console.WriteLine("Invalid index.");
                return null;
            }
        }


        public void ShowAllStudents ()
        {
            foreach(var student in students)
            {
                Console.WriteLine(student.GetPersonDescription());
            }
        }

        public void AddSubject(Subject subject)
        {
            subjects.Add(subject);
        }

        public Subject GetSubjectByIndex(int index)
        {
            if (index >= 0 && index < students.Count)
            {
                return subjects[index];
            }
            else
            {
                Console.WriteLine("Invalid index. Returning null.");
                return null;
            }
        }


        public void ShowAllSubjects()
        {
            foreach(var subject in subjects)
            {
                Console.WriteLine(subject.GetSubjectDescription());
            }
        }

        public void AddMark(Student student, Subject subject, int mark)
        {
            Mark newmark = new Mark(student, subject, mark);
            marks.Add(newmark);
        }
        public void ShowAllMarks()
        {
            foreach(var mark in marks)
            {
                Console.WriteLine(mark.GetMarkDescription());
            }
        }

    }
}
