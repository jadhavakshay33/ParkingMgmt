CREATE TABLE [dbo].[History]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[VehicleNo] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AllocatedDate] [date] NOT NULL,
[AllocatedTime] [time] NOT NULL,
[UnAllocatedDate] [date] NOT NULL,
[UnAllocatedTime] [time] NOT NULL,
[SlotNo] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[History] ADD CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
