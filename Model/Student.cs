using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_Application.Model
{
    public class Student
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public DateTime birthday { get; set; }
        public bool sex { get; set; }
        public string classroom { get; set; }
        public string phone { get; set; }
        public string path { get; set; }

        public Student(string id, string name, string email, string address, DateTime birthday, bool sex, string classroom, string phone, string path)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.address = address;
            this.birthday = birthday;
            this.sex = sex;
            this.classroom = classroom;
            this.phone = phone;
            this.path = path;
        }

        public Student(string id, string name, DateTime birthday, bool sex)
        {
            this.id = id;
            this.name = name;
            this.birthday = birthday;
            this.sex = sex;
        }

        public Student()
        {
        }
    }
}
