using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace LAB;

public class ExcelStorage
{
    public List<Artist> Artists = new List<Artist>();
    public List<Album> Albums = new List<Album>();
    public List<Genre> Genres = new List<Genre>();
    public List<Track> Tracks = new List<Track>();

    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Файл не найден: {filePath}");

        Artists.Clear();
        Albums.Clear();
        Genres.Clear();
        Tracks.Clear();

        using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        IWorkbook workbook = new HSSFWorkbook(fs);


        ISheet artistSheet = workbook.GetSheet("Артисты"); 
        for (int r = 1; r <= artistSheet.LastRowNum; r++)
        {
            IRow row = artistSheet.GetRow(r);
            if (row == null || row.GetCell(0) == null) continue;

            int id = (int)row.GetCell(0).NumericCellValue;
            string name = row.GetCell(1)?.ToString()?.Trim() ?? "";
            Artists.Add(new Artist(name, id));
        }


        ISheet albumSheet = workbook.GetSheet("Альбомы");
        for (int r = 1; r <= albumSheet.LastRowNum; r++)
        {
            IRow row = albumSheet.GetRow(r);
            if (row == null || row.GetCell(0) == null) continue;

            int id = (int)row.GetCell(0).NumericCellValue;
            string title = row.GetCell(1)?.ToString()?.Trim() ?? "";
            int artistId = (int)row.GetCell(2).NumericCellValue;
            Albums.Add(new Album(id, title, artistId));
        }


        ISheet genreSheet = workbook.GetSheet("Жанры");
        for (int r = 1; r <= genreSheet.LastRowNum; r++)
        {
            IRow row = genreSheet.GetRow(r);
            if (row == null || row.GetCell(0) == null) continue;

            int id = (int)row.GetCell(0).NumericCellValue;
            string name = row.GetCell(1)?.ToString()?.Trim() ?? "";
            Genres.Add(new Genre(id, name));
        }


        ISheet trackSheet = workbook.GetSheet("Треки");
        for (int r = 1; r <= trackSheet.LastRowNum; r++)
        {
            IRow row = trackSheet.GetRow(r);
            if (row == null || row.GetCell(0) == null) continue;

            int id = (int)row.GetCell(0).NumericCellValue;
            string name = row.GetCell(1)?.ToString()?.Trim() ?? "";
            int albumId = (int)row.GetCell(2).NumericCellValue;
            int genreId = (int)row.GetCell(3).NumericCellValue;
            long duration = (long)row.GetCell(4).NumericCellValue;
            long size = (long)row.GetCell(5).NumericCellValue;
            decimal price = (decimal)row.GetCell(6).NumericCellValue;

            Tracks.Add(new Track(id, name, albumId, genreId, duration, size, price));
        }
    }

    
    public void Save(string filePath)
    {
        IWorkbook workbook = new HSSFWorkbook();

        ISheet albumSheet = workbook.CreateSheet("Альбомы");
        IRow alHeader = albumSheet.CreateRow(0);
        alHeader.CreateCell(0).SetCellValue("ID");
        alHeader.CreateCell(1).SetCellValue("Название");
        alHeader.CreateCell(2).SetCellValue("ID Исполнителя");
        for (int i = 0; i < Albums.Count; i++)
        {
            IRow row = albumSheet.CreateRow(i + 1);
            row.CreateCell(0).SetCellValue(Albums[i].Id);
            row.CreateCell(1).SetCellValue(Albums[i].Title);
            row.CreateCell(2).SetCellValue(Albums[i].ArtistId);
        }

        ISheet artistSheet = workbook.CreateSheet("Артисты");
        IRow arHeader = artistSheet.CreateRow(0);
        arHeader.CreateCell(0).SetCellValue("ID");
        arHeader.CreateCell(1).SetCellValue("Имя");
        for (int i = 0; i < Artists.Count; i++)
        {
            IRow row = artistSheet.CreateRow(i + 1);
            row.CreateCell(0).SetCellValue(Artists[i].Id);
            row.CreateCell(1).SetCellValue(Artists[i].Name);
        }

        ISheet trackSheet = workbook.CreateSheet("Треки");
        IRow trHeader = trackSheet.CreateRow(0);
        trHeader.CreateCell(0).SetCellValue("ID");
        trHeader.CreateCell(1).SetCellValue("Название трека");
        trHeader.CreateCell(2).SetCellValue("ID альбома");
        trHeader.CreateCell(3).SetCellValue("ID жанра");
        trHeader.CreateCell(4).SetCellValue("Длительность");
        trHeader.CreateCell(5).SetCellValue("Размер");
        trHeader.CreateCell(6).SetCellValue("Стоимость");
        for (int i = 0; i < Tracks.Count; i++)
        {
            IRow row = trackSheet.CreateRow(i + 1);
            row.CreateCell(0).SetCellValue(Tracks[i].Id);
            row.CreateCell(1).SetCellValue(Tracks[i].Name);
            row.CreateCell(2).SetCellValue(Tracks[i].AlbumId);
            row.CreateCell(3).SetCellValue(Tracks[i].GenreId);
            row.CreateCell(4).SetCellValue(Tracks[i].Duration);
            row.CreateCell(5).SetCellValue(Tracks[i].Size);
            row.CreateCell(6).SetCellValue((double)Tracks[i].Price);
        }

        ISheet genreSheet = workbook.CreateSheet("Жанры");
        IRow gnHeader = genreSheet.CreateRow(0);
        gnHeader.CreateCell(0).SetCellValue("ID");
        gnHeader.CreateCell(1).SetCellValue("Название");
        for (int i = 0; i < Genres.Count; i++)
        {
            IRow row = genreSheet.CreateRow(i + 1);
            row.CreateCell(0).SetCellValue(Genres[i].Id);
            row.CreateCell(1).SetCellValue(Genres[i].Name);
        }

        using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        workbook.Write(fs);
    }
}