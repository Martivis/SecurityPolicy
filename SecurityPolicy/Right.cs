namespace SecurityPolicy;

[Flags]
public enum Right
{
    None = 0b000,
    Read = 0b001,
    Write = 0b010,
    Grant = 0b100,
}