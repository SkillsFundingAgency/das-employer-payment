CREATE PROCEDURE [dbo].[CreateAccount]
	@accountId BIGINT
	
AS

	INSERT INTO ACCOUNT (AccountId) 
	VALUES (@accountId)

GO