using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireCoreExample.Domain.Services
{
    public interface IJobCreator
    {
        string NewFireAndForget(string message);
    }
}
