using Isu.Extra.Entities;

namespace Isu.Extra.Services;

public interface IIsuExtra
{
    public AdditionalSubject CreateAdditionalSubject(string name, char megafaculty, List<Flow> flows);
    public void AssignAdditionalSubjectToStudent(ExtraStudent student, AdditionalSubject additionalSubject);
    public void DeleteStudentFromAdditionalSubject(ExtraStudent student, AdditionalSubject additionalSubject);
    public List<Flow> GetFlowsForSubject(AdditionalSubject subject);
    public List<ExtraStudent> GetStudentsFromSubject(AdditionalSubject subject);
    public List<ExtraStudent> GetStudentsWithoutAnyAdditionalSubject(ExtraGroup group);
}