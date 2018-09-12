CREATE TABLE [etl].[ETL_Audit] (
    [AuditPK]                  INT              IDENTITY (1, 1) NOT NULL,
    [TableName]                NVARCHAR (50)    NOT NULL,
    [PkgName]                  NVARCHAR (50)    NULL,
    [MillCode]                 NVARCHAR (50)    NULL,
    [PkgGUID]                  UNIQUEIDENTIFIER NULL,
    [ExecStartDT]              DATETIME         NULL,
    [ExecStopDT]               DATETIME         NULL,
    [ExtractRowCount]          BIGINT           NULL,
    [InsertRowCount]           BIGINT           NULL,
    [UpdateRowCount]           BIGINT           NULL,
    [ErrorRowCount]            BIGINT           NULL,
    [SuccessFullProcessingInd] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_AuditPK] PRIMARY KEY CLUSTERED ([AuditPK] ASC)
);

