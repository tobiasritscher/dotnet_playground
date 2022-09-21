﻿// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var gradeBook = new Book(stringArrayToDoubleList(args));

            gradeBook.AddGrade(56.1);

            if (args.Length > 0)
            {
                var statistics = gradeBook.GetStatistics(); 
                Console.WriteLine($"The Book \"{gradeBook.GetName()}\" contains {statistics.GradeCount} grades.");
                Console.WriteLine($"Avarage grade: {statistics.Average:N2}\nMinimum grade: {statistics.Low:N2}\nMaximum grade: {statistics.High:N2}"); 
            }
            else
            {
                Console.WriteLine("Hello!");
            }
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
    }
}