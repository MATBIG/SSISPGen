CREATE TABLE [etl].[ETL_ErrorLog] (
    [ErrorPK]     INT            IDENTITY (1, 1) NOT NULL,
    [AuditPK]     INT            NULL,
    [PkgName]     NVARCHAR (50)  NULL,
    [TableName]   NVARCHAR (50)  NULL,
    [ETLMode]     SMALLINT       NULL,
    [ExecStartDT] DATETIME       NULL,
    [ErrorCode]   INT            NULL,
    [ErrorMsg]    NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ErrorPK] ASC)
);

