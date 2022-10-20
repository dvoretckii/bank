using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private GroupName _groupName;
    private List<Student> _listOfStudents;
    private int _maxNumberOfStudents = 20;
    public Group()
    {
        _groupName = new GroupName();
        _listOfStudents = new List<Student>();
    }

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

        _listOfStudents.Add(student);
        student.SetGroup(this);
    }

    public void AddStudents(List<Student> students)
    {
        foreach (Student student in students)
        {
            AddStudent(student);
        }
    }

    public void DeleteStudent(Student student)
    {
        if (!_listOfStudents.Contains(student))
        {
            throw new GroupException("No such student in this group");
        }

        _listOfStudents.Remove(student);
    }

    public List<Student> GetStudents()
    {
        List<Student> cp = _listOfStudents;
        return cp;
    }

    public GroupName GetGroupName()
    {
        GroupName groupName = _groupName;
        return groupName;
    }
}