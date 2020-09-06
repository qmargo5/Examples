-- Первый шаг: созадание таблицы, если её нет и очитска, если она есть
-- эта таблица для выгрузки контрагентов из Excel, создается в текущей БД однократно если её еще нет.
IF NOT EXISTS (SELECT [name] FROM sys.tables where[name] = 'Контрагенты')
CREATE TABLE [dbo].[Контрагенты](
	[GUID] [varchar](80) NULL,
	[Сокращенное наименование] [varchar](512) NULL,
	[Полное наименование] [varchar](512) NULL,
	[Юридический адрес] [varchar](512) NULL,
	[ИНН] [varchar](512) NULL,
	[КПП] [varchar](512) NULL,
	[Код ОКПО] [varchar](512) NULL,
	[ОГРН] [varchar](512) NULL,
	[Фамилия] [varchar](512) NULL,
	[Имя] [varchar](512) NULL,
	[Отчество] [varchar](512) NULL,
	[Головная организация] [varchar](512) NULL,
	[IsAdded] [bit] NOT NULL DEFAULT 0
) ON [PRIMARY]

-- перед выполнением импорта данных из Excel удаляем предыдущие записи
TRUNCATE TABLE [dbo].[Контрагенты] 

--второй шаг: запуск импортера и добавление записей из файла в промежуточную таблицу

--третий шаг: в переменную грязно читаем GUID из Контрагенты и join с промежуточной таблицой, чтобы узнать, каких записей еще нет в системе

--ID справочника ГТТ.Контрагенты
DECLARE @ID int = (SELECT Vid FROM dbo.MBVidAn WHERE Kod = 'Organization') 

-- в переменную запишем все GUID уже добавленных в систему записей
DECLARE @GUID TABLE ([GUID] VARCHAR(80)) 
INSERT INTO @GUID SELECT [String] FROM [dbo].[MBAnalit] AS MBA WITH(NOLOCK) WHERE MBA.[Vid] = @ID

--SELECT * FROM @Guid 

-- найти уже добавленные записи
UPDATE [dbo].[Контрагенты]
set [IsAdded] = 1
	where [GUID] in (select [GUID] from @GUID)