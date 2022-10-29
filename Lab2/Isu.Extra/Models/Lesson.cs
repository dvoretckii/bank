namespace Isu.Extra.Models;
using Isu.Entities;
using Isu.Models;

public interface ILessonTimeBuilder
{
    IAudienceBuilder WithLessonTime(LessonTime lessonTime);
}

public interface IAudienceBuilder
{
    ISubjectBuilder WithAudience(Audience audience);
}

public interface ISubjectBuilder
{
    ITeacherBuilder WithSubject(string subject);
}

public interface ITeacherBuilder
{
    ILessonBuilder WithTeacher(string teacher);
}

public interface ILessonBuilder
{
    Lesson Build();
}

public class Lesson
{
    private LessonTime _lessonTime = null!;
    private Audience _audience = null!;
    private string _subject = null!;
    private string _teacher = null!;

    public static ILessonTimeBuilder Builder => new LessonBuilder();

    public LessonTime GetLessonTime()
    {
        LessonTime lessonTime = _lessonTime;
        return _lessonTime;
    }

    public Audience GetAudience()
    {
        Audience audience = _audience;
        return audience;
    }

    public string GetTeacher()
    {
        string teacher = _teacher;
        return teacher;
    }

    public string GetSubject()
    {
        string subject = _subject;
        return _subject;
    }

    private class LessonBuilder : ILessonTimeBuilder, IAudienceBuilder, ISubjectBuilder, ITeacherBuilder, ILessonBuilder
    {
        private Lesson _lesson = new Lesson();
        public IAudienceBuilder WithLessonTime(LessonTime lessonTime)
        {
            _lesson._lessonTime = lessonTime;
            return this;
        }

        public ISubjectBuilder WithAudience(Audience audience)
        {
            audience.BanTime(_lesson._lessonTime);
            _lesson._audience = audience;
            return this;
        }

        public ITeacherBuilder WithSubject(string subject)
        {
            _lesson._subject = subject;
            return this;
        }

        public ILessonBuilder WithTeacher(string teacher)
        {
            _lesson._teacher = teacher;
            return this;
        }

        public Lesson Build()
        {
            return _lesson;
        }
    }
}