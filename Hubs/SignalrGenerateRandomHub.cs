using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InowBackend.Models;

namespace InowBackend.Hubs
{
    public class SignalrGenerateRandomHub : Hub<ISignalrGenerateRandomHub>
    {
        //public void Counter()
        //{
        //    var stopwatch = new Stopwatch();
        //    stopwatch.Start();

        //    var random = new Random();

 
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int waitTimeMilliseconds = random.Next(100, 2500);
        //        Thread.Sleep(waitTimeMilliseconds);

   
        //        Clients.Caller.CounterUpdate(new CounterDTO { counter1 = i, counter2 = i + 1, counter3 = i + 2 });
        //    }

        //    stopwatch.Stop();


        //}
    }
}
