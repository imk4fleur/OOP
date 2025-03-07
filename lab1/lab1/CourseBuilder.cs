using Lab1.Courses;

namespace Lab1;

public class CourseBuilder
{
    private string _name;
    private string _location;
    private bool _isOnline;

    public CourseBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public CourseBuilder SetOnline(string platform)
    {
        _location = platform;
        _isOnline = true;
        return this;
    }

    public CourseBuilder SetOffline(string room)
    {
        _location = room;
        _isOnline = false;
        return this;
    }

    public Course Build()
    {
        if (_isOnline)
            return new OnlineCourse(_name, _location);
        else
            return new OfflineCourse(_name, _location);
    }
}

