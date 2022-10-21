using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class Schedule
{
    private List<Lesson> _lessons;

    public Schedule()
    {
        _lessons = new List<Lesson>();
    }

    public Schedule(List<Lesson> lessons)
    {
        _lessons = new List<Lesson>(lessons);
    }

    public Schedule(Schedule schedule)
    {
        _lessons = schedule.GetLessons();
    }

    public List<Lesson> GetLessons()
    {
        var lessons = new List<Lesson>(_lessons);
        return lessons;
    }

    public bool CheckMergePossibility(Schedule schedule)
    {
        bool flg = true;
        foreach (Lesson lesson in schedule.GetLessons())
        {
            if (_lessons.Contains(lesson))
            {
                flg = false;
            }
        }

        return flg;
    }

    public Schedule? AddLesson(Lesson lesson)
    {
        if (_lessons.Contains(lesson))
        {
            return null;
        }

        _lessons.Add(lesson);
        return this;
    }

    public Schedule? RemoveLesson(Lesson lesson)
    {
        if (!_lessons.Contains(lesson))
        {
            return null;
        }

        _lessons.Remove(lesson);
        return this;
    }

    public Schedule? MergeSchedule(Schedule schedule)
    {
        if (!CheckMergePossibility(schedule))
        {
            return null;
        }

        foreach (Lesson lesson in schedule.GetLessons())
        {
            AddLesson(lesson);
        }

        return this;
    }

    public bool CheckComplementPossibility(Schedule schedule)
    {
        bool flg = true;
        foreach (Lesson lesson in schedule.GetLessons())
        {
            if (!_lessons.Contains(lesson))
            {
                flg = false;
            }
        }

        return flg;
    }

    public Schedule? ComplementSchedule(Schedule schedule)
    {
        if (!CheckComplementPossibility(schedule))
        {
            return null;
        }

        foreach (Lesson lesson in schedule.GetLessons())
        {
            RemoveLesson(lesson);
        }

        return this;
    }
}