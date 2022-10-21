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
        foreach (Student? student in from @group in _groups from student in @group.GetStudents() where student.GetStudentId().GetStudentID() == id select student)
        {
            return student;
        }

        throw new StudentException("No such student");
    }

    public Student? FindStudent(int id)
    {
        return _groups.SelectMany(group => group.GetStudents()).FirstOrDefault(student => student.GetStudentId().GetStudentID() == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        foreach (Group group in _groups.Where(group => group.GetGroupName().GetGroupName() == groupName.GetGroupName()))
        {
            return group.GetStudents();
        }

        throw new GroupException("No such group");
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var fullList = new List<Student>();
        foreach (Group group in _groups.Where(group => group.GetGroupName().GetCourseNumber().GetCourseNumber() == courseNumber.GetCourseNumber()))
        {
            fullList.AddRange(group.GetStudents());
        }

        if (fullList.Count == 0)
        {
            throw new CourseNumberException("No students in such course");
        }

        return fullList;
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.FirstOrDefault(group => group.GetGroupName().GetGroupName() == groupName.GetGroupName());
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        var groups = _groups.Where(group => group.GetGroupName().GetCourseNumber().GetCourseNumber() == courseNumber.GetCourseNumber()).ToList();

        if (groups.Count == 0)
        {
            throw new CourseNumberException("No groups in such course");
        }

        return groups;
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

        student.GetGroup().DeleteStudent(student);
        newGroup.AddStudent(student);
    }

    public List<Group> GetGroups()
    {
        List<Group> groups = _groups;
        return groups;
    }
}