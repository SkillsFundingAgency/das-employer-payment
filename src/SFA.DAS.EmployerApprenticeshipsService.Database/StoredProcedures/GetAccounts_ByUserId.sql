﻿CREATE PROCEDURE [dbo].[GetAccounts_ByUserId]
	@userId UNIQUEIDENTIFIER
	
AS
select 
	a.Id, a.Name, m.RoleId 
from 
	[dbo].[User] u 
inner join 
	[dbo].[Membership] m on m.UserId = u.Id
inner join
	[dbo].[Account]  a on m.AccountId = a.Id
where 
u.PireanKey = @userId
