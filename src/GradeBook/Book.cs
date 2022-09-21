namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public String Name 
        {
            get;
            set;
        }
    }

    public interface IBook 
    {
        void AddGrade(double grade);
        BookStatistics GetStatistics();
        string Name {get;}
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public virtual BookStatistics GetStatistics()
        {
            throw new NotImplementedException();
        }
    }

    public class InMemoryBook : Book
    {
        public override event GradeAddedDelegate? GradeAdded = null;

        readonly string category = "science";
        public const string IDENTIFIER = "CH12434";


        private List<double> grades;
        private static int bookCount = 0;

        public InMemoryBook() : base($"Book_{++bookCount}")
        {
            grades = new List<double>();
            Name = $"Book_{++bookCount}";
        }

        public InMemoryBook(List<double> values) : base($"Book_{++bookCount}")
        {
            grades = values;
            Name = $"Book_{++bookCount}";
        }

        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public void AddGrade(char grade)
        {
            switch(grade)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    throw new ArgumentException("The minimum letter grade is a D, everything else has to be a number!");
            }
        }

        public override void AddGrade(double grade) 
        {
            if (grade <= 100 && grade >= 0){
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else 
            {
                throw new ArgumentException($"The {nameof(grade)} should be between 0 and 100");
            }
        }

        public override BookStatistics GetStatistics()
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

        public override string ToString()
        {
            var statistics = GetStatistics();

            return $"The Book \"{Name}\" is in the category {category} and contains {statistics.GradeCount} grades.\n" +
            $"Avarage grade: {statistics.Average:N2}\nMinimum grade: {statistics.Low:N2}\nMaximum grade: {statistics.High:N2}\nLetter grade: {statistics.Letter}";
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