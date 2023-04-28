
CREATE PROCEDURE [dbo].[usp_GetAccountDetails]
	@AccountId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		*
	FROM
		Orders
	WHERE
		AccountId = @AccountId

	SELECT 
		IssuerName
		,SharePrice
		,TotalShare
	FROM
		ShareByAccounts
	WHERE
		AccountId = @AccountId

	SELECT 
		*
	FROM
		Accounts
	WHERE
		AccountId = @AccountId
END