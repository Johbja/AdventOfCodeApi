namespace AdventOfCode.Infrastructure.Attributes;

public class DayInfo : Attribute
{
    public string SolutionName { get; set; }
    public string Day { get; set; }

    public DayInfo(string solutionName, string day) 
    {
        SolutionName = solutionName;
        Day = day;
    }

}
