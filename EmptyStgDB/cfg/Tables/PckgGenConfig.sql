CREATE TABLE [cfg].[PckgGenConfig] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [Project]          VARCHAR (100) NULL,
    [Package]          VARCHAR (100) NULL,
    [SrcType]          VARCHAR (10)  NULL,
    [SrcCode]          VARCHAR (MAX) NULL,
    [DesTabCreateFlag] INT           NULL,
    [DesTab]           VARCHAR (100) NULL,
    [MasterPackage]    VARCHAR (100) NULL,
    [SeqOrder]         INT           NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_PckgGenConfig] UNIQUE NONCLUSTERED ([Project] ASC, [Package] ASC)
);

