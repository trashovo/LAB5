namespace LAB;

public class Album
{
    private int _id;
    private string _title;
    private int _artistId;

    public Album()
    {
        _id = 0;
        _title = "";
        _artistId = 0;
    }

    public Album(int id, string title, int artistId)
    {
        _id = id;
        _title = title;
        _artistId = artistId;
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

    public string Title
    {
        get { return _title; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _title = value;
            }
        }
    }

    public int ArtistId
    {
        get { return _artistId; }
        set
        {
            if (value >= 0)
            {
                _artistId = value;
            }
        }
    }

    public override string ToString()
    {
        return $"Альбом: {_title} | ID: ({_id}) | ID исполнителя: ({_artistId})";
    }
}