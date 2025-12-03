using AdventOfCode.Api.Attributes;
using AdventOfCode.Application.Operations;
using AdventOfCode.Application.Operations.Interfaces;
using AdventOfCode.Application.Operations.Years;
using Microsoft.AspNetCore.Mvc;

namespace AdventOfCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ControllerName("Advent of code 2025")]
    public class AdventOfCode2025Controller(IApplicationOperationResolver operationResolver) 
        : ApiController(operationResolver)
    {
        
        [HttpPost("v1/solve-with-payload-for/{day}")]
        public async Task<IActionResult> SolveForDay(
            [FromRoute] int day,
            IFormFile payload)
        {
            if (payload.ContentType != "text/plain")
                throw new ArgumentException("payload is not a text file");

            using var reader = new StreamReader(payload.OpenReadStream());
            var fileContent = await reader.ReadToEndAsync();

            var input = new AdventOfCode2025DaySelectionOperation.Input(day, fileContent);
            var traceableOutput = CreateOperation<AdventOfCode2025DaySelectionOperation>().Execute(input);
            var result = await CreateBaseResponse(traceableOutput);
            return Ok(result);
        }

        [HttpPost("v1/solve-with-session-key/{sessionKey}/for/{day}")]
        public async Task<IActionResult> SolveWithInputFromAdventOfCode(
            [FromRoute] string sessionKey,
            [FromRoute] int day)
        {
            var textInputResult = CreateOperation<GetInputFromAdventOfCodeOperation>()
                .Execute(new GetInputFromAdventOfCodeOperation.Input(2025, day, sessionKey));
            var textInputBaseResponse = await CreateBaseResponse(textInputResult);


            var input = new AdventOfCode2025DaySelectionOperation.Input(day, textInputBaseResponse.Result.TextInput);
            var resultFromSolvedProblem = CreateOperation<AdventOfCode2025DaySelectionOperation>().Execute(input);
            _ = await CreateBaseResponse(resultFromSolvedProblem);

            return Ok(GetCurrentResponses);
        }
    }
}
