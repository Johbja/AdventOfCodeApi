using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.Operations.Interfaces;
public interface IApplicationOperationResolver
{
    TOperation ResolveOperation<TOperation>() where TOperation : class, IApplicationOperation;
}
