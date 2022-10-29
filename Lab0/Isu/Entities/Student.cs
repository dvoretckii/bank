using Isu.Exceptions;
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
        group.AddStudent(this);
        _group = group;
    }

    public void SetGroup(Group group)
    {
        _group = group;
    }

    public Group GetGroup() => _group;

    public StudentId GetStudentId() => _id;

    public string GetName() => _name;

    public void ChangeStudentGroup(Group group)
    {
        if (group.GetStudents() !.Count == 20)
        {
            throw new GroupException("Can't add student to this group. Group is full");
        }

        GetGroup().DeleteStudent(this);
        group.AddStudent(this);
    }
}