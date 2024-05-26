using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndEars.Services.Interfaces
{
    public interface IService<TParameter, TResult> : 
        IGettingService<TResult>, 
        ICreatingService<TParameter>
        where TParameter : class
        where TResult : class
    {

    }
}
