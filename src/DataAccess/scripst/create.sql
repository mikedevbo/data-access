
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[BirthYear] [int] NOT NULL,
	[BirthMonth] [int] NOT NULL,
	[BirthDay] [int] NOT NULL,
	[BirthplaceCountry] [varchar](50) NOT NULL,
	[BirthplaceCity] [varchar](50) NOT NULL,
	[Weight] [int] NOT NULL,
	[Height] [int] NOT NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Player](
	[Id] [int] NOT NULL,
	[IsRightHanded] [bit] NOT NULL,
	[IsTwoHandedBackhand] [bit] NOT NULL,
	[CoachId] [int] NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Player]  WITH NOCHECK ADD  CONSTRAINT [FK_Player_Person_CoachId] FOREIGN KEY([CoachId])
REFERENCES [dbo].[Person] ([Id])
GO

ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_Person_CoachId]
GO

ALTER TABLE [dbo].[Player]  WITH NOCHECK ADD  CONSTRAINT [FK_Player_Person_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
GO

ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_Person_Id]
GO

create view PlayersBaseInfo
as
	select
		person.id
		, person.FirstName
		, person.LastName
		, person.BirthYear
		, person.BirthMonth
		, person.BirthDay
		, person.BirthplaceCountry
		, person.BirthplaceCity
		, person.[Weight]
		, person.Height
		, player.IsRightHanded
		, player.IsTwoHandedBackhand
		, coach.Id			as CoachId
		, coach.FirstName	as CoachFirstName
		, coach.LastName	as CoachLastName
	from dbo.Player player
		inner join dbo.Person person on player.Id = person.Id
		left join dbo.Person coach on player.CoachId = coach.Id

insert into [dbo].[Person]
	select 'Iron', 'Man', '2018', '12', '01', 'Poland', 'Warsaw', '85', '185'
	union
	select 'Spider', 'Man', '2018', '12', '01', 'Poland', 'Warsaw', '85', '185'