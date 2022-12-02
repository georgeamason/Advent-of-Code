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

            var roundScore = BattleEngine(foe.Sign, friendly.Sign);
            totalScore += roundScore + friendly.Score;
        }

        Console.WriteLine(totalScore);
    }

    private static int BattleEngine(Sign foe, Sign friendly)
    {
        if (foe == friendly)
            return 3;

        if (foe is Sign.Rock)
            return friendly is Sign.Paper ? 6 : 0;

        if (foe is Sign.Paper)
            return friendly is Sign.Scissors ? 6 : 0;

        if (foe is Sign.Scissors)
            return friendly is Sign.Rock ? 6 : 0;

        throw new Exception();
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

    private enum Sign
    {
        Rock,
        Paper,
        Scissors
    };
}