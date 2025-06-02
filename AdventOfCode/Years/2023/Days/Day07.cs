using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Attributes;

namespace AdventOfCode.Years._2023;

[DayInfo("Camel Cards", "Day 7")]
internal class Day07 : ISolution
{
    private readonly string[] _input;
    private readonly List<(int[] cards, int bid)> hands;
    private readonly List<(int[] cards, int bid)> handsPartTwo;

    private readonly Dictionary<string, int> cardRanks = new()
    {
        {
            "5", 7
        },
        {
            "41", 6
        },
        {
            "32", 5
        },
        {
            "311", 4
        },
        {
            "221", 3
        },
        {
            "2111", 2
        },
        {
            "11111",  1
        }
    };

    public Day07(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day07), 2023);
        hands = _input.Select(line => ParseHand(line.Split(" ", StringSplitOptions.RemoveEmptyEntries))).ToList();
        handsPartTwo = _input.Select(line => ParseHandWithJoker(line.Split(" ", StringSplitOptions.RemoveEmptyEntries))).ToList();
    }

    private (int[] hand, int bid) ParseHand(string[] inputLine)
        => (inputLine[0].Select(ConvertCard).ToArray(), int.Parse(inputLine[1]));

    private (int[] hand, int bid) ParseHandWithJoker(string[] inputLine)
        => (inputLine[0].Select(ConverCardWithJoker).ToArray(), int.Parse(inputLine[1]));

    private int ConvertCard(char card)
        => card switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => 11,
            'T' => 10,
            _ => int.Parse(card.ToString())
        };

    private int ConverCardWithJoker(char card)
        => card switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'T' => 10,
            'J' => 1,
            _ => int.Parse(card.ToString())
        };

    private int FindOptimalJokerExchange(int[] cards)
    {
        int[] cardsCopy = new int[cards.Length];
        Array.Copy(cards, cardsCopy, cards.Length);

        int[] IndexOfJokers = cardsCopy
            .Select((card, index) => (card, index))
            .Where(x => x.card == 1)
            .Select(x => x.index)
            .ToArray();

        int maxRank = 0;

        for(int i = 1; i <= 14; i++)
        {
            foreach(var index in IndexOfJokers)
            {
                cardsCopy[index] = i;
            }

            var rank = cardsCopy
                .GroupBy(x => x)
                .ToList()
                .Select(x => x.Count().ToString())
                .OrderByDescending(x => x)
                .Aggregate("", (a, b) => a + b);

            var rankValue = cardRanks[rank];

            if(rankValue > maxRank)
                maxRank = rankValue;
        }

        return maxRank;
    }

    public void SolvePartOne()
    {
        var rankedHands = hands.Select(hand =>
        {
            var rank = hand.cards
                .GroupBy(x => x)
                .ToList()
                .Select(x => x.Count().ToString())
                .OrderByDescending(x => x)
                .Aggregate("", (a, b) => a + b);

            var rankValue = cardRanks[rank];

            return (rankValue, rank, hand);
        })
        .GroupBy(x => x.rankValue)
        .ToDictionary(group => group.Key, group => group.ToList());

        foreach (var key in rankedHands.Keys)
        {
            rankedHands[key].Sort((a, b) =>
            {
                for (int i = 0; i < a.hand.cards.Length; i++)
                {
                    if (a.hand.cards[i] > b.hand.cards[i])
                        return 1;
                    if (a.hand.cards[i] < b.hand.cards[i])
                        return -1;
                }

                return 0;
            });
        }

        var orderedHands = rankedHands
            .Select(x => (x.Key, x.Value))
            .OrderBy(x => x.Key)
            .SelectMany(x => x.Value);

        int sum = orderedHands.Select((hand, index) => hand.hand.bid * (index + 1)).Sum();

        Console.WriteLine($"Total winnings: {sum}");
    }

    public void SolvePartTwo()
    {
        var rankedHands = handsPartTwo.Select(hand =>
        {
            var currentCards = hand.cards;

            var rank = currentCards
                .GroupBy(x => x)
                .ToList()
                .Select(x => x.Count().ToString())
                .OrderByDescending(x => x)
                .Aggregate("", (a, b) => a + b);

            var rankValue = cardRanks[rank];

            if (hand.cards.Contains(1))
            {
                rankValue = FindOptimalJokerExchange(currentCards);
            }

            return (rankValue, rank, hand);
        })
        .GroupBy(x => x.rankValue)
        .ToDictionary(group => group.Key, group => group.ToList());

        foreach (var key in rankedHands.Keys)
        {
            rankedHands[key].Sort((a, b) =>
            {
                for (int i = 0; i < a.hand.cards.Length; i++)
                {
                    if (a.hand.cards[i] > b.hand.cards[i])
                        return 1;
                    if (a.hand.cards[i] < b.hand.cards[i])
                        return -1;
                }

                return 0;
            });
        }

        var orderedHands = rankedHands
            .Select(x => (x.Key, x.Value))
            .OrderBy(x => x.Key)
            .SelectMany(x => x.Value);

        int sum = orderedHands.Select((hand, index) => hand.hand.bid * (index + 1)).Sum();

        Console.WriteLine($"Total winnings: {sum}");
    }
}
