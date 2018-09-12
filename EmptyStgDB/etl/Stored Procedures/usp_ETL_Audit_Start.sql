CREATE PROC [etl].[usp_ETL_Audit_Start]
	@TableName	NVARCHAR(50)
,	@PkgName	NVARCHAR(50)
,	@PkgGUID	UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @AuditPKTab TABLE(AuditPK INT)
	DECLARE @AuditPK INT
	;

	INSERT INTO [etl].[ETL_Audit]
		(
			[TableName]
		,	[PkgName]
		,	[MillCode]
		,	[PkgGUID]
		,	[ExecStartDT]
		,	[ExecStopDT]
		,	[ExtractRowCount]
		,	[InsertRowCount]
		,	[UpdateRowCount]
		,	[ErrorRowCount]
		,	[SuccessFullProcessingInd]
		)
	OUTPUT inserted.AuditPK INTO @AuditPKTab
	VALUES
		(
			@TableName		--	[TableName]
		,	@PkgName		--	[PkgName]
		,	''				--	[MillCode]
		,	@PkgGUID		--	[PkgGUID]
		,	GETDATE()		--	[ExecStartDT]
		,	NULL			--	[ExecStopDT]
		,	NULL			--	[ExtractRowCount]
		,	NULL			--	[InsertRowCount]
		,	NULL			--	[UpdateRowCount]
		,	NULL			--	[ErrorRowCount]
		,	'Processing'	--	[SuccessFullProcessingInd]
		)

	SELECT @AuditPK = (SELECT MAX(AuditPK) FROM @AuditPKTab)
	SELECT @AuditPK AS AuditPK

END