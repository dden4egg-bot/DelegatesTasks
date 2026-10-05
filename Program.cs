namespace DelegatesTasks;

// ============================================================
// Task 1. Class + custom delegate
// ============================================================
public delegate List<int> RangeGenerator(int start, int end);

public class NumberRange
{
    public List<int> GenerateRange(int start, int end)
    {
        var result = new List<int>();
        for (int i = start; i <= end; i++)
            result.Add(i);
        return result;
    }
}

// ============================================================
// Main program
// ============================================================
internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // ---------- Task 1 ----------
        var numberRange = new NumberRange();
        RangeGenerator rangeGen = numberRange.GenerateRange;

        List<int> numbers = rangeGen(1, 30);
        Console.WriteLine("[Task 1] Range 1..30:");
        Console.WriteLine("  " + string.Join(", ", numbers));

        // ---------- Task 2 ----------
        Action<string> printMessage = message => Console.WriteLine(message);

        // ---------- Task 3 ----------
        Func<string, List<string>> splitSentence = sentence =>
            sentence
                .Split(new[] { ' ', ',', '.', '!', '?', ';', ':' },
                       StringSplitOptions.RemoveEmptyEntries)
                .ToList();

        string sentence = "Delegates in C# are powerful and flexible tools";
        List<string> words = splitSentence(sentence);

        Console.WriteLine("\n[Task 3] Words from sentence:");
        Console.WriteLine("  " + string.Join(", ", words));

        // ---------- Task 4 ----------
        Func<List<string>, string> findLongest = list =>
            list.OrderByDescending(w => w.Length).FirstOrDefault() ?? "";

        string longest = findLongest(words);

        // ---------- Task 5 ----------
        Console.WriteLine("\n[Task 5] Longest word (via Action):");
        printMessage("  Longest word: " + longest);

        // ---------- Task 6 ----------
        Func<List<int>, Func<int, bool>, List<int>> filterNumbers =
            (list, predicate) => list.Where(predicate).ToList();

        Func<int, bool> greaterThanSeven = n => n > 7;

        List<int> sourceNumbers = rangeGen(1, 20);
        List<int> filtered = filterNumbers(sourceNumbers, greaterThanSeven);

        Console.WriteLine("\n[Task 6] Numbers > 7 from range 1..20:");
        Console.WriteLine("  " + string.Join(", ", filtered));

        // ---------- Task 7 ----------
        Func<Func<List<int>, Func<int, bool>, List<int>>, bool, List<int>> pickEvenOrOdd =
            (filterFunc, isEven) =>
            {
                Func<int, bool> condition = isEven
                    ? n => n % 2 == 0
                    : n => n % 2 != 0;
                return filterFunc(sourceNumbers, condition);
            };

        List<int> evenNumbers = pickEvenOrOdd(filterNumbers, true);
        List<int> oddNumbers = pickEvenOrOdd(filterNumbers, false);

        Console.WriteLine("\n[Task 7] Even numbers from range 1..20:");
        Console.WriteLine("  " + string.Join(", ", evenNumbers));

        Console.WriteLine("\n[Task 7] Odd numbers from range 1..20:");
        Console.WriteLine("  " + string.Join(", ", oddNumbers));
    }
}