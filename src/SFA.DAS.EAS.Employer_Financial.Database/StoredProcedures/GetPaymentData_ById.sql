CREATE PROCEDURE [GetPaymentData_ById]
	@paymentId uniqueIdentifier
	
AS
	SELECT * from [Payment] where PaymentId = @paymentId

