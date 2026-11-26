namespace LAB;

public class Genre
{
    private int _id;
    private string _name;

    public Genre()
    {
        _id = 0;
        _name = "";
    }

    public Genre(int id, string name)
    {
        _id = id;
        _name = name;
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

    public override string ToString()
    {
        return $"Жанр: {_name} | ID: ({_id})";
    }
}