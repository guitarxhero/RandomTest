﻿/// <summary>
/// Quickly written by pt300 and modified afterwards by guitarxhero.
/// </summary>
public class PCGRandom
{
    static ulong[] INITIALIZER =
    {
        0x853c49e6748fea9bUL, 0xda3e39cb94b95bdbUL
    };

    private ulong state;
    private ulong inc;

    public PCGRandom() : this(INITIALIZER[0], INITIALIZER[1]) { }

    public PCGRandom(ulong seed, ulong seq)
    {
        state = 0U;
        inc = (seq << 1) | 1;
        pcg32_random();
        state += seed;
        pcg32_random();
    }

    public uint pcg32_random()
    {
        ulong oldstate = state;
        state = oldstate * 6364136223846793005ul + inc;
        uint xorshifted = (uint)((oldstate >> 18) ^ oldstate) >> 27;
        uint rot = (uint)(oldstate >> 59);
        return (xorshifted >> (int)rot) | (xorshifted << ((-(int)rot) & 31));
    }

    public int Next(uint MaxValue)
    {
        uint threshold = (uint)((0 - (ulong)MaxValue) % (ulong)MaxValue);
        for (;;)
        {
            uint r = pcg32_random();
            if (r >= threshold)
                return (int)(r % MaxValue);
        }
    }
}