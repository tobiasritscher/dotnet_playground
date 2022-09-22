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

        public override string ToString()
        {
            var statistics = GetStatistics();

            return $"The Book \"{Name}\" contains {statistics.GradeCount} grades.\n" +
            $"Avarage grade: {statistics.Average:N2}\nMinimum grade: {statistics.Low:N2}\nMaximum grade: {statistics.High:N2}\nLetter grade: {statistics.Letter}";
        }
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate? GradeAdded = null;

        public override void AddGrade(double grade)
        {
            using(var fileWriter = File.AppendText(Name + ".txt"))
            {
                fileWriter.WriteLine(grade);

                if (GradeAdded != null) {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override BookStatistics GetStatistics()
        {
            BookStatistics statistics = new BookStatistics();
            
            var grades = new List<double>();

            using(var reader = File.OpenText(Name + ".txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    grades.Add(Convert.ToDouble(line));
                    line = reader.ReadLine();
                }
            }

            statistics.CalculateStatistics(grades);

            return statistics;
        }
    }

    public class InMemoryBook : Book
    {
        public override event GradeAddedDelegate? GradeAdded = null;

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
            statistics.CalculateStatistics(grades);

            return statistics;
        }
    }
}