using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Application.Operations.Interfaces;
using AdventOfCode.Application.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode.Application.Tests.Operations;

public class OperationsTestHarness(AdventOfCodeOperationsFixture fixture)
    : IClassFixture<AdventOfCodeOperationsFixture>
{
    private readonly IApplicationOperationResolver _operationResolver =
        fixture.ServiceProvider.GetRequiredService<IApplicationOperationResolver>();

    protected TOperation CreateOperation<TOperation>()
        where TOperation : class,
        IApplicationOperation
    {
        return _operationResolver.ResolveOperation<TOperation>();
    }
}

