using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class LessonTime
{
    private int _day;
    private int _lessonNumber;
    private bool _availability;

    public LessonTime(int day, int lessonNumber)
    {
        if (day < 1 || day > 6 || lessonNumber < 1 || lessonNumber > 8)
        {
            throw new LessonTimeException("Invalid time");
        }

        _day = day;
        _lessonNumber = lessonNumber;
        _availability = true;
    }

    public int GetDay()
    {
        int day = _day;
        return day;
    }

    public int GetLessonNumber()
    {
        int lessonNumber = _lessonNumber;
        return lessonNumber;
    }

    public bool GetAvailability()
    {
        bool availability = _availability;
        return availability;
    }

    public void SetAvailability(bool availability)
    {
        _availability = availability;
    }
}