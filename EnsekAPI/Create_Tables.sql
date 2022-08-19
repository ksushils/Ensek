CREATE TABLE [dbo].[AccountInfo] (
    [AccountId] INT           NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([AccountId] ASC)
);



CREATE TABLE [dbo].[Meter_Reading] (
    [Id]                   INT      IDENTITY (1, 1) NOT NULL,
    [AccountId]            INT      NOT NULL,
    [MeterReadingDateTime] DATETIME NOT NULL,
    [MeterReadValue]       INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Meter_Reading_ToTable] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[AccountInfo] ([AccountId])
);




