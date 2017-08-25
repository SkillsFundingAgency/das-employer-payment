CREATE PROCEDURE [GetAccountPaymentIds]
	@accountId BIGINT = 0	
AS
	SELECT PaymentId FROM [Payment] where AccountId = @accountId

