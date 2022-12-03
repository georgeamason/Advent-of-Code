// See https://aka.ms/new-console-template for more information

namespace RockPaperScissors;

internal static class Program
{
    private static void Main()
    {
        using var streamReader = new StreamReader("input.txt");

        var totalScore = 0;
        while (!streamReader.EndOfStream)
        {
            var round = streamReader.ReadLine();

            var plays = round!.Split(' ');

            totalScore += Part2(plays);
        }

        Console.WriteLine(totalScore);
    }

    private static int Part1(IReadOnlyList<string> plays)
    {
        var foe = new Shape(plays[0]);
        var friendly = new Shape(plays[1]);

        var result = BattleEngine(foe.Sign, friendly.Sign);

        return result switch
        {
            Outcome.Win => friendly.Score + 6,
            Outcome.Draw => friendly.Score + 3,
            Outcome.Loss => friendly.Score,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static int Part2(IReadOnlyList<string> plays)
    {
        var foe = new Shape(plays[0]);

        var expectedOutcome = new Shape(plays[1]).Sign switch
        {
            Sign.Rock => Outcome.Loss,
            Sign.Paper => Outcome.Draw,
            Sign.Scissors => Outcome.Win,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var friendly = BattleEngine(foe.Sign, expectedOutcome);

        return friendly switch
        {
            Sign.Rock => 1 + (int) expectedOutcome,
            Sign.Paper => 2 + (int) expectedOutcome,
            Sign.Scissors => 3 + (int) expectedOutcome,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static Outcome BattleEngine(Sign foe, Sign friendly)
    {
        if (foe == friendly)
            return Outcome.Draw;

        return foe switch
        {
            Sign.Rock => friendly is Sign.Paper ? Outcome.Win : Outcome.Loss,
            Sign.Paper => friendly is Sign.Scissors ? Outcome.Win : Outcome.Loss,
            Sign.Scissors => friendly is Sign.Rock ? Outcome.Win : Outcome.Loss,
            _ => throw new ArgumentOutOfRangeException(nameof(foe))
        };
    }

    private static Sign BattleEngine(Sign foe, Outcome outcome)
    {
        if (outcome == Outcome.Draw)
            return foe;

        return foe switch
        {
            Sign.Rock => outcome is Outcome.Win ? Sign.Paper : Sign.Scissors,
            Sign.Paper => outcome is Outcome.Win ? Sign.Scissors : Sign.Rock,
            Sign.Scissors => outcome is Outcome.Win ? Sign.Rock : Sign.Paper,
            _ => throw new ArgumentOutOfRangeException(nameof(foe))
        };
    }

    private sealed class Shape
    {
        internal Shape(string input)
        {
            _input = char.Parse(input);
        }

        public Sign Sign => ToShape();

        public int Score => ToScore();

        private Sign ToShape() => _input switch
        {
            'A' or 'X' => Sign.Rock,
            'B' or 'Y' => Sign.Paper,
            'C' or 'Z' => Sign.Scissors,
            _ => throw new ArgumentException(nameof(_input))
        };

        private int ToScore() => _input switch
        {
            'A' or 'X' => 1,
            'B' or 'Y' => 2,
            'C' or 'Z' => 3,
            _ => throw new ArgumentException(nameof(_input))
        };

        private readonly char _input;
    }

    private enum Outcome
    {
        Win = 6,
        Draw = 3,
        Loss = 0
    }

    private enum Sign
    {
        Rock,
        Paper,
        Scissors
    };
}