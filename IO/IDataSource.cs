using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab04_Application.Model;

namespace Lab04_Application.IO
{
    public interface IDataSource
    {
        List<Student> GetData(String filename);
        void SaveChange(List<Student> students, string filename);
    }
}
