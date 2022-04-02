using InowBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InowBackend.Hubs
{
  public interface ISignalrGenerateRandomHub
    {
        Task CounterUpdate(CounterDTO obj);
    }
}
