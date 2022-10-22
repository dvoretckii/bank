using Isu.Exceptions;

namespace Isu.Services;
using Entities;
using Models;

public class IsuService : IIsuService
{
    private List<Group> _groups;

    public IsuService()
    {
        _groups = new List<Group>();
    }

    public Group AddGroup(GroupName name)
    {
        var group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        if (!_groups.Contains(group))
        {
            throw new GroupException("No such group");
        }

        var student = new Student(name, group);
        group.AddStudent(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        foreach (Group group in _groups)
        {
            foreach (Student student in group.GetStudents() !)
            {
                if (student.GetStudentId().GetStudentID() == id)
                {
                    return student;
                }
            }
        }

        throw new StudentException("No such student");
    }

    public Student? FindStudent(int id)
    {
        foreach (Group group in _groups)
        {
            foreach (Student student in group.GetStudents() !)
            {
                if (student.GetStudentId().GetStudentID() == id)
                {
                    return student;
                }
            }
        }

        return null;
    }

    public List<Student>? FindStudents(GroupName groupName)
    {
        foreach (Group group in _groups.Where(group => group.GetGroupName().GetGroupName() == groupName.GetGroupName()))
        {
            return group.GetStudents();
        }

        return null;
    }

    public List<Student>? FindStudents(CourseNumber courseNumber)
    {
        var fullList = new List<Student>();
        foreach (Group group in _groups.Where(group => group.GetGroupName().GetCourseNumber().GetCourseNumber() == courseNumber.GetCourseNumber()))
        {
            fullList.AddRange(group.GetStudents() !);
        }

        return fullList.Count == 0 ? null : fullList;
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.FirstOrDefault(group => group.GetGroupName().GetGroupName() == groupName.GetGroupName());
    }

    public List<Group>? FindGroups(CourseNumber courseNumber)
    {
        var groups = _groups.Where(group => group.GetGroupName().GetCourseNumber().GetCourseNumber() == courseNumber.GetCourseNumber()).ToList();

        return groups.Count == 0 ? null : groups;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (FindStudent(student.GetStudentId().GetStudentID()) is null)
        {
            throw new StudentException("No such student");
        }

        if (FindGroup(newGroup.GetGroupName()) is null)
        {
            throw new GroupException("No such group");
        }

        student.ChangeStudentGroup(newGroup);
    }

    public List<Group> GetGroups()
    {
        List<Group> groups = _groups;
        return groups;
    }
}