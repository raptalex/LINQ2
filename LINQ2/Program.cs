class Program
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double Tuition { get; set; }
    }
    public class StudentClubs
    {
        public int StudentID { get; set; }
        public string ClubName { get; set; }
    }
    public class StudentGPA
    {
        public int StudentID { get; set; }
        public double GPA { get; set; }
    }
    public static void Main()
    {
        IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 1, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 2, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 3, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 5, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
        };
        // Student GPA Collection
        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
        // Club collection
        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };

        var groupGPA = studentGPAList.GroupBy(a => a.GPA);

        foreach (var group in groupGPA)
        {
            foreach (var val in group)
                Console.WriteLine($"ID: {val.StudentID}");
        }

        Console.WriteLine();

        var clubGroup = studentClubList.OrderBy(a => a.ClubName).GroupBy(a => a.ClubName);

        foreach (var group in clubGroup)
            foreach (var val in group)
                Console.WriteLine($"ID: {val.StudentID}");

        var decentToGoodGPA = studentGPAList.Where(a => a.GPA > 2.5 && a.GPA < 4.0).Count();

        Console.WriteLine($"There are {decentToGoodGPA} students who have a GPA between 2.5 and 4.0");

        var tuitionAvg = studentList.Average(a => a.Tuition);

        Console.WriteLine($"The average tuition for a student is ${tuitionAvg:.02}");

        var highestTuition = studentList.Max(a => a.Tuition);
        var studentHighestTuition = studentList.Where(a => a.Tuition == highestTuition).First();

        Console.WriteLine($"Name: {studentHighestTuition.StudentName}");
        Console.WriteLine($"Major: {studentHighestTuition.Major}");
        Console.WriteLine($"Tuition: ${studentHighestTuition.Tuition:.02}");

        /*
         *         var innerJoin = studentList.Join(studentClubList,
                                student => student.StudentID,
                                club => club.StudentID,
                                (student, club) => new
                                {
                                    StudentName = student.StudentName,
                                    Age = student.Age,
                                    ClubName = club.ClubName
                                });
        */

        var gpaJoin = studentList.Join(studentGPAList,
            student => student.StudentID,
            gpa => gpa.StudentID,
            (student, gpa) => new
            {
                Name = student.StudentName,
                Major = student.Major,
                Student_GPA = gpa.GPA,
            });

        foreach (var v in gpaJoin)
        {
            Console.WriteLine($"Name: {v.Name}");
            Console.WriteLine($"Major: {v.Major}");
            Console.WriteLine($"GPA: {v.Student_GPA}");
        }

        var clubJoin = studentList.Join(studentClubList,
            student => student.StudentID,
            club => club.StudentID,
            (student, club) => new
            {
                Name = student.StudentName,
                ClubName = club.ClubName,
            }).Where(a => a.ClubName == "Game");

        Console.WriteLine("Names of students in the game club.");
        foreach(var v in clubJoin)
        {
            Console.WriteLine(v.Name);
        }
    }
}