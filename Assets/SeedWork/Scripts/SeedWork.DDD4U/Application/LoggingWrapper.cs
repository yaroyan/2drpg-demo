using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using System;
using System.Diagnostics;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Command;

namespace Yaroyan.SeedWork.DDD4U.Application
{
    public class LoggingWrapper : IApplicationService
    {
        readonly IApplicationService _service;

        public LoggingWrapper(IApplicationService service)
        {
            _service = service;
        }

        public virtual void Execute(ICommand command)
        {
            Console.WriteLine($@"Command: {command}");
            try
            {
                var watch = Stopwatch.StartNew();
                _service.Execute(command);
                Console.WriteLine($@"   Completed in {watch.ElapsedMilliseconds} ms");
            }
            catch (Exception e)
            {
                Console.WriteLine($@"Error: {e}");
            }
        }
    }
}
