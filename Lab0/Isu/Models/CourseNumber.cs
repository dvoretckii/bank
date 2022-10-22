using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    private char _courseNumber;

    public CourseNumber(char courseNumber)
    {
        if (courseNumber is < '1' or > '4')
        {
            throw new GroupNameException("Course should be from 1 to 4");
        }

        _courseNumber = courseNumber;
    }

    public CourseNumber()
    {
        _courseNumber = '\0';
    }

    public char GetCourseNumber() => _courseNumber;
}