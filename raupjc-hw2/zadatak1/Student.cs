using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace zadatak1
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public static bool operator ==(Student s1, Student s2)
        {
            return s1.Jmbag.Equals(s2.Jmbag);
        }

        public static bool operator !=(Student s1, Student s2)
        {
            return !s1.Jmbag.Equals(s2.Jmbag);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            };
            if (!(obj is Student))
            {
                return false;
            }
            return Jmbag.Equals(((Student) obj).Jmbag);
        }

        public override int GetHashCode()
        {
            return Jmbag.GetHashCode();
        }
    }
    public enum Gender
    {
        Male, Female
    }
}
