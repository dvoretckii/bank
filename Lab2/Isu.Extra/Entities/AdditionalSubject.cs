namespace Isu.Extra.Entities;

public class AdditionalSubject
{
    private string _name;
    private List<Flow> _flows;
    private char _megafaculty;

    public AdditionalSubject()
    {
        _name = string.Empty;
        _flows = new List<Flow>();
        _megafaculty = '\0';
    }

    public AdditionalSubject(AdditionalSubject additionalSubject)
    {
        _name = additionalSubject.GetName();
        _flows = additionalSubject.GetFlows();
        _megafaculty = additionalSubject.GetMegafaculty();
    }

    public AdditionalSubject(string name, char megafaculty)
    {
        _name = name;
        _flows = new List<Flow>();
        _megafaculty = megafaculty;
    }

    public AdditionalSubject(string name, char megafaculty, List<Flow> flows)
    {
        _name = name;
        _flows = flows;
        _megafaculty = megafaculty;
        foreach (Flow flow in _flows)
        {
            flow.SetAdditionalSubject(this);
        }
    }

    public void SetFlows(List<Flow> flows)
    {
        _flows = flows;
    }

    public char GetMegafaculty()
    {
        char cp = _megafaculty;
        return cp;
    }

    public string GetName()
    {
        string cp = _name;
        return cp;
    }

    public List<Flow> GetFlows()
    {
        var cp = new List<Flow>(_flows);
        return cp;
    }
}