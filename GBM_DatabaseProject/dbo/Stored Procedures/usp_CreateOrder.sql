CREATE PROCEDURE [dbo].[usp_CreateOrder]
	@IssuerName varchar(5)
	,@Shares int
	,@SharePrice money
	,@Timestamp int
	,@AccountId int
	,@Operation bit
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO
		Orders
	(IssuerName, Shares, SharePrice, [Timestamp], AccountId, Operation)
		Values
	(@IssuerName, @Shares, @SharePrice, @Timestamp, @AccountId, @Operation)

	SELECT @@IDENTITY AS OrderId
		
END