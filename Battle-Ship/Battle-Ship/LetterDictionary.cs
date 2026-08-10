namespace Battle_Ship;
using System.Collections.Generic;

public static class LetterDictionary
{
    public static Dictionary<int, char> RenderLetters = new()
    {
        { 0, 'A' },
        { 1, 'B' },
        { 2, 'C' },
        { 3, 'D' },
        { 4, 'E' },
        { 5, 'F' },
        { 6, 'G' },
        { 7, 'H' },
        { 8, 'I' },
        { 9, 'J' }
    };

    public static Dictionary<char, int> ParsingLetters = new()
    {
        { 'A', 0 },
        { 'B', 1 },
        { 'C', 2 },
        { 'D', 3 },
        { 'E', 4 },
        { 'F', 5 },
        { 'G', 6 },
        { 'H', 7 },
        { 'I', 8 },
        { 'J', 9 },
    };
}