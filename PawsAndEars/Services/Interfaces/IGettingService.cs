using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGrease;

namespace PawsAndEars.Services.Interfaces
{
    public interface IGettingService<TResult> where TResult : class
    {
        TResult Get(string id);
    }
}
