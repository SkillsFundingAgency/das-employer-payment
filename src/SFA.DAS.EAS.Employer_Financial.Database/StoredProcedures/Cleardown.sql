CREATE PROCEDURE [Cleardown]
AS
	
	DELETE FROM [Payment]
	DELETE FROM [PeriodEnd]
	DELETE FROM [PaymentMetaData]
RETURN 0
