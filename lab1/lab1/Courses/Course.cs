namespace Lab1.Courses;

public abstract class Course
{
    public string Name { get; protected set; }
    public Teacher AssignedTeacher { get; set; }
    public List<Student> Students { get; } = new List<Student>();

    protected Course(string name)
    {
        Name = name;
    }

    public abstract string GetCourseType();
}

