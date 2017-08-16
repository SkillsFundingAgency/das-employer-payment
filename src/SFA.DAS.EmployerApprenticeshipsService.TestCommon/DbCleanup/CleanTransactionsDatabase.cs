using System.Data;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.EmployerPayments.Domain.Configuration;
using SFA.DAS.Sql.Client;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EAS.TestCommon.DbCleanup
{
    public class CleanTransactionsDatabase : BaseRepository, ICleanTransactionsDatabase
    {
        public CleanTransactionsDatabase(EmployerPaymentsConfiguration configuration, ILog logger) : base(configuration.DatabaseConnectionString, logger)
        {
        }

        public async Task Execute()
        {
            await WithConnection(async c => await c.ExecuteAsync(
                "[employer_financial].[Cleardown]",
                commandType: CommandType.StoredProcedure));   
        }
    }
}
