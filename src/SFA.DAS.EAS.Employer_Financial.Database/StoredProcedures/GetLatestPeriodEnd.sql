CREATE PROCEDURE [GetLatestPeriodEnd]
	
AS
SELECT top 1 
      [PeriodEndId] AS Id
      ,[CalendarPeriodMonth]
      ,[CalendarPeriodYear]
      ,[AccountDataValidAt]
      ,[CommitmentDataValidAt]
      ,[CompletionDateTime]
      ,[PaymentsForPeriod]
  FROM [PeriodEnd]
  ORDER BY 
	completiondatetime DESC

