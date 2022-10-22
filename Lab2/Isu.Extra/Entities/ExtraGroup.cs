using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class ExtraGroup
{
    private Group _group;
    private List<ExtraStudent> _students;
    private Schedule _schedule;

    public ExtraGroup(GroupName groupName, Schedule schedule)
    {
        _group = new Group(groupName);
        _schedule = new Schedule(schedule);
        _students = new List<ExtraStudent>();
    }

    public ExtraGroup(ExtraGroup extraGroup)
    {
        _group = extraGroup._group;
        _schedule = extraGroup._schedule;
        _students = extraGroup._students;
    }

    public Group GetGroup()
    {
        return _group;
    }

    public List<ExtraStudent> GetExtraStudents()
    {
        var students = new List<ExtraStudent>(_students);
        return students;
    }

    public Schedule GetGroupSchedule()
    {
        var schedule = new Schedule(_schedule);
        return schedule;
    }

    public void SetGroupSchedule(Schedule schedule)
    {
        _schedule = schedule;
    }

    public void SetGroup(Group group)
    {
        _group = group;
    }

    public void AddExtraStudent(ExtraStudent extraStudent)
    {
        if (_group.GetStudents() !.Count == 20)
        {
            throw new ExtraGroupException("Can't add ExtraStudent. This ExtraGroup is full");
        }

        if (_students.Contains(extraStudent))
        {
            throw new ExtraGroupException("This ExtraStudent is already in this ExtraGroup");
        }

        _students.Add(extraStudent);
        extraStudent.SetExtraGroup(this);
        extraStudent.SetSchedule(_schedule);
    }

    public void RemoveExtraStudent(ExtraStudent extraStudent)
    {
        if (!_students.Contains(extraStudent))
        {
            throw new ExtraGroupException("No such ExtraStudent in this ExtraGroup");
        }

        _group.DeleteStudent(extraStudent.GetStudent());
        _students.Remove(extraStudent);
    }

    public void ChangeExtraStudentExtraGroup(ExtraStudent extraStudent, ExtraGroup extraGroup)
    {
        RemoveExtraStudent(extraStudent);
        extraGroup.AddExtraStudent(extraStudent);
    }
}