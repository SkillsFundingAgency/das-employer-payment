﻿using System.Data;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.EmployerPayments.Domain.Configuration;
using SFA.DAS.Sql.Client;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EAS.TestCommon.DbCleanup
{
    public class CleanDatabase : BaseRepository, ICleanDatabase
    {
        public CleanDatabase(EmployerPaymentsConfiguration configuration, ILog logger) : base(configuration.DatabaseConnectionString, logger)
        {
        }

        public async Task Execute()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@INCLUDEUSERTABLE", 1, DbType.Int16);
            await WithConnection<int>(async c => await c.ExecuteAsync(
                "[employer_account].[Cleardown]",
                parameters,
                commandType: CommandType.StoredProcedure));

            await WithConnection<int>(async c => await c.ExecuteAsync(
                "[employer_account].[SeedDataForRoles]",
                null,
                commandType: CommandType.StoredProcedure));

        }
    }
}
