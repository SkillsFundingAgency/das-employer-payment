﻿CREATE PROCEDURE [GetPaymentDetail_ByAccountProviderCourseAndDateRange]
	@accountId BIGINT,
	@ukprn BIGINT,
	@courseName NVARCHAR(MAX),
	@pathwayCode INT,
	@courseLevel INT,
	@fromDate DATETIME,
	@toDate DATETIME
AS	
SELECT
    p.[AccountId]
     ,3 as TransactionType
	--,MAX(dervx.DateCreated) as DateCreated
	--,MAX(dervx.TransactionDate) as TransactionDate
	,MAX(p.PaymentId) as PaymentId
	,MAX(p.UkPrn) as UkPrn
	,MAX(p.PeriodEnd) as PeriodEnd	
	,MAX(meta.ProviderName) as ProviderName
	,(SUM(pays1.[Amount]) * -1) as LineAmount
	,meta.ApprenticeshipCourseName as CourseName
	,meta.ApprenticeshipCourseLevel	as CourseLevel 
	,meta.PathwayName as PathwayName
	,MAX(meta.ApprenticeshipCourseStartDate) as CourseStartDate
	,MAX(meta.ApprenticeName) as ApprenticeName
	,MAX(meta.ApprenticeNINumber) as ApprenticeNINumber	
	,(SUM(pays2.Amount) * -1) as SfaCoInvestmentAmount
	,(SUM(pays3.Amount) * -1) as EmployerCoInvestmentAmount	
  FROM [Payment] p
  inner JOIN [PaymentMetaData] meta ON p.PaymentMetaDataId = meta.Id
  --inner join (select PeriodEnd,AccountId,ukprn, TransactionDate, DateCreated from TransactionLine where DateCreated >= @fromDate AND 
  --      DateCreated <= @toDate) dervx on dervx.AccountId = p.AccountId and dervx.PeriodEnd = p.PeriodEnd and dervx.Ukprn = p.Ukprn
  left join [Payment] pays1 on pays1.AccountId = p.AccountId and pays1.Ukprn = p.Ukprn and pays1.FundingSource = 1 and pays1.PaymentMetaDataId = meta.id
  left join [Payment] pays2 on pays2.AccountId = p.AccountId and pays2.Ukprn = p.Ukprn and pays2.FundingSource = 2 and pays2.PaymentMetaDataId = meta.id
  left join [Payment] pays3 on pays3.AccountId = p.AccountId and pays3.Ukprn = p.Ukprn and pays3.FundingSource = 3 and pays3.PaymentMetaDataId = meta.id
  where 
  p.AccountId = @accountid AND
  p.Ukprn = @ukprn AND
  meta.ApprenticeshipCourseName = @courseName AND
  meta.ApprenticeshipCourseLevel = @courseLevel AND
  ISNULL(meta.PathwayCode, -1) = ISNULL(@pathwayCode, -1) AND
  p.FundingSource IN (1,2,3)  
  group by p.AccountId, p.Ukprn, meta.ApprenticeshipCourseName, meta.ApprenticeshipCourseLevel, meta.PathwayName, meta.ApprenticeName