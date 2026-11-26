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
<br>
<img width="567" height="82" alt="image" src="https://github.com/user-attachments/assets/7cb065ec-c96c-4b75-a0b9-368f2d3335b9" />


## Просмотр базы данных

<img width="423" height="648" alt="image" src="https://github.com/user-attachments/assets/91cbd01c-e9c0-4a6d-ae24-551ab4cf477c" />
<br>
<img width="519" height="226" alt="image" src="https://github.com/user-attachments/assets/9aa1651d-4865-4b76-9754-30139b16ad61" />
<br>
<img width="557" height="187" alt="image" src="https://github.com/user-attachments/assets/3d2e9a1e-7183-48e4-8c74-30a45ed16222" />



## Удаление записи

<img width="517" height="161" alt="image" src="https://github.com/user-attachments/assets/e13cdaa0-75ff-4eb5-8d79-5848c2d269ec" />
<br>
<img width="481" height="169" alt="image" src="https://github.com/user-attachments/assets/b02e4c77-a494-4cec-b340-b34923f3b46d" />
<br>
<img width="548" height="159" alt="image" src="https://github.com/user-attachments/assets/d968757a-5e4e-48b1-a3ce-397bf3ca006a" />
<br>
<img width="423" height="105" alt="image" src="https://github.com/user-attachments/assets/db4525b0-108a-41d7-a975-681be2c01478" />

## Добавление записи


<img width="439" height="142" alt="image" src="https://github.com/user-attachments/assets/0779aa43-7da9-4d3e-9ee6-349462632ddc" />
<br>
<img width="492" height="206" alt="image" src="https://github.com/user-attachments/assets/7f373e1b-0e54-4cfa-b37d-223f6a17e9eb" />
<br>
<img width="417" height="263" alt="image" src="https://github.com/user-attachments/assets/ae5eb1f7-0c0e-495e-96e1-ef681c7622b1" />


## Выполнение LINQ-запросов

### Запрос 1
<img width="1994" height="202" alt="image" src="https://github.com/user-attachments/assets/8addda8a-a61b-4b41-b875-b7d994814d7a" />
<br>
<img width="592" height="87" alt="image" src="https://github.com/user-attachments/assets/5ba7d79e-f6f7-4c12-b2fa-6fda089f4e25" />
<br>
<img width="317" height="78" alt="image" src="https://github.com/user-attachments/assets/ab59c6d1-a6e8-4ec3-be5c-dd3bb30773c5" />

### Запрос 2
<img width="437" height="91" alt="image" src="https://github.com/user-attachments/assets/ca3c85b4-f3bb-4ebc-b3d8-1cd63a8cc1cd" />
<br>
<img width="340" height="85" alt="image" src="https://github.com/user-attachments/assets/ee57b629-8dbb-4513-8dd2-85f4926d5e19" />

### Запрос 3
<img width="560" height="251" alt="image" src="https://github.com/user-attachments/assets/7fdc50fd-3481-4ca2-8a7d-f5e0a9730384" />
<br>
<img width="503" height="189" alt="image" src="https://github.com/user-attachments/assets/c83cbdf2-c750-417a-9227-219581fb1e40" />

### Запрос 4
<img width="694" height="84" alt="image" src="https://github.com/user-attachments/assets/9be63867-ae30-4766-a7df-c4650a2e9480" />
<br>
<img width="669" height="141" alt="image" src="https://github.com/user-attachments/assets/7074bcbc-1def-47b2-9404-54a8767dd201" />


## Сохранение в Excel
<img width="1013" height="50" alt="image" src="https://github.com/user-attachments/assets/cd0e46d1-a5df-4afb-96c2-4f72ac9fd949" />
<br>
<img width="1202" height="116" alt="image" src="https://github.com/user-attachments/assets/c67c76ab-1c49-44d2-8b4d-9c89bcabedc6" />

