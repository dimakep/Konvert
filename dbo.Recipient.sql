CREATE TABLE [dbo].[Recipient] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Firm]      NVARCHAR (50) NOT NULL,
    [Index]     INT           NOT NULL,
    [Region]    NVARCHAR (50) NULL,
    [Area]      NVARCHAR (50) NULL,
    [City]      NVARCHAR (50) NULL,
    [Street]    NVARCHAR (50) NULL,
    [Home]      NVARCHAR (50) NULL,
    [Frame]     NVARCHAR (50) NULL,
    [Structure] NVARCHAR (50) NULL,
    [Flat]      NVARCHAR (50) NULL,
    CONSTRAINT [PK_Recipient] PRIMARY KEY CLUSTERED ([Id] ASC)
);

