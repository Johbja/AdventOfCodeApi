using AdventOfCode.Application.RestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.Datasources.AdventOfCode;
public class AdventOfCodeServiceSettings : IAdventOfCodeServiceSettings
{
    public string BaseClientUrl => "https://adventofcode.com/";

    public IReadOnlyCollection<Header> DefaultHeaders => [];
}
