CREATE PROCEDURE [dbo].[usp_UpdateShareByAccount]
	@AccountId int
	,@IssuerName varchar(5)
	,@SharePrice money
	,@TotalShare int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE ShareByAccounts
		SET 
			SharePrice = @SharePrice
			,TotalShare = @TotalShare
	WHERE 
		AccountId = @AccountId 
		AND IssuerName = @IssuerName
END