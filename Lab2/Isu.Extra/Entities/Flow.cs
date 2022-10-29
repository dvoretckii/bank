using System.Runtime.InteropServices.ComTypes;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Flow
{
    private List<ExtraStudent> _students = new List<ExtraStudent>();
    private Schedule _schedule = new Schedule();
    private int _maxNumberOfStudents;
    private AdditionalSubject _additionalSubject = new AdditionalSubject();

    public Flow(Schedule schedule)
    {
        _students = new List<ExtraStudent>();
        _schedule = schedule;
        _maxNumberOfStudents = 30;
        _additionalSubject = new AdditionalSubject();
    }

    public Flow(Flow flow)
    {
        var students = new List<ExtraStudent>(_students);
        var schedule = new Schedule(_schedule);
        int maxNumberOfStudents = _maxNumberOfStudents;
        var additionalSubject = new AdditionalSubject(_additionalSubject);
    }

    public Flow()
    {
        _students = new List<ExtraStudent>();
        _schedule = new Schedule();
        _maxNumberOfStudents = 30;
        _additionalSubject = new AdditionalSubject();
    }

    public void SetAdditionalSubject(AdditionalSubject additionalSubject)
    {
        _additionalSubject = additionalSubject;
    }

    public Schedule GetSchedule()
    {
        var schedule = new Schedule(_schedule);
        return schedule;
    }

    public AdditionalSubject GetAdditionalSubject()
    {
        var additionalSubject = new AdditionalSubject(_additionalSubject);
        return additionalSubject;
    }

    public int GetMaxNumberOfStudents()
    {
        int cp = _maxNumberOfStudents;
        return cp;
    }

    public List<ExtraStudent> GetExtraStudents()
    {
        var students = new List<ExtraStudent>(_students);
        return students;
    }

    public void AddExtraStudentToFlow(ExtraStudent student)
    {
        if (_students.Contains(student))
        {
            throw new ExtraStudentException("This student was already assigned to this flow");
        }

        _students.Add(student);
    }

    public void RemoveExtraStudentFromFlow(ExtraStudent student)
    {
        if (!_students.Contains(student))
        {
            throw new ExtraStudentException("This student was not already assigned to this flow");
        }

        _students.Remove(student);
    }
}