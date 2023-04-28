
CREATE PROCEDURE [dbo].[usp_UpdateAccount]
	@AccountId int
	,@Cash money
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Accounts
		SET 
			Cash = @Cash
	WHERE 
		AccountId = @AccountId 
END