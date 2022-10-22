using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private string _name;
    private StudentId _id;
    private Group _group;

    public Student(string name, Group group)
    {
        _name = name;
        _id = new StudentId();
        _group = group;
    }

    public void SetGroup(Group group)
    {
        _group = group;
    }

    public Group GetGroup()
    {
        Group cp = _group;
        return cp;
    }

    public StudentId GetStudentId()
    {
        StudentId id = _id;
        return id;
    }

    public string GetName()
    {
        string name = _name;
        return name;
    }

    public void ChangeStudentGroup(Group group)
    {
        GetGroup().DeleteStudent(this);
        group.AddStudent(this);
    }
}