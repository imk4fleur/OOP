namespace Lab1.Courses;

public class OfflineCourse : Course
{
    public string Room { get; }

    public OfflineCourse(string name, string room) : base(name)
    {
        Room = room;
    }

    public override string GetCourseType() => "Offline";
}

