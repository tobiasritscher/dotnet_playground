namespace GradeBook
{
    public class BookStatistics
    {
        public double Low, High, Average, GradeCount;
        public char Letter;       

        public void CalculateStatistics(List<double> grades) 
        {
            GradeCount = grades.Count;
            Average = grades.Sum() / GradeCount;
            High = grades.Max();
            Low = grades.Min();

            switch(Average)
            {
                case var d when d >= 90.0:
                    Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    Letter = 'D';
                    break;
                default:
                    Letter = 'F';
                    break;
            }
        }
    }
}