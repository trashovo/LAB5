# Суханов Сергей Лаб-2(ИТ-5) Лабораторная №5 (Вариант 15)

# Постановка задачи
Разработать консольное приложение с дружественным интерфейсом с возможностью выбора заданий.

## Требования
Приложение должно выполнять следующие функции:
1. Чтение базы из Excel-файла.
2. Просмотр таблиц.
3. Удаление элементов по `ID`.
4. Добавление новых элементов.
5. Реализация 4 LINQ-запросов (1 таблица → перечень, 2 таблицы → значение, 3 таблицы → перечень, 3 таблицы → значение).
6. Сохранение изменений в Excel.
7. Обработка ошибок.

## Предметная область

База «Аудиотека» состоит из четырёх таблиц:

- «Артисты» (`ID`, `Имя`).
- «Альбомы» (`ID`, `Название`, `ID Исполнителя`).
- «Жанры» (`ID`, `Название`).
- «Треки» (`ID`, `Название трека`, `ID альбома`, `ID жанра`, `Длительность`, `Размер`, `Стоимость`).

# Описание классов

## Модели данных

Четыре класса: `Artist`, `Album`, `Genre`, `Track`. В каждом — закрытые поля, свойства с валидацией (непустые строки, неотрицательные числа), два конструктора (по умолчанию и с параметрами) и переопределённый `ToString()`.

## `ExcelStorage` (`ExcelWork.cs`)

Работа с файлом через библиотеку NPOI (`HSSFWorkbook` для `.xls`).

- `Load(path)` — открывает файл, читает четыре листа по именам, построчно заполняет списки `Artists`, `Albums`, `Genres`, `Tracks`. Текстовые ячейки берутся через `cell.ToString().Trim()`, числа — через `NumericCellValue` с приведением к нужному типу.
- `Save(path)` — создаёт новую книгу, четыре листа с заголовками, записывает данные из списков и сохраняет на диск.

## `AudioLinqQueries` (`LINQRequests.cs`)

Четыре статических метода с LINQ-запросами:

1. **`GetTracksLongerThan`** (1 таблица, перечень) — треки длиннее `N` мс, отсортированы по убыванию длительности.
2. **`CountTracksByGenre`** (2 таблицы, значение) — количество треков в жанре (соединение `Tracks` + `Genres`).
3. **`GetTopArtistsByTrackCount`** (3 таблицы, перечень) — топ-N исполнителей по количеству треков (соединение `Artists` + `Albums` + `Tracks`, группировка по имени артиста).
4. **`GetMinArtistTotalPriceInGenre`** (3 таблицы, значение) — минимальная суммарная стоимость исполнителя в заданном жанре (соединение `Tracks` + `Genres` + `Albums`, группировка по `ArtistId`, минимум сумм).

## `Checked` (`Checked.cs`)

Вспомогательный класс для ввода: `ReadInt`, `ReadLong`, `ReadDecimal`, `NewReadInt` (только положительные), `ReadCount` (число в диапазоне), `ReadString` (непустая строка). Все методы повторяют запрос при ошибке, поэтому программа не падает от неверного ввода.

## `Program` (`Program.cs`)

Консольное меню в цикле с обработкой ошибок и пользовательским вводом.
**Обработка ошибок:** `try/catch` вокруг `Load` (при ошибке — сообщение и выход) и вокруг `switch` внутри меню (при ошибке — сообщение и возврат в меню без потери данных).

# Тестирование

## Загрузка файла
<img width="282" height="42" alt="image" src="https://github.com/user-attachments/assets/9c683d1b-edf2-4212-8e36-5788f875a81b" />
<img width="567" height="82" alt="image" src="https://github.com/user-attachments/assets/7cb065ec-c96c-4b75-a0b9-368f2d3335b9" />


## Просмотр базы данных

<img width="423" height="648" alt="image" src="https://github.com/user-attachments/assets/91cbd01c-e9c0-4a6d-ae24-551ab4cf477c" />
<img width="519" height="226" alt="image" src="https://github.com/user-attachments/assets/9aa1651d-4865-4b76-9754-30139b16ad61" />
<img width="557" height="187" alt="image" src="https://github.com/user-attachments/assets/3d2e9a1e-7183-48e4-8c74-30a45ed16222" />



## Удаление записи

<img width="517" height="161" alt="image" src="https://github.com/user-attachments/assets/e13cdaa0-75ff-4eb5-8d79-5848c2d269ec" />
<img width="481" height="169" alt="image" src="https://github.com/user-attachments/assets/b02e4c77-a494-4cec-b340-b34923f3b46d" />
<img width="548" height="159" alt="image" src="https://github.com/user-attachments/assets/d968757a-5e4e-48b1-a3ce-397bf3ca006a" />
<img width="423" height="105" alt="image" src="https://github.com/user-attachments/assets/db4525b0-108a-41d7-a975-681be2c01478" />

## Добавление записи


<img width="439" height="142" alt="image" src="https://github.com/user-attachments/assets/0779aa43-7da9-4d3e-9ee6-349462632ddc" />
<img width="492" height="206" alt="image" src="https://github.com/user-attachments/assets/7f373e1b-0e54-4cfa-b37d-223f6a17e9eb" />
<img width="417" height="263" alt="image" src="https://github.com/user-attachments/assets/ae5eb1f7-0c0e-495e-96e1-ef681c7622b1" />


## Выполнение LINQ-запросов

### Запрос 1


<img width="317" height="78" alt="image" src="https://github.com/user-attachments/assets/ab59c6d1-a6e8-4ec3-be5c-dd3bb30773c5" />

2. **Запрос 2** — количество треков в жанре.
3. **Запрос 3** — топ-N исполнителей по числу треков.
4. **Запрос 4** — минимальная суммарная стоимость исполнителя в Blues (по примеру из задания — 977).

## Сохранение в Excel

После изменений выполняется сохранение; при повторной загрузке данные актуальны.
