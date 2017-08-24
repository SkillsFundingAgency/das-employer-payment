﻿CREATE PROCEDURE [CreatePeriodEnd]
	@PeriodEndId nvarchar(20),
	@CalendarPeriodMonth int,
	@CalendarPeriodYear int,
	@AccountDataValidAt datetime = null,
	@CommitmentDataValidAt datetime = null,
	@CompletionDateTime datetime,
	@PaymentsForPeriod nvarchar(250)
AS

INSERT INTO [PeriodEnd]
           ([PeriodEndId]
           ,[CalendarPeriodMonth]
           ,[CalendarPeriodYear]
           ,[AccountDataValidAt]
           ,[CommitmentDataValidAt]
           ,[CompletionDateTime]
           ,[PaymentsForPeriod])
     VALUES
           (@PeriodEndId
           ,@CalendarPeriodMonth
           ,@CalendarPeriodYear
           ,@AccountDataValidAt
           ,@CommitmentDataValidAt
           ,@CompletionDateTime
           ,@PaymentsForPeriod)