using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Attributes;

namespace AdventOfCode.Years._2023;

[DayInfo("Scratchcards", "Day 4")]
public class Day04 : ISolution
{
    private readonly string[] _input;

    public Day04(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day04), 2023);
    }

    public void SolvePartOne()
    {
        var cards = _input.Select(x => x.Split(":")[1].Split("|").Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray()).ToList()).ToList();

        int sum = 0;
        foreach(var card in cards)
        {
            var result = card[1].Where(number => card[0].Contains(number)).ToArray();

            int points = 0;
            for(int i = 0; i < result.Length; i++)
            {
                if(i == 0)
                {
                    points = 1;
                    continue;
                }

                points *= 2;
            }

            sum += points;
        }

        Console.WriteLine(sum);
    }

    public void SolvePartTwo()
    {
        var cardsTable = _input.Select(x => x.Split(":"))
            .Select
            (
                x => 
                (
                    cardNumber: int.Parse(x[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]), 
                    card: x[1].Split("|")
                        .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => int.Parse(x)).ToArray())
                        .ToList()
                )
            ).ToDictionary(key => key.cardNumber, value => (count:1, value.card));;

        foreach(var key in cardsTable.Keys)
        {
            var currentCard = cardsTable[key];
            
            for(int cardCount = 0; cardCount < currentCard.count; cardCount++)
            {
                var cardResult = currentCard.card[1].Where(number => currentCard.card[0].Contains(number)).Count();

                for (int offsetKey = key + 1; offsetKey <= key + cardResult; offsetKey++)
                {
                    cardsTable[offsetKey] = (cardsTable[offsetKey].count + 1, cardsTable[offsetKey].card);
                }
            }
        }

        var result = cardsTable.Select(x => x.Value.count).Sum();

        Console.WriteLine(result);
    }
}
