
CREATE PROCEDURE [dbo].[usp_CreateAccount]
	@Cash money
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO
		Accounts
	(Cash)
		Values
	(@Cash)

	SELECT @@IDENTITY AS AccountId
END