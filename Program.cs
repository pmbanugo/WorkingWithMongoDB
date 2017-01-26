using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WorkingWithMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            //Console.ForegroundColor = ConsoleColor.Yellow;

            MainAsync().Wait();
            Console.WriteLine();
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        static async Task MainAsync()
        {

            var client = new MongoClient();

            IMongoDatabase db = client.GetDatabase("schoool");

            var collection = db.GetCollection<Student>("students");

            await collection.Find(student => student.Age < 25 && student.FirstName != "Peter")
                .ForEachAsync(student => Debug.WriteLine($"Id: {student.Id}, FirstName: {student.FirstName}, LastName: {student.LastName}"));
        }

        static async Task MainAsync2()
        {

            var client = new MongoClient();

            var db = client.GetDatabase("schoool");

            var collection = db.GetCollection<Student>("students");

            int count = 1;
            await collection.Find(FilterDefinition<Student>.Empty)
                .Project(x => new { x.FirstName, x.LastName })
                .ForEachAsync(
                    student =>
                    {
                        Debug.WriteLine($"{count}. \t FirstName: {student.FirstName} - LastName {student.LastName}");
                        count++;
                    });

            Debug.WriteLine("");
        }

        private static IEnumerable<Student> CreateNewStudents()
        {
            var student1 = new Student
            {
                FirstName = "Gregor",
                LastName = "Felix",
                Subjects = new List<string>() { "English", "Mathematics", "Physics", "Biology" },
                Class = "JSS 3",
                Age = 23
            };

            var student2 = new Student
            {
                FirstName = "Machiko",
                LastName = "Elkberg",
                Subjects = new List<string> { "English", "Mathematics", "Spanish" },
                Class = "JSS 3",
                Age = 23
            };

            var student3 = new Student
            {
                FirstName = "Julie",
                LastName = "Sandal",
                Subjects = new List<string> { "English", "Mathematics", "Physics", "Chemistry" },
                Class = "JSS 1",
                Age = 25
            };

            var student4 = new Student
            {
                FirstName = "Peter",
                LastName = "Cyborg",
                Subjects = new List<string> { "English", "Mathematics", "Physics", "Chemistry" },
                Class = "JSS 1",
                Age = 39
            };

            var student5 = new Student
            {
                FirstName = "James",
                LastName = "Cyborg",
                Subjects = new List<string> { "English", "Mathematics", "Physics", "Chemistry" },
                Class = "JSS 1",
                Age = 39
            };

            var newStudents = new List<Student> { student1, student2, student3, student4, student5 };

            return newStudents;
        }
    }
}
