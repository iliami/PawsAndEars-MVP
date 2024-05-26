using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsAndEars.Services.Interfaces
{
    public interface ICreatingService<TParameter> 
        where TParameter : class
    {
        void Create(string id, TParameter value);
        void Create(string id);
    }
}
