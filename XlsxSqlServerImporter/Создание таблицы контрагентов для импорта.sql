-- ������ ���: ��������� �������, ���� � ��� � �������, ���� ��� ����
-- ��� ������� ��� �������� ������������ �� Excel, ��������� � ������� �� ���������� ���� � ��� ���.
IF NOT EXISTS (SELECT [name] FROM sys.tables where[name] = '�����������')
CREATE TABLE [dbo].[�����������](
	[GUID] [varchar](80) NULL,
	[����������� ������������] [varchar](512) NULL,
	[������ ������������] [varchar](512) NULL,
	[����������� �����] [varchar](512) NULL,
	[���] [varchar](512) NULL,
	[���] [varchar](512) NULL,
	[��� ����] [varchar](512) NULL,
	[����] [varchar](512) NULL,
	[�������] [varchar](512) NULL,
	[���] [varchar](512) NULL,
	[��������] [varchar](512) NULL,
	[�������� �����������] [varchar](512) NULL,
	[IsAdded] [bit] NOT NULL DEFAULT 0
) ON [PRIMARY]

-- ����� ����������� ������� ������ �� Excel ������� ���������� ������
TRUNCATE TABLE [dbo].[�����������] 

--������ ���: ������ ��������� � ���������� ������� �� ����� � ������������� �������

--������ ���: � ���������� ������ ������ GUID �� ����������� � join � ������������� ��������, ����� ������, ����� ������� ��� ��� � �������

--ID ����������� ���.�����������
DECLARE @ID int = (SELECT Vid FROM dbo.MBVidAn WHERE Kod = 'Organization') 

-- � ���������� ������� ��� GUID ��� ����������� � ������� �������
DECLARE @GUID TABLE ([GUID] VARCHAR(80)) 
INSERT INTO @GUID SELECT [String] FROM [dbo].[MBAnalit] AS MBA WITH(NOLOCK) WHERE MBA.[Vid] = @ID

--SELECT * FROM @Guid 

-- ����� ��� ����������� ������
UPDATE [dbo].[�����������]
set [IsAdded] = 1
	where [GUID] in (select [GUID] from @GUID)