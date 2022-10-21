using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTest : IsuService
{
    [Fact]
    public void AddNewAdditionalSubject()
    {
        var isu = new IsuExtra();
        var lessonTime = new LessonTime(1, 1);
        var audience = new Audience(123);
        Lesson lesson = Lesson.Builder
            .WithLessonTime(lessonTime)
            .WithAudience(audience)
            .WithSubject("math")
            .WithTeacher("Vova")
            .Build();
        var lessons = new List<Lesson>();
        lessons.Add(lesson);
        var schedule = new Schedule(lessons);
        var flow = new Flow(schedule);
        var flows = new List<Flow>();
        flows.Add(flow);
        AdditionalSubject additionalSubject = isu.CreateAdditionalSubject("french", 'f', flows);
        Assert.Contains(additionalSubject, isu.GetAdditionalSubjects());
    }

    [Fact]
    public void AddStudentToSubject()
    {
        var isu = new IsuExtra();
        var lessonTime = new LessonTime(1, 1);
        var audience = new Audience(123);
        Lesson lesson = Lesson.Builder
            .WithLessonTime(lessonTime)
            .WithAudience(audience)
            .WithSubject("math")
            .WithTeacher("Vova")
            .Build();
        var lessons = new List<Lesson>();
        lessons.Add(lesson);
        var schedule = new Schedule(lessons);
        var flow = new Flow(schedule);
        var flows = new List<Flow>();
        flows.Add(flow);
        AdditionalSubject additionalSubject = isu.CreateAdditionalSubject("french", 'f', flows);
        var courseNumber = new CourseNumber('2');
        var groupName = new GroupName('M', '3', courseNumber, "90");
        var groupLessonTime = new LessonTime(2, 2);
        var groupAudience = new Audience(13);
        Lesson groupLesson = Lesson.Builder
            .WithLessonTime(groupLessonTime)
            .WithAudience(groupAudience)
            .WithSubject("chemistry")
            .WithTeacher("Masha")
            .Build();
        var groupLessons = new List<Lesson>();
        groupLessons.Add(groupLesson);
        var groupSchedule = new Schedule(groupLessons);
        var group = new ExtraGroup(groupName, groupSchedule);
        string name = "pasha";
        var student = new ExtraStudent(name);
        group.AddExtraStudent(student);
        isu.AssignAdditionalSubjectToStudent(student, additionalSubject);
        Assert.Contains(flow, student.GetFlows());
    }

    [Fact]
    public void RemoveStudentFromSubject()
    {
        var isu = new IsuExtra();
        var lessonTime = new LessonTime(1, 1);
        var audience = new Audience(123);
        Lesson lesson = Lesson.Builder
            .WithLessonTime(lessonTime)
            .WithAudience(audience)
            .WithSubject("math")
            .WithTeacher("Vova")
            .Build();
        var lessons = new List<Lesson>();
        lessons.Add(lesson);
        var schedule = new Schedule(lessons);
        var flow = new Flow(schedule);
        var flows = new List<Flow>();
        flows.Add(flow);
        AdditionalSubject additionalSubject = isu.CreateAdditionalSubject("french", 'f', flows);
        var courseNumber = new CourseNumber('2');
        var groupName = new GroupName('M', '3', courseNumber, "90");
        var groupLessonTime = new LessonTime(2, 2);
        var groupAudience = new Audience(13);
        Lesson groupLesson = Lesson.Builder
            .WithLessonTime(groupLessonTime)
            .WithAudience(groupAudience)
            .WithSubject("chemistry")
            .WithTeacher("Masha")
            .Build();
        var groupLessons = new List<Lesson>();
        groupLessons.Add(groupLesson);
        var groupSchedule = new Schedule(groupLessons);
        var group = new ExtraGroup(groupName, groupSchedule);
        string name = "pasha";
        var student = new ExtraStudent(name);
        group.AddExtraStudent(student);
        isu.AssignAdditionalSubjectToStudent(student, additionalSubject);
        isu.DeleteStudentFromAdditionalSubject(student, additionalSubject);
        Assert.DoesNotContain(flow, student.GetFlows());
    }

    [Fact]
    public void GetSubjectFlows()
    {
        var isu = new IsuExtra();
        var lessonTime = new LessonTime(1, 1);
        var audience = new Audience(123);
        Lesson lesson = Lesson.Builder
            .WithLessonTime(lessonTime)
            .WithAudience(audience)
            .WithSubject("math")
            .WithTeacher("Vova")
            .Build();
        var lessons = new List<Lesson>();
        lessons.Add(lesson);
        var schedule = new Schedule(lessons);
        var flow = new Flow(schedule);
        var flows = new List<Flow>();
        flows.Add(flow);
        AdditionalSubject additionalSubject = isu.CreateAdditionalSubject("french", 'f', flows);
        Assert.Equal(flows, additionalSubject.GetFlows());
    }

    [Fact]
    public void GetStudentsFromSubject()
    {
        var isu = new IsuExtra();
        var lessonTime = new LessonTime(1, 1);
        var audience = new Audience(123);
        Lesson lesson = Lesson.Builder
            .WithLessonTime(lessonTime)
            .WithAudience(audience)
            .WithSubject("math")
            .WithTeacher("Vova")
            .Build();
        var lessons = new List<Lesson>();
        lessons.Add(lesson);
        var schedule = new Schedule(lessons);
        var flow = new Flow(schedule);
        var flows = new List<Flow>();
        flows.Add(flow);
        AdditionalSubject additionalSubject = isu.CreateAdditionalSubject("french", 'f', flows);
        var courseNumber = new CourseNumber('2');
        var groupName = new GroupName('M', '3', courseNumber, "90");
        var groupLessonTime = new LessonTime(2, 2);
        var groupAudience = new Audience(13);
        Lesson groupLesson = Lesson.Builder
            .WithLessonTime(groupLessonTime)
            .WithAudience(groupAudience)
            .WithSubject("chemistry")
            .WithTeacher("Masha")
            .Build();
        var groupLessons = new List<Lesson>();
        groupLessons.Add(groupLesson);
        var groupSchedule = new Schedule(groupLessons);
        var group = new ExtraGroup(groupName, groupSchedule);
        string name = "pasha";
        var student = new ExtraStudent(name);
        group.AddExtraStudent(student);
        isu.AssignAdditionalSubjectToStudent(student, additionalSubject);
        var students = new List<ExtraStudent>();
        students.Add(student);
        Assert.Equal(students, isu.GetStudentsFromSubject(additionalSubject));
    }

    [Fact]
    public void GetStudentsWithNoAdditionalSubject()
    {
        var isu = new IsuExtra();
        var lessonTime = new LessonTime(1, 1);
        var audience = new Audience(123);
        Lesson lesson = Lesson.Builder
            .WithLessonTime(lessonTime)
            .WithAudience(audience)
            .WithSubject("math")
            .WithTeacher("Vova")
            .Build();
        var lessons = new List<Lesson>();
        lessons.Add(lesson);
        var schedule = new Schedule(lessons);
        var flow = new Flow(schedule);
        var flows = new List<Flow>();
        flows.Add(flow);
        AdditionalSubject additionalSubject = isu.CreateAdditionalSubject("french", 'f', flows);
        var courseNumber = new CourseNumber('2');
        var groupName = new GroupName('M', '3', courseNumber, "90");
        var groupLessonTime = new LessonTime(2, 2);
        var groupAudience = new Audience(13);
        Lesson groupLesson = Lesson.Builder
            .WithLessonTime(groupLessonTime)
            .WithAudience(groupAudience)
            .WithSubject("chemistry")
            .WithTeacher("Masha")
            .Build();
        var groupLessons = new List<Lesson>();
        groupLessons.Add(groupLesson);
        var groupSchedule = new Schedule(groupLessons);
        var group = new ExtraGroup(groupName, groupSchedule);
        string name = "pasha";
        var student = new ExtraStudent(name);
        string mashaName = "masha";
        var masha = new ExtraStudent(mashaName);
        group.AddExtraStudent(student);
        group.AddExtraStudent(masha);
        isu.AssignAdditionalSubjectToStudent(masha, additionalSubject);
        var students = new List<ExtraStudent>();
        students.Add(student);
        Assert.Equal(students, isu.GetStudentsWithoutAnyAdditionalSubject(group));
    }
}