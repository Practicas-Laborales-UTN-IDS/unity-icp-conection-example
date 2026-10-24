
using System;

public sealed class SimonRandom
{
    private uint state;
    private uint packed;
    private int remainingColors;

    public SimonRandom(ulong seed)
    {
        state = (uint)seed ^ (uint)(seed >> 32);

        // Xorshift32 no admite estado cero.
        if (state == 0)
            state = 0x6D2B79F5u;

        packed = 0;
        remainingColors = 0;
    }

    private uint NextUInt()
    {
        uint x = state;
        x ^= x << 13;
        x ^= x >> 17;
        x ^= x << 5;
        state = x;
        return x;
    }

    public int NextColor()
    {
        if (remainingColors == 0)
        {
            packed = NextUInt();
            remainingColors = 16;
        }

        int color = (int)(packed & 3u);
        packed >>= 2;
        remainingColors--;

        return color;
    }
}
