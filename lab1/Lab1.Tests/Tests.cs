using Lab1.Courses;

namespace Lab1.Tests
{
    public class CourseManagerTests
    {
        [Fact]
        public void AddCourse_ShouldIncreaseCourseCount()
        {
            var courseManager = CourseManager.Instance;
            var course = new OnlineCourse("C# Basics", "Zoom");

            courseManager.AddCourse(course);
            var courses = courseManager.GetCoursesByTeacher(null);

            Assert.Contains(course, courses);
        }

        [Fact]
        public void RemoveCourse_ShouldDecreaseCourseCount()
        {
            var courseManager = CourseManager.Instance;
            var course = new OnlineCourse("C# Basics", "Zoom");

            courseManager.AddCourse(course);
            courseManager.RemoveCourse(course);
            var courses = courseManager.GetCoursesByTeacher(null);

            Assert.DoesNotContain(course, courses);
        }

        [Fact]
        public void GetCoursesByTeacher_ShouldReturnCorrectCourses()
        {
            var courseManager = CourseManager.Instance;
            var teacher = new Teacher("John Doe");
            var course1 = new OnlineCourse("C# Basics", "Zoom") { AssignedTeacher = teacher };
            var course2 = new OfflineCourse("OOP Principles", "Room 101") { AssignedTeacher = teacher };
            var course3 = new OnlineCourse("Advanced C#", "Teams");

            courseManager.AddCourse(course1);
            courseManager.AddCourse(course2);
            courseManager.AddCourse(course3);

            var teacherCourses = courseManager.GetCoursesByTeacher(teacher);

            Assert.Contains(course1, teacherCourses);
            Assert.Contains(course2, teacherCourses);
            Assert.DoesNotContain(course3, teacherCourses);
        }

        [Fact]
        public void CourseBuilder_ShouldCreateOnlineCourse()
        {
            var builder = new CourseBuilder()
                .SetName("C# Advanced")
                .SetOnline("Zoom");

            var course = builder.Build();

            Assert.IsType<OnlineCourse>(course);
            Assert.Equal("C# Advanced", course.Name);
            Assert.Equal("Online", course.GetCourseType());
        }

        [Fact]
        public void CourseBuilder_ShouldCreateOfflineCourse()
        {
            var builder = new CourseBuilder()
                .SetName("C# Workshop")
                .SetOffline("Room 202");

            var course = builder.Build();

            Assert.IsType<OfflineCourse>(course);
            Assert.Equal("C# Workshop", course.Name);
            Assert.Equal("Offline", course.GetCourseType());
        }

        [Fact]
        public void AssignTeacherToCourse_ShouldSetTeacher()
        {
            var course = new OnlineCourse("Data Science", "Udemy");
            var teacher = new Teacher("Jane Smith");

            course.AssignedTeacher = teacher;

            Assert.Equal(teacher, course.AssignedTeacher);
        }

        [Fact]
        public void AddStudentToCourse_ShouldIncreaseStudentCount()
        {
            var course = new OnlineCourse("Machine Learning", "Coursera");
            var student = new Student("Alice Johnson");

            course.Students.Add(student);

            Assert.Contains(student, course.Students);
        }
    }

}