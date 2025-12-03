using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class TestableOperationAttribute : Attribute { }

