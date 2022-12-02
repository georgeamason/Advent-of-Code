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

            var x = round!.Split(' ');

            var foe = new Shape(x[0]);
            var friendly = new Shape(x[1]);

            var result = BattleEngine(foe.Sign, friendly.Sign);

            totalScore += result switch
            {
                Result.Win => friendly.Score + 6,
                Result.Draw => friendly.Score + 3,
                Result.Loss => friendly.Score,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        Console.WriteLine(totalScore);
    }

    private static Result BattleEngine(Sign foe, Sign friendly)
    {
        if (foe == friendly)
            return Result.Draw;

        return foe switch
        {
            Sign.Rock => friendly is Sign.Paper ? Result.Win : Result.Loss,
            Sign.Paper => friendly is Sign.Scissors ? Result.Win : Result.Loss,
            Sign.Scissors => friendly is Sign.Rock ? Result.Win : Result.Loss,
            _ => throw new Exception()
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

    private enum Result
    {
        Win,
        Draw,
        Loss
    }

    private enum Sign
    {
        Rock,
        Paper,
        Scissors
    };
}