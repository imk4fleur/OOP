namespace Lab1.Courses;

public class OnlineCourse : Course
{
    public string Platform { get; }

    public OnlineCourse(string name, string platform) : base(name)
    {
        Platform = platform;
    }

    public override string GetCourseType() => "Online";
}

