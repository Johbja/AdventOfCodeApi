using AdventOfCode.Application.RestServices;
using AdventOfCode.Application.RestServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Application.Datasources.AdventOfCode;

public class AdventOfCodeRestService(
        IAdventOfCodeServiceSettings settings,
        IRestClientFactory clientFactory) 
    : RestService<IAdventOfCodeServiceApiCall>(
        clientFactory.CreateRestClient(settings))   
{

}

public interface IAdventOfCodeServiceSettings : IRestServiceSettings
{

}

public interface IAdventOfCodeServiceApiCall : IRestApiCall
{

}
