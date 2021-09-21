using Lab04_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab04_Application.Model;
using System.IO;

namespace Lab04_Application.IO
{
    public class TextDataSource : IDataSource
    {
        private string filename { get;set; }
        List<Student> IDataSource.GetData(string filename)
        {
            List<Student> students = new List<Student>();
            if (File.Exists(filename))
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var rd = new StreamReader(stream))
                    {
                        String line;
                        while ((line = rd.ReadLine()) != null)
                        {
                            students.Add(StudentParse(line));
                        }
                    }
                }
            }
            return students;
        }
        private Student StudentParse(String line)
        {
            Student sd = new Student();
            String[] strs = line.Split('*');
            sd.id = strs[0];
            sd.name = strs[1];
            sd.email = strs[7];
            sd.address = strs[3];
            sd.path = strs[5];
            sd.birthday = DateTime.Parse(strs[2]);
            sd.sex = strs[6] == "1" ? true : false;
            sd.classroom = strs[4];
            sd.phone = strs[8];
            return sd;
        }

        void IDataSource.SaveChange(List<Student>students, string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                using (var sw = new StreamWriter(stream))
                {
                    foreach (var item in students)
                    {
                        String line = FormatStudent(item);
                        sw.WriteLine(line);
                    }
                }
            }
        }
        private String FormatStudent(Student student)
        {
            String kq = "";
            string sex = student.sex == true ? "1" : "0";
            kq += student.id + '*' + student.name + '*' + student.birthday + '*' + student.address + '*' + student.classroom + '*' + student.path + '*' + sex + '*' +
                student.email + '*' + student.phone;
            return kq;
        }
    }
}
