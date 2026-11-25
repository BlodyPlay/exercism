using System;
using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        var list = dominoes.ToList();

        if (list.Count == 0)
        {
            return true;
        }

        var used = new bool[list.Count];
        used[0] = true;

        var first = list[0];
        return BuildChain(list, used, 1, first.Item1, first.Item2);
    }

    private static bool BuildChain(
        List<(int, int)> dominoes,
        bool[] used,
        int count,
        int startValue,
        int currentEnd)
    {
        if (count == dominoes.Count)
        {
            return startValue == currentEnd;
        }

        for (int i = 0; i < dominoes.Count; i++)
        {
            if (used[i])
            {
                continue;
            }

            var (left, right) = dominoes[i];

            if (left == currentEnd)
            {
                used[i] = true;
                if (BuildChain(dominoes, used, count + 1, startValue, right))
                {
                    return true;
                }
                used[i] = false;
            }
            else if (right == currentEnd)
            {
                used[i] = true;
                if (BuildChain(dominoes, used, count + 1, startValue, left))
                {
                    return true;
                }
                used[i] = false;
            }
        }

        return false;
    }
}