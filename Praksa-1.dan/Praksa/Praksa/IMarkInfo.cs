using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praksa
{
    public interface IMarkInfo
    {
        void AddMark(Student student, Subject subject, int mark);
        void ShowAllMarks();
    }
}
