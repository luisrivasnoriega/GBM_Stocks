
CREATE PROCEDURE [dbo].[usp_GetAccount]
	@AccountId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		*
	FROM
		Accounts
	WHERE
		AccountId = @AccountId
END