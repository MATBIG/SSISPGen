CREATE PROC [etl].[usp_ETL_Audit_Error]
		@AuditPK			INT
	,	@ExtractRowCount	INT	
	,	@InsertRowCount		INT	
	,	@UpdateRowCount		INT	
	,	@ErrorRowCount		INT
	,	@PkgName			NVARCHAR(50)
	,	@TableName			NVARCHAR(50)
	,	@ETLMode			INT
	,	@ExecStartDT		DATETIME
	,	@ErrorCode			INT
	,	@ErrorMsg			NVARCHAR(MAX)
AS
BEGIN
	
	UPDATE [etl].[ETL_Audit]
	SET
		[ExecStopDT]				=	GETDATE()
	,	[ExtractRowCount]			=	@ExtractRowCount		
	,	[InsertRowCount]			=	@InsertRowCount			
	,	[UpdateRowCount]			=	@UpdateRowCount			
	,	[ErrorRowCount]				=	@ErrorRowCount
	,	[SuccessFullProcessingInd]	=	'Failed'
	WHERE 1=1
	AND [AuditPK] = @AuditPK
	;

	INSERT INTO [etl].[ETL_ErrorLog]
	(
			[AuditPK]		
		,	[PkgName]		
		,	[TableName]		
		,	[ETLMode]		
		,	[ExecStartDT]	
		,	[ErrorCode]		
		,	[ErrorMsg]
	)
	VALUES
	(
			@AuditPK	
		,	@PkgName	
		,	@TableName	
		,	@ETLMode
		,	@ExecStartDT
		,	@ErrorCode
		,	@ErrorMsg
	)
	;
END