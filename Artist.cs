namespace LAB;

public class Artist
{
    private string _name;
    private int _id;

    public Artist()
    {
        _name = "";
        _id = 0;
    }
    
    public Artist(string name, int id)
    {
        _name = name;
        _id = id;
    }

    public string Name
    {
        get { return _name; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _name = value;
            }
        }
    }

    public int Id
    {
        get { return _id; }
        set
        {
            if (value >= 0)
            {
                _id = value;
            }
        }
    }
    public override string ToString()
    {
        return $"Исполнитель: {_name} ID: ({_id})";
    }
}