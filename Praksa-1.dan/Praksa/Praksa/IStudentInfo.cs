﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa
{
    public interface IStudentInfo
    {
        void AddStudent(Student student);
        void RemoveStudent(int id);

        void ShowAllStudents();
    }
}
