using System;
using System.Collections.Generic;

public static class VariableLengthQuantity
{
    private const uint SevenBitsMask = 0x7Fu;
    private const uint EighthBitMask = 0x80u;

    public static uint[] Encode(uint[] numbers)
    {
        var result = new List<uint>();

        foreach (uint number in numbers)
        {
            var chunks = new List<uint>();
            uint current = number;

            chunks.Add(current & SevenBitsMask);
            current >>= 7;

            while (current > 0)
            {
                chunks.Add((current & SevenBitsMask) | EighthBitMask);
                current >>= 7;
            }

            chunks.Reverse();
            result.AddRange(chunks);
        }

        return result.ToArray();
    }

    public static uint[] Decode(uint[] bytes)
    {
        var result = new List<uint>();
        uint current = 0;
        bool inSequence = false;

        foreach (uint b in bytes)
        {
            current = (current << 7) | (b & SevenBitsMask);

            if ((b & EighthBitMask) != 0)
            {
                inSequence = true;
            }
            else
            {
                result.Add(current);
                current = 0;
                inSequence = false;
            }
        }

        if (inSequence)
        {
            throw new InvalidOperationException("Incomplete byte sequence.");
        }

        return result.ToArray();
    }
}