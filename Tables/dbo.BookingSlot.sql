CREATE TABLE [dbo].[BookingSlot]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[VehicleNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AllocatedDate] [date] NULL,
[AllocatedTime] [time] NULL,
[VehicleType] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SlotNo] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookingSlot] ADD CONSTRAINT [PK_ParkingSlot] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
