using Lab1.Courses;

namespace Lab1;

public class CourseManager
{
    private static readonly CourseManager _instance = new CourseManager();
    private readonly List<Course> _courses = new List<Course>();

    private CourseManager() { }

    public static CourseManager Instance => _instance;

    public void AddCourse(Course course)
    {
        if (course != null)
            _courses.Add(course);
    }

    public void RemoveCourse(Course course)
    {
        if (course != null)
            _courses.Remove(course);
    }

    public List<Course> GetCoursesByTeacher(Teacher teacher)
    {
        return _courses.Where(c => c.AssignedTeacher == teacher).ToList();
    }
}

