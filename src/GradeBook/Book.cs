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

        public Book(string name)
        {
            grades = new List<double>();
            this.name = name;
        }

        public void AddGrade(double grade) 
        {
            if (grade <= 100 && grade >= 0){
                grades.Add(grade);
            }
            else 
            {
                throw new ArgumentException($"The {nameof(grade)} should be between 0 and 100");
            }
        }

        public string GetName() 
        {
            return name;
        }

        public void ChangeName(string name) {
            this.name = name;
        }

        public BookStatistics GetStatistics()
        {
            BookStatistics statistics = new BookStatistics();

            statistics.GradeCount = grades.Count;
            statistics.Average = CalculateSumOfGrades() / statistics.GradeCount;
            statistics.High = grades.Max();
            statistics.Low = grades.Min();

            switch(statistics.Average)
            {
                case var d when d >= 90.0:
                    statistics.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    statistics.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    statistics.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    statistics.Letter = 'D';
                    break;
                default:
                    statistics.Letter = 'F';
                    break;
            }

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