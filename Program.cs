using System;

namespace LAB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Checked check = new Checked();
            
            ExcelStorage storage = new ExcelStorage();
            string Filename = "C:\\Users\\af\\Documents\\C#\\LAB\\LAB\\LR5-var15.xls";

            try
            {
                storage.Load(Filename);
                Console.WriteLine("\nБаза данных успешно загружена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n{ex.Message}");
                return;
            }
            Console.ReadKey();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Загружено: Артистов — {storage.Artists.Count}, Альбомов — {storage.Albums.Count}, Жанров — {storage.Genres.Count}, Треков — {storage.Tracks.Count}");
                Console.WriteLine("1. Просмотр таблиц базы данных");
                Console.WriteLine("2. Удаление элемента по ID");
                Console.WriteLine("3. Добавление нового элемента");
                Console.WriteLine("4. Выполнение LINQ-запросов");
                Console.WriteLine("5. Сохранение изменений в Excel-файл");
                Console.WriteLine("0. Выход");

                int choice = check.ReadCount("Введите номер задания (от 0 до 5): ", 0, 5);
                if (choice == 0)
                {
                    Console.WriteLine("\nЗавершение работы программы.");
                    break;
                }

                Console.WriteLine();
                
                try
                {
                    switch (choice)
                    {
                        case 1:
                        {
                            Console.WriteLine("\n1 — Артисты | 2 — Альбомы | 3 — Жанры | 4 — Треки");
                            int table = check.ReadCount("Введите номер таблицы: ", 1, 4);
                            int maxCount = table switch
                            {
                                1 => storage.Artists.Count,
                                2 => storage.Albums.Count,
                                3 => storage.Genres.Count,
                                4 => storage.Tracks.Count
                            };
                            int count = check.ReadCount($"Сколько первых записей показать (максимум {maxCount}): ", 1, maxCount);


                            switch (table)
                            {
                                case 1:
                                    foreach (Artist item in storage.Artists.Take(count))
                                        Console.WriteLine(item);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    foreach (Album item in storage.Albums.Take(count))
                                        Console.WriteLine(item);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    foreach (Genre item in storage.Genres.Take(count))
                                        Console.WriteLine(item);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    foreach (Track item in storage.Tracks.Take(count))
                                        Console.WriteLine(item);
                                    Console.ReadKey();
                                    break;
                            }

                            break;
                        }
                        case 2:
                        {
                            Console.WriteLine("\n1 — Артист | 2 — Альбом | 3 — Жанр | 4 — Трек");
                            int table = check.ReadCount("Введите номер таблицы: ", 1, 4);
                            (int minId, int maxId) = table switch
                            {
                                1 => (storage.Artists.Min(a => a.Id), storage.Artists.Max(a => a.Id)),
                                2 => (storage.Albums.Min(a => a.Id), storage.Albums.Max(a => a.Id)),
                                3 => (storage.Genres.Min(g => g.Id), storage.Genres.Max(g => g.Id)),
                                4 => (storage.Tracks.Min(t => t.Id), storage.Tracks.Max(t => t.Id)),
                            };
                            int id = check.ReadCount($"Введите ID ({minId}-{maxId}): ", minId, maxId);

                            bool removed = false;

                            switch (table)
                            {
                                case 1:
                                    if (storage.Albums.Any(a => a.ArtistId == id))
                                        throw new InvalidOperationException(
                                            "Нельзя удалить артиста, у которого есть альбомы");
                                    var artist = storage.Artists.FirstOrDefault(a => a.Id == id);
                                    if (artist != null) removed = storage.Artists.Remove(artist);
                                    break;

                                case 2:
                                    if (storage.Tracks.Any(t => t.AlbumId == id))
                                        throw new InvalidOperationException(
                                            "Нельзя удалить альбом, в котором есть треки");
                                    var album = storage.Albums.FirstOrDefault(a => a.Id == id);
                                    if (album != null) removed = storage.Albums.Remove(album);
                                    break;

                                case 3:
                                    if (storage.Tracks.Any(t => t.GenreId == id))
                                        throw new InvalidOperationException(
                                            "Нельзя удалить жанр, который используется в треках");
                                    var genre = storage.Genres.FirstOrDefault(g => g.Id == id);
                                    if (genre != null) removed = storage.Genres.Remove(genre);
                                    break;

                                case 4:
                                    var track = storage.Tracks.FirstOrDefault(t => t.Id == id);
                                    if (track != null) removed = storage.Tracks.Remove(track);
                                    break;
                            }

                            Console.WriteLine(removed ? "Запись удалена" : $"Запись с ID {id} не найдена");
                            Console.ReadKey();
                            break;
                        }
                        case 3:
                        {
                            Console.WriteLine("\n1 — Артист | 2 — Альбом | 3 — Жанр | 4 — Трек");
                            int table = check.ReadCount("В какую таблицу добавить: ", 1, 4);

                            switch (table)
                            {
                                case 1:
                                    int MaxIdArtist = storage.Artists.Max(a => a.Id);
                                    int arId = check.NewReadInt($"ID артиста(последний записанный {MaxIdArtist}): ");
                                    if (storage.Artists.Any(a => a.Id == arId))
                                        throw new ArgumentException($"Артист с ID {arId} уже существует");
                                    string arName = check.ReadString("Имя исполнителя: ");
                                    storage.Artists.Add(new Artist(arName, arId));
                                    break;

                                case 2:
                                    int MaxIdAlbums = storage.Albums.Max(a => a.Id);
                                    int alId = check.NewReadInt($"ID альбома(последний записанный {MaxIdAlbums}): ");
                                    if (storage.Albums.Any(a => a.Id == alId))
                                        throw new ArgumentException($"Альбом с ID {alId} уже существует");
                                    string title = check.ReadString("Название альбома: ");
                                    int artistId = check.NewReadInt("ID исполнителя: ");
                                    if (!storage.Artists.Any(a => a.Id == artistId))
                                        throw new ArgumentException($"Артист с ID {artistId} не найден");
                                    storage.Albums.Add(new Album(alId, title, artistId));
                                    break;

                                case 3:
                                    int MaxIdGenres =  storage.Genres.Max(a => a.Id);
                                    int gId = check.NewReadInt($"ID жанра(последний записанный {MaxIdGenres}): ");
                                    if (storage.Genres.Any(g => g.Id == gId))
                                        throw new ArgumentException($"Жанр с ID {gId} уже существует");
                                    string gName = check.ReadString("Название жанра: ");
                                    storage.Genres.Add(new Genre(gId, gName));
                                    break;

                                case 4:
                                    int MaxIDTracks = storage.Tracks.Max(t => t.Id);
                                    int MaxIDAlbums = storage.Albums.Max(a => a.Id);
                                    int MinIDAlbums = storage.Albums.Min(a => a.Id);
                                    int MaxIDGenres = storage.Genres.Max(g => g.Id);
                                    int MinIDGenres = storage.Genres.Min(g => g.Id);
                                    int trId = check.NewReadInt($"ID трека(последний записанный {MaxIDTracks}): ");
                                    if (storage.Tracks.Any(t => t.Id == trId))
                                        throw new ArgumentException($"Трек с ID {trId} уже существует");

                                    string trName = check.ReadString("Название трека: ");

                                    int albumId = check.NewReadInt($"ID альбома({MinIDAlbums}-{MaxIDAlbums}): ");
                                    if (!storage.Albums.Any(a => a.Id == albumId))
                                        throw new ArgumentException($"Альбом с ID {albumId} не найден.");

                                    int genreId = check.NewReadInt($"ID жанра({MinIDGenres}-{MaxIDGenres}): ");
                                    if (!storage.Genres.Any(g => g.Id == genreId))
                                        throw new ArgumentException($"Жанр с ID {genreId} не найден");

                                    long duration = check.ReadLong("Длительность (мс): ");

                                    long size = check.ReadLong("Размер (байт): ");

                                    decimal price = check.ReadDecimal("Стоимость (руб.): ");

                                    storage.Tracks.Add(new Track(trId, trName, albumId, genreId, duration, size,
                                        price));
                                    break;
                            }
                            Console.WriteLine("Запись добавлена.");
                            Console.ReadKey();
                            break;
                        }
                        case 4:
                        {
                            Console.WriteLine("\n1. (1 таблица(перечень)) Треки длиннее N мс");
                            Console.WriteLine("2. (2 таблицы(значение)) Количество треков в жанре");
                            Console.WriteLine("3. (3 таблицы(перечень)) Топ N исполнителей по кол-ву треков");
                            Console.WriteLine("4. (3 таблицы(значение)) min суммарная стоимость артиста в жанре");

                            int q = check.ReadCount("Выберите запрос: ", 1, 4);
                            Console.WriteLine();

                            switch (q)
                            {
                                case 1:
                                {
                                    long minMs = check.ReadLong("Минимальная длительность (мс): ");
                                    var longTracks = AudioLinqQueries.GetTracksLongerThan(storage.Tracks, minMs);
                                    Console.WriteLine($"Найдено: {longTracks.Count}:");
                                    foreach (Track t in longTracks) Console.WriteLine(t);
                                    Console.ReadKey();
                                    break;
                                }

                                case 2:
                                {
                                    string genre = check.ReadString("Название жанра: ");
                                    int count = AudioLinqQueries.CountTracksByGenre(storage.Tracks, storage.Genres,
                                        genre);
                                    Console.WriteLine($"Треков в жанре \"{genre}\": {count}");
                                    Console.ReadKey();
                                    break;
                                }

                                case 3:
                                {
                                    int top = check.ReadCount("Сколько исполнителей показать: ", 1, storage.Artists.Count);
                                    var topArtists = AudioLinqQueries.GetTopArtistsByTrackCount(
                                        storage.Artists, storage.Albums, storage.Tracks, top);
                                    Console.WriteLine($"Топ-{top} исполнителей по количеству треков:");
                                    foreach (string item in topArtists)
                                        Console.WriteLine(item);
                                    Console.ReadKey();
                                    break;
                                }

                                case 4:
                                {
                                    string genre = check.ReadString("Название жанра: ");
                                    decimal minPrice = AudioLinqQueries.GetMinArtistTotalPriceInGenre(
                                        storage.Tracks, storage.Albums, storage.Genres, genre);
                                    Console.WriteLine($"Минимальная суммарная стоимость в жанре {genre}: {minPrice}");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            break;
                        }
                        case 5:
                        {
                            Console.Write($"Имя файла для сохранения (Enter — '{Filename}'): ");
                            string savePath = Console.ReadLine()?.Trim() ?? "";
                            if (string.IsNullOrWhiteSpace(savePath))
                            {
                                storage.Save(Filename);
                                Console.WriteLine($"\nИзменения сохранены");
                            }
                            else
                            {
                                storage.Save($"C:\\Users\\af\\Documents\\C#\\LAB\\LAB\\{savePath}.xls");
                                Console.WriteLine($"\nИзменения сохранены в '{savePath}'");
                            } 
                            
                            Console.ReadKey();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка: {ex.Message}");
                    Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
                    Console.ReadKey();
                }
            }
        }
    }
}