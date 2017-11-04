using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zadatak1;

namespace zadatak4
{
    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            return intArray.GroupBy(number => number).OrderBy(grouping => grouping.Key)
                .Select(grouping =>
                    $"Broj {grouping.Key} ponavlja se {grouping.ToList().Count} puta").ToArray();
            
        }

        public static University[] Linq2_1(University[] universityArray)
        {
            return universityArray.Where(univ => univ.Students.Where(st=>st.Gender==Gender.Female).ToArray().Length==0).ToArray();
        }

        public static University[] Linq2_2(University[] universityArray)
        {
            double avgStudents = universityArray.Select(univ => univ.Students.Length).Average();
            return  universityArray.Where(univ=>univ.Students.Length<avgStudents).ToArray();
        }

        public static Student[] Linq2_3(University[] universityArray)
        {
            return universityArray.SelectMany(univ=>univ.Students).Distinct().ToArray();
        }

        public static Student[] Linq2_4(University[] universityArray)
        {
            return universityArray.Where(univ => univ.Students.Where(st => st.Gender == Gender.Female).ToArray().Length == 0 || univ.Students.Where(st => st.Gender == Gender.Male).ToArray().Length == 0).SelectMany(univ=>univ.Students).Distinct().ToArray();
        }

        public static Student[] Linq2_5(University[] universityArray)
        {
            return universityArray.SelectMany(univ => univ.Students).Distinct().Where(student => universityArray.Where(univ=>univ.Students.Contains(student)).ToArray().Length>=2).ToArray();
        }

    }
}
