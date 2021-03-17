IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointments]') AND type in (N'U'))
    DROP TABLE [dbo].[Appointments]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sessions]') AND type in (N'U'))
    DROP TABLE [dbo].[Sessions]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND type in (N'U'))
    DROP TABLE [dbo].[Customers]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND type in (N'U'))
    DROP TABLE [dbo].[Services]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
    DROP TABLE [dbo].[Users]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Organizations]') AND type in (N'U'))
    DROP TABLE [dbo].[Organizations]

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employees]') AND type in (N'U'))
    DROP TABLE [dbo].[Employees]

GO

CREATE TABLE Users (
	[Id]                [NVARCHAR](300)     NOT NULL,
	[Password]          [NVARCHAR](1000)    NULL,

    CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED ([Id] ASC)
)

INSERT INTO Users (Id, [Password]) VALUES ('myUser', 'myPass')

CREATE TABLE Organizations (
	[Id]                [NVARCHAR](300)     NOT NULL,
	[UserId]            [NVARCHAR](300)     NOT NULL,

    CONSTRAINT [PK_ORGANIZATIONS] PRIMARY KEY CLUSTERED ([Id] ASC)
)


INSERT INTO Organizations (Id, [UserId]) VALUES ('myOrganization', 'myUser')

CREATE TABLE Employees (
	[Id]                [INT]               NOT NULL        IDENTITY (1, 1),
	[Name]              [NVARCHAR](150)     NULL,
	[Title]             [NVARCHAR](150)     NULL,
	[Image]             [VARBINARY](MAX)    NULL,
    [OrganizationId]    [NVARCHAR](150)     NOT NULL,
    [Pin]               [NVARCHAR](150)     NOT NULL,
    [Color]             [NVARCHAR](50)      NOT NULL,

    CONSTRAINT [PK_EMPLOYEES] PRIMARY KEY CLUSTERED ([Id] ASC)
)

INSERT INTO Employees (Name, [Title], [Image], [OrganizationId], [Pin], [Color])
VALUES (
    'Ricardo Nunes',
    'Engineer',
    NULL,
    'myOrganization',
    '1234',
    '0xFF2196F3'
)

create table Services
(
    [Id]                [INT]               NOT NULL        IDENTITY (1, 1),
    [Name]              [NVARCHAR](150)     NOT NULL,
    [Value]             [FLOAT]             NOT NULL,
    [IsCoTherapy]       [BIT]               NULL,
    [Inactive]          [BIT]               NULL,
    [Image]             [VARBINARY](MAX)    NULL,
    [Intensive]         [BIT]               NULL,

    constraint PK_SERVICES                  PRIMARY KEY (Id),
    unique (Name)
)

INSERT INTO Services (Name, Value)
VALUES ('Development', 30.0)

create table Customers
(
    [Id]                [INT]               NOT NULL        IDENTITY (1, 1),
    [Name]              [NVARCHAR](150)     NOT NULL,
    [Inactive]          [BIT]               NULL,
    [Image]             [VARBINARY](MAX)    NULL,

    constraint PK_CUSTOMERS                 PRIMARY KEY (Id),
    unique (Name)
)

INSERT INTO Customers (Name)
VALUES ('Diogo Quintas')

create table Sessions
(
    [Id]                [INT]               NOT NULL        IDENTITY (1, 1),
    [StartedAt]         [DATE]              NOT NULL,
    [CustomerId]        [INT]               NOT NULL,
    [ServiceId]         [INT]               NOT NULL,
    [EmployeeId]        [INT]               NOT NULL,
    [Hours]             [FLOAT]             NOT NULL,
    [Discount]          [FLOAT]             NULL,
    [Value]             [FLOAT]             NULL,
    [InvoiceId]         [INT]               NULL,

    constraint PK_SessionS              PRIMARY KEY (Id),
    constraint Sessions_fk0             FOREIGN KEY (CustomerId) references Customers,
    constraint Sessions_fk1             FOREIGN KEY (ServiceId) references Services,
    constraint Sessions_fk2             FOREIGN KEY (EmployeeId) references [Employees]
)

create table Appointments
(
    [Id]                    [INT]               NOT NULL        IDENTITY (1, 1),
    [From]                  [DATE]              NOT NULL,
    [To]                    [DATE]              NOT NULL,
    [ServiceId]             [INT]               NOT NULL,
    [EmployeeId]            [INT]               NOT NULL,
    [CustomerId]            [INT]               NOT NULL,
    [RecurrenceType]        [NVARCHAR](50)      NULL            CHECK (RecurrenceType in ('DAILY', 'WEEKLY')),
    [RecurrenceWeekDays]    [NVARCHAR](50)      NULL,
    [RecurrenceStart]       [DATE]              NULL,
    [RecurrenceEnd]         [DATE]              NULL,

    constraint PK_Appointments          PRIMARY KEY (Id),
    constraint Appointments_fk0         FOREIGN KEY (CustomerId) references Customers,
    constraint Appointments_fk1         FOREIGN KEY (ServiceId) references Services,
    constraint Appointments_fk2         FOREIGN KEY (EmployeeId) references [Employees]
)

INSERT INTO Appointments ([From], [To], [ServiceId], [CustomerId], [EmployeeId]) values
('2021-03-16 9:00', '2021-03-16 10:00', 1, 1, 1)

INSERT INTO Appointments ([From], [To], [ServiceId], [CustomerId], [EmployeeId], [RecurrenceType], [RecurrenceWeekDays], [RecurrenceStart], [RecurrenceEnd]) values
('2021-03-16 15:00', '2021-03-16 16:00', 1, 1, 1, 'WEEKLY', '1,2', '2021-03-16', '2021-04-16')
