// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook gradeBook;
            if (args[0] == "Disk")
            {
                gradeBook = new DiskBook("My New Disk Book");
            }
            else
            {
                gradeBook = new InMemoryBook("My New Memory Book");
            }
            gradeBook.GradeAdded += OnGradeAdded;
            InputGrades(gradeBook);

            try
            {
                Console.WriteLine(gradeBook.ToString());
            }
            catch
            {
                Console.WriteLine("No Data!");
            }
        }

        private static void InputGrades(IBook gradeBook)
        {
            var input = "q";
            do
            {
                Console.WriteLine("Please Enter a grade [0-100] or 'q' to quit: ");
                input = Console.ReadLine();

                if (input != "q")
                {
                    try
                    {
                        gradeBook.AddGrade(Convert.ToDouble(input));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }
            } while (input != "q");
        }

        static private List<double> stringArrayToDoubleList(string[] strings)
        {
            List<double> grades = new List<double>();

            foreach (var item in strings)
            {
                try
                {
                    grades.Add(Convert.ToDouble(item));
                }
                catch (System.Exception)
                {
                    Console.WriteLine($"There was a wrong input: \"{item}\". This input was skipped!\n");
                }                
            }

            return grades;
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}