using Hangfire;
using HangfireCoreExample.Domain.Services;
using System;

namespace HangfireCoreExample.Application.Services
{
    public class JobCreator : IJobCreator
    {
        public IBackgroundJobClient JobClient { get; }

        public JobCreator(IBackgroundJobClient jobClient)
        {
            JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
        }

        public string NewFireAndForget(string message)
        {
            return JobClient.Enqueue(() => System.Diagnostics.Debug.WriteLine($"Hello, {message}"));
        }
    }
}
