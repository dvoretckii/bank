using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtra : IsuService, IIsuExtra
{
    private List<AdditionalSubject> _additionalSubjects = new List<AdditionalSubject>();
    private List<ExtraGroup> _groups = new List<ExtraGroup>();

    public List<AdditionalSubject> GetAdditionalSubjects()
    {
        var subjects = new List<AdditionalSubject>(_additionalSubjects);
        return subjects;
    }

    public AdditionalSubject CreateAdditionalSubject(string name, char megafaculty, List<Flow> flows)
    {
        var additionalSubject = new AdditionalSubject(name, megafaculty, flows);
        if (_additionalSubjects.Contains(additionalSubject))
        {
            throw new AdditionalSubjectException("This additional subject has been already created");
        }

        _additionalSubjects.Add(additionalSubject);
        return additionalSubject;
    }

    public void AssignAdditionalSubjectToStudent(ExtraStudent student, AdditionalSubject additionalSubject)
    {
        if (!_additionalSubjects.Contains(additionalSubject))
        {
            throw new AdditionalSubjectException("No such additional subject");
        }

        if (student.GetFlows().Count == 2)
        {
            throw new AdditionalSubjectException("Student has max number of additional subjects");
        }

        if (student.GetStudent().GetGroup().GetGroupName().GetFaculty() == additionalSubject.GetMegafaculty())
        {
            throw new AdditionalSubjectException(
                "Student should choose additional subject which is different from his megafaculty");
        }

        if (student.GetFlows().Any(flow => flow.GetAdditionalSubject().GetName() == additionalSubject.GetName() && flow.GetAdditionalSubject().GetMegafaculty() == additionalSubject.GetMegafaculty()))
        {
            throw new AdditionalSubjectException("Student should choose another additional subject");
        }

        List<Flow> flows = additionalSubject.GetFlows();
        foreach (Flow flow in flows.Where(flow => student.GetStudentSchedule().MergeSchedule(flow.GetSchedule()) is not null && flow.GetMaxNumberOfStudents() != flow.GetExtraStudents().Count))
        {
            student.SetSchedule(student.GetStudentSchedule().MergeSchedule(flow.GetSchedule()) !);
            student.AddFlow(flow);
            flow.AddExtraStudentToFlow(student);
            additionalSubject.SetFlows(flows);
            return;
        }

        throw new ScheduleException("No available flow for this additional subject");
    }

    public void DeleteStudentFromAdditionalSubject(ExtraStudent student, AdditionalSubject additionalSubject)
    {
        bool flg = false;
        foreach (Flow flow in additionalSubject.GetFlows().Where(flow => student.GetFlows().Contains(flow) && flow.GetExtraStudents().Contains(student)))
        {
            flg = true;
        }

        if (!flg)
        {
            throw new AdditionalSubjectException("No such student on this additional subject");
        }

        List<Flow> flows = additionalSubject.GetFlows();
        foreach (Flow flow in flows.Where(flow => student.GetFlows().Contains(flow) && flow.GetExtraStudents().Contains(student)))
        {
            student.SetSchedule(student.GetStudentSchedule().ComplementSchedule(flow.GetSchedule()) !);
            flow.GetExtraStudents().Remove(student);
            student.RemoveFlow(flow);
            flow.RemoveExtraStudentFromFlow(student);
        }

        additionalSubject.SetFlows(flows);
    }

    public List<Flow> GetFlowsForSubject(AdditionalSubject subject)
    {
        if (!_additionalSubjects.Contains(subject))
        {
            throw new AdditionalSubjectException("No such additional subject in the base");
        }

        return subject.GetFlows();
    }

    public List<ExtraStudent> GetStudentsFromSubject(AdditionalSubject subject)
    {
        var students = new List<ExtraStudent>();
        foreach (Flow flow in subject.GetFlows())
        {
            students.AddRange(flow.GetExtraStudents());
        }

        return students;
    }

    public List<ExtraStudent> GetStudentsWithoutAnyAdditionalSubject(ExtraGroup group)
    {
        return group.GetExtraStudents().Where(student => student.GetFlows().Count == 0).ToList();
    }
}