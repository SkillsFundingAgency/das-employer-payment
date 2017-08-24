using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.ServiceRuntime;
using SFA.DAS.EmployerPayments.Domain.Configuration;
using SFA.DAS.EmployerPayments.Infrastructure.DependencyResolution;
using SFA.DAS.EmployerPayments.Infrastructure.Logging;
using SFA.DAS.EmployerPayments.Worker.DependencyResolution;
using SFA.DAS.EmployerPayments.Worker.Providers;
using SFA.DAS.Messaging;
using StructureMap;

namespace SFA.DAS.EmployerPayments.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        private const string ServiceName = "SFA.DAS.EmployerPayments";

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);
        private IContainer _container;

        public override void Run()
        {
            LoggingConfig.ConfigureLogging();
            Trace.TraceInformation("SFA.DAS.PaymentProvider.Worker is running");

            try
            {
                var providers = _container.GetAllInstances<IMessageProcessor>();
                var taskList = providers.Select(x => x.RunAsync(_cancellationTokenSource.Token));
                Task.WaitAll(taskList.ToArray());
            }
            finally
            {
                _runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            var result = base.OnStart();

            Trace.TraceInformation("SFA.DAS.EAS.PaymentProvider.Worker has been started");
           
            _container = new Container(c =>
            {
                c.Policies.Add(new ConfigurationPolicy<EmployerPaymentsConfiguration>(ServiceName));
                c.Policies.Add(new ConfigurationPolicy<PaymentsApiClientConfiguration>("SFA.DAS.PaymentsAPI"));
                c.Policies.Add(new ConfigurationPolicy<CommitmentsApiClientConfiguration>("SFA.DAS.CommitmentsAPI"));
                c.Policies.Add(new MessagePolicy<EmployerPaymentsConfiguration>(ServiceName));
                c.Policies.Add(new ExecutionPolicyPolicy());
                c.AddRegistry<DefaultRegistry>();
            });
            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("SFA.DAS.EAS.PaymentProvider.Worker is stopping");

            _cancellationTokenSource.Cancel();
            _runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("SFA.DAS.EAS.PaymentProvider.Worker has stopped");
        }
    }
}
