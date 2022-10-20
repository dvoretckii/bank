namespace Isu.Models;

public class StudentId
{
    private static int _currentId = 100000;
    private int _id;

    public StudentId()
    {
        _id = _currentId;
        _currentId++;
    }

    public int GetStudentID()
    {
        int id = _id;
        return id;
    }
}