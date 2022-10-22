using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private GroupName _groupName;
    private List<Student> _listOfStudents;
    private int _maxNumberOfStudents = 20;

    public Group(GroupName groupName)
    {
        _groupName = groupName;
        _listOfStudents = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        if (_listOfStudents.Count == _maxNumberOfStudents)
        {
            throw new StudentException("The group is full. Service can not add student");
        }

        if (_listOfStudents.Contains(student))
        {
            throw new StudentException("Student already in the group");
        }

        _listOfStudents.Add(student);
        student.SetGroup(this);
    }

    public void DeleteStudent(Student student)
    {
        if (!_listOfStudents.Remove(student))
        {
            throw new GroupException("No such student in this group");
        }
    }

    public List<Student>? GetStudents()
    {
        var cp = new List<Student>(_listOfStudents);
        return cp;
    }

    public GroupName GetGroupName()
    {
        GroupName groupName = _groupName;
        return groupName;
    }
}