using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.EAS.Domain.Configuration;
using SFA.DAS.EAS.Domain.Data.Repositories;
using SFA.DAS.EAS.Domain.Models.Payments;
using SFA.DAS.Sql.Client;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EAS.Infrastructure.Data
{
    public class DasLevyRepository : BaseRepository, IDasLevyRepository
    {
        private readonly LevyDeclarationProviderConfiguration _configuration;


        public DasLevyRepository(LevyDeclarationProviderConfiguration configuration, ILog logger)
            : base(configuration.DatabaseConnectionString, logger)
        {
            _configuration = configuration;
        }
        
        public async Task CreateNewPeriodEnd(PeriodEnd periodEnd)
        {
            await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PeriodEndId", periodEnd.Id, DbType.String);
                parameters.Add("@CalendarPeriodMonth", periodEnd.CalendarPeriodMonth, DbType.Int32);
                parameters.Add("@CalendarPeriodYear", periodEnd.CalendarPeriodYear, DbType.Int32);
                parameters.Add("@AccountDataValidAt", periodEnd.AccountDataValidAt, DbType.DateTime);
                parameters.Add("@CommitmentDataValidAt", periodEnd.CommitmentDataValidAt, DbType.DateTime);
                parameters.Add("@CompletionDateTime", periodEnd.CompletionDateTime, DbType.DateTime);
                parameters.Add("@PaymentsForPeriod", periodEnd.PaymentsForPeriod, DbType.String);

                return await c.ExecuteAsync(
                    sql: "[employer_financial].[CreatePeriodEnd]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });
        }

        public async Task<PeriodEnd> GetLatestPeriodEnd()
        {
            var result = await WithConnection(async c => await c.QueryAsync<PeriodEnd>(
                "[employer_financial].[GetLatestPeriodEnd]",
                null,
                commandType: CommandType.StoredProcedure));
            
            return result.SingleOrDefault();
        }
        
        public async Task CreatePaymentData(IEnumerable<PaymentDetails> payments)
        {
            using (var connection = new SqlConnection(_configuration.DatabaseConnectionString))
            {
                await connection.OpenAsync();

                using (var unitOfWork = new UnitOfWork(connection))
                {
                    try
                    {
                        foreach (var details in payments)
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@PaymentId", Guid.Parse(details.Id), DbType.Guid);
                            parameters.Add("@Ukprn", details.Ukprn, DbType.Int64);
                            parameters.Add("@ProviderName", details.ProviderName, DbType.String);
                            parameters.Add("@Uln", details.Uln, DbType.Int64);
                            parameters.Add("@AccountId", details.EmployerAccountId, DbType.Int64);
                            parameters.Add("@ApprenticeshipId", details.ApprenticeshipId, DbType.Int64);
                            parameters.Add("@DeliveryPeriodMonth", details.DeliveryPeriodMonth, DbType.Int32);
                            parameters.Add("@DeliveryPeriodYear", details.DeliveryPeriodYear, DbType.Int32);
                            parameters.Add("@CollectionPeriodId", details.CollectionPeriodId, DbType.String);
                            parameters.Add("@CollectionPeriodMonth", details.CollectionPeriodMonth, DbType.Int32);
                            parameters.Add("@CollectionPeriodYear", details.CollectionPeriodYear, DbType.Int32);
                            parameters.Add("@EvidenceSubmittedOn", details.EvidenceSubmittedOn, DbType.DateTime);
                            parameters.Add("@EmployerAccountVersion", details.EmployerAccountVersion, DbType.String);
                            parameters.Add("@ApprenticeshipVersion", details.ApprenticeshipVersion, DbType.String);
                            parameters.Add("@FundingSource", details.FundingSource, DbType.String);
                            parameters.Add("@TransactionType", details.TransactionType, DbType.String);
                            parameters.Add("@Amount", details.Amount, DbType.Decimal);
                            parameters.Add("@PeriodEnd", details.PeriodEnd, DbType.String);
                            parameters.Add("@StandardCode", details.StandardCode, DbType.Int64);
                            parameters.Add("@FrameworkCode", details.FrameworkCode, DbType.Int32);
                            parameters.Add("@ProgrammeType", details.ProgrammeType, DbType.Int32);
                            parameters.Add("@PathwayCode", details.PathwayCode, DbType.Int32);
                            parameters.Add("@PathwayName", details.PathwayName, DbType.String);
                            parameters.Add("@CourseName", details.CourseName, DbType.String);
                            parameters.Add("@ApprenticeName", details.ApprenticeName, DbType.String);
                            parameters.Add("@ApprenticeNINumber", details.ApprenticeNINumber, DbType.String);
                            parameters.Add("@ApprenticeshipCourseLevel", details.CourseLevel, DbType.Int32);
                            parameters.Add("@ApprenticeshipCourseStartDate", details.CourseStartDate, DbType.DateTime);

                            await unitOfWork.Execute("[employer_financial].[CreatePayment]", parameters,
                                CommandType.StoredProcedure);
                        }

                        unitOfWork.CommitChanges();
                    }
                    catch (Exception)
                    {
                        unitOfWork.RollbackChanges();
                        throw;
                    }
                }
            }
        }

        public async Task<Payment> GetPaymentData(Guid paymentId)
        {
            var result = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@paymentId", paymentId, DbType.Guid);

                return await c.QueryAsync<Payment>(
                    sql: "[employer_financial].[GetPaymentData_ById]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<Guid>> GetAccountPaymentIds(long accountId)
        {
            var result = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId, DbType.Int64);

                return await c.QueryAsync<Guid>(
                    sql: "[employer_financial].[GetAccountPaymentIds]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            return result.ToArray();
        }
        
    }
}

