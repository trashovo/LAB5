namespace LAB;

public class Track
{
    private int _id;
    private string _name;
    private int _albumId;
    private int _genreId;
    private long _duration;
    private long _size;
    private decimal _price;

    public Track()
    {
        _id = 0;
        _name = "";
        _albumId = 0;
        _genreId = 0;
        _duration = 0;
        _size = 0;
        _price = 0;
    }

    public Track(int id, string name, int albumId, int genreId, long duration, long size, decimal price)
    {
        _id = id;
        _name = name;
        _albumId = albumId;
        _genreId = genreId;
        _duration = duration;
        _size = size;
        _price = price;
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

    public int AlbumId
    {
        get { return _albumId; }
        set
        {
            if (value >= 0)
            {
                _albumId = value;
            }
        }
    }

    public int GenreId
    {
        get { return _genreId; }
        set
        {
            if (value >= 0)
            {
                _genreId = value;
            }
        }
    }

    public long Duration
    {
        get { return _duration; }
        set
        {
            if (value >= 0)
            {
                _duration = value;
            }
        }
    }

    public long Size
    {
        get { return _size; }
        set
        {
            if (value >= 0)
            {
                _size = value;
            }
        }
    }

    public decimal Price
    {
        get { return _price; }
        set
        {
            if (value >= 0)
            {
                _price = value;
            }
        }
    }

    public override string ToString()
    {
        return $"Трек: {_name} | ID: ({_id}) | Альбом: ({_albumId}) | Жанр: ({_genreId}) " +
               $"Длительность: {_duration} мс | Размер: {_size} Б | Стоимость: {_price} руб.";
    }
}