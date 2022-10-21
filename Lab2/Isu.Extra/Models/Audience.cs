namespace Isu.Extra.Models;
using Exceptions;

public class Audience
{
    private int _number;
    private List<LessonTime> _workHours;

    public Audience(int number)
    {
        _number = number;
        _workHours = new List<LessonTime>();
        for (int i = 1; i < 7; ++i)
        {
            for (int j = 1; j < 9; ++j)
            {
                var lessonTime = new LessonTime(i, j);
                _workHours.Add(lessonTime);
            }
        }
    }

    public int GetAudienceNumber()
    {
        int number = _number;
        return number;
    }

    public List<LessonTime> GetWorkHours()
    {
        var workHours = new List<LessonTime>(_workHours);
        return workHours;
    }

    public void SetWorkHours(List<LessonTime> workHours)
    {
        _workHours = workHours;
    }

    public void BanTime(LessonTime newLessonTime)
    {
        int day = newLessonTime.GetDay();
        int lessonNumber = newLessonTime.GetLessonNumber();
        if (day < 1 || day > 6 || lessonNumber < 1 || lessonNumber > 8)
        {
            throw new LessonTimeException("Invalid time");
        }

        foreach (LessonTime lessonTime in _workHours.Where(lessonTime => lessonTime.GetDay() == day && lessonTime.GetLessonNumber() == lessonNumber))
        {
            if (lessonTime.GetAvailability() == false)
            {
                throw new BanTimeException("This time is already banned");
            }

            lessonTime.SetAvailability(false);
        }
    }
}