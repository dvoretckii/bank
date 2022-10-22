﻿using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class ExtraStudent
{
    private Student _student;
    private Schedule _schedule;
    private ExtraGroup _extraGroup;
    private List<Flow> _flows;

    public ExtraStudent(string name, ExtraGroup extraGroup)
    {
        _student = new Student(name, extraGroup.GetGroup());
        _schedule = new Schedule();
        _extraGroup = new ExtraGroup(extraGroup);
        _flows = new List<Flow>();
    }

    public Student GetStudent()
    {
        Student student = _student;
        return student;
    }

    public Schedule GetStudentSchedule()
    {
        var schedule = new Schedule(_schedule);
        return schedule;
    }

    public ExtraGroup GetExtraGroup()
    {
        ExtraGroup extraGroup = _extraGroup;
        return extraGroup;
    }

    public void SetSchedule(Schedule schedule)
    {
        _schedule = schedule;
    }

    public void SetExtraGroup(ExtraGroup extraGroup)
    {
        _extraGroup = extraGroup;
    }

    public List<Flow> GetFlows()
    {
        var flows = new List<Flow>(_flows);
        return flows;
    }

    public void AddFlow(Flow flow)
    {
        _flows.Add(flow);
    }

    public void RemoveFlow(Flow flow)
    {
        _flows.Remove(flow);
    }
}