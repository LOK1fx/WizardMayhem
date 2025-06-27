using System;

[Flags]
public enum ESpellId : int
{
    None = 0,
    Fire = 1 << 0,
    Wind = 1 << 1,
    Reserved = 1 << 2,
}