CREATE PROC [etl].[usp_ETL_Audit_Stop]
	@AuditPK			INT
,	@ExtractRowCount	INT	
,	@InsertRowCount		INT	
,	@UpdateRowCount		INT	
,	@ErrorRowCount		INT
AS
BEGIN

	UPDATE [etl].[ETL_Audit]
	SET 
		[ExecStopDT]				=	GETDATE()
	,	[ExtractRowCount]			=	@ExtractRowCount		
	,	[InsertRowCount]			=	@InsertRowCount			
	,	[UpdateRowCount]			=	@UpdateRowCount			
	,	[ErrorRowCount]				=	@ErrorRowCount			
	,	[SuccessFullProcessingInd]	=	'Completed'
	WHERE [AuditPK] = @AuditPK

END