﻿CREATE VIEW [account].[GetAccountPayeSchemes]
AS 
SELECT p.Ref AS EmpRef,
	ah.AccountId,
	acc.HashedId,
	l.Name AS LegalEntityName,
	l.Id as LegalEntityId
FROM 
	[account].[Paye] p
inner join 
	[account].[LegalEntity] l on l.Id = p.LegalEntityId
inner join 
	[account].[AccountHistory] ah on ah.PayeRef = p.Ref and ah.RemovedDate is null
inner join 
	[account].[Account] acc on acc.Id = ah.AccountId

