CREATE PROCEDURE [dbo].[usp_CreateShareByAccount]
	@AccountId int
	,@IssuerName varchar(5)
	,@SharePrice money
	,@TotalShare int
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO
		ShareByAccounts
	(AccountId, IssuerName, SharePrice, TotalShare)
		Values
	(@AccountId, @IssuerName, @SharePrice, @TotalShare)

	SELECT @@IDENTITY AS ShareByAccountId
		
END