namespace GradeBook
{
    public class Book
    {
        private List<double> grades;
        private string name;
        private static int bookCount = 0;

        public Book()
        {
            grades = new List<double>();
            name = $"Book_{++bookCount}";
        }

        public Book(List<double> values)
        {
            grades = values;
            name = $"Book_{++bookCount}";
        }

        public void AddGrade(double grade) 
        {
            grades.Add(grade);
        }

        public string GetName() 
        {
            return name;
        }

        public BookStatistics GetStatistics()
        {
            BookStatistics statistics = new BookStatistics();

            statistics.GradeCount = grades.Count;
            statistics.Average = CalculateSumOfGrades() / statistics.GradeCount;
            statistics.High = grades.Max();
            statistics.Low = grades.Min();
            return statistics;
        }

        private double CalculateSumOfGrades()
        {
            double sum = 0;

            foreach (var grade in grades)
            {
                sum += grade;
            }

            return sum;
        }
    }
}