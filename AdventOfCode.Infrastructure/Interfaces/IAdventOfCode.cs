using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Infrastructure.Interfaces;
public interface IAdventOfCode
{
    public ISolution CreateInstanceOfSolution(string arg);
    public void Solve(ISolution solution);
    public void PrintSeparetor(int length);
    public void PrintRepo();
}
