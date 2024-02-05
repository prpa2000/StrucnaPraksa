

using Praksa;

class Program
{
    public static void Main()
    {
        Faculty faculty = new Faculty();
        bool isMenuActive = true;
        while (isMenuActive)
        {
           //baza je pri svakom pokretanju programa prazna, stoga je potrebno prvo unijeti studente i predmete kako bi se mogli dalje koristiti navedene operacije

            Console.WriteLine("1 Add student");
            Console.WriteLine("2 Show all students");
            Console.WriteLine("3 Add subject");
            Console.WriteLine("4 Show all subjects");
            Console.WriteLine("5 Add mark");
            Console.WriteLine("6 Show all marks");
            Console.WriteLine("7 Remove student");
            Console.WriteLine("8 Update student");
            Console.WriteLine("9 Exit");

            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    AddNewStudent(faculty);
                    break;
                case "2":
                    faculty.ShowAllStudents();
                    break;
                case "3":
                    AddNewSubject(faculty);
                    break;
                case "4":
                    faculty.ShowAllSubjects();
                    break;
                case "5":
                    AddNewMark(faculty);
                    break;
                case "6":
                    faculty.ShowAllMarks();
                    break;
                case "7":
                    RemoveEnteredStudent(faculty);
                    break;
                case "8":
                    EditEnteredStudent(faculty);
                    break;
                case "9":
                    isMenuActive = false;
                    break;
            }
        }
    }

    static void AddNewStudent(Faculty faculty)
    {
        Console.WriteLine("First name:");
        string firstname = Console.ReadLine();
        Console.WriteLine("Last name:");
        string lastname = Console.ReadLine();
        Console.WriteLine("JMBG");
        string jmbg = Console.ReadLine();
        Console.WriteLine("Address:");
        string address = Console.ReadLine();
        
        int age;
        do
        {
            Console.Write("Enter age: ");
        } while (!int.TryParse(Console.ReadLine(), out age) && age < 0);

        Student student = new Student(firstname, lastname, jmbg, address, age);
        faculty.AddStudent(student);
        Console.WriteLine("Student added!");
        Console.WriteLine();
    }

    static void AddNewSubject(Faculty faculty)
    {
        Console.WriteLine("Subject name:");
        string subjectname = Console.ReadLine();
        Console.WriteLine("Subject ID: ");
        string subjectid = Console.ReadLine();

        Professor professor = new Professor();
        Console.WriteLine("Professor info");
        Console.WriteLine("First name:");
        professor.FirstName = Console.ReadLine();
        Console.WriteLine("Last name:");
        professor.LastName = Console.ReadLine();
        Console.WriteLine("JMBG:");
        professor.Jmbg = Console.ReadLine();
        Console.WriteLine("Address:");
        professor.Address = Console.ReadLine();
        Console.WriteLine("Professor title:");
        professor.ProfessorTitle = Console.ReadLine();

        Subject subject = new Subject(subjectname, subjectid, professor);
        faculty.AddSubject(subject);
        Console.WriteLine("Subject added!");
        Console.WriteLine();
    }
    static void AddNewMark(Faculty faculty)
    {
        Console.WriteLine("Choose student:");
        faculty.ShowAllStudents();

        int studentIndex;
        Console.Write("Enter student index: "); 
        while (!int.TryParse(Console.ReadLine(), out studentIndex) || studentIndex < 0)
        {
            Console.Write("Enter a valid index: ");
        }

        Student selectedstudent = faculty.GetStudentByIndex(studentIndex);


        Console.WriteLine("Choose subject:");
        faculty.ShowAllSubjects();
        int subjectIndex;
        Console.Write("Enter subject index: ");
        while (!int.TryParse(Console.ReadLine(), out subjectIndex) || subjectIndex < 0)
        {
            Console.Write("Enter a valid index: ");
        }

        Subject selectedsubject = faculty.GetSubjectByIndex(subjectIndex);

        int mark;
        do
        {
            Console.Write("Enter mark: ");
        } while (!int.TryParse(Console.ReadLine(), out mark));

        faculty.AddMark(selectedstudent, selectedsubject, mark);
        Console.WriteLine("Mark added!");
        Console.WriteLine();
        
    }

    static void RemoveEnteredStudent(Faculty faculty)
    {
        Console.WriteLine("Students:");
        faculty.ShowAllStudents();

        int removeindex;
        Console.Write("Enter student index: ");
        while (!int.TryParse(Console.ReadLine(), out removeindex) || removeindex < 0)
        {
            Console.Write("Enter a valid index: ");
        }

        faculty.RemoveStudent(removeindex);
        
    }

    static void EditEnteredStudent(Faculty faculty)
    {
        Console.WriteLine("Choose student you want to update");
        faculty.ShowAllStudents();
        int editIndex;
        Console.Write("Enter student index: ");
        while (!int.TryParse(Console.ReadLine(), out editIndex) || editIndex < 0)
        {
            Console.Write("Enter a valid index: ");
        }

        Console.WriteLine("Updated first name:");
        string firstname = Console.ReadLine();
        Console.WriteLine("Update last name:");
        string lastname = Console.ReadLine();
        Console.WriteLine("Update JMBG");
        string jmbg = Console.ReadLine();
        Console.WriteLine("Update Address:");
        string address = Console.ReadLine();

        int age;
        do
        {
            Console.Write("Update age: ");
        } while (!int.TryParse(Console.ReadLine(), out age) && age < 0);

        faculty.EditStudentByIndex(editIndex, firstname, lastname, jmbg, address, age);

    }




}