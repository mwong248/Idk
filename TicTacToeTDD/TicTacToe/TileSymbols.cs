namespace TicTacToe;

public enum SymbolEnum { X, O, Empty, }

public static class TileSymbols
{
    private static Dictionary<SymbolEnum, string> EnumStringMap { get; } = new()
    {
        { SymbolEnum.X, "X" },
        { SymbolEnum.O, "O" },
        { SymbolEnum.Empty, " " }
    };

    public static string GetSymbolString(this SymbolEnum symbolEnum)
    {
        return EnumStringMap[symbolEnum];
    }
    
    public static SymbolEnum GetSymbolEnum(string symbolString)
    {
        return EnumStringMap.FirstOrDefault(x => x.Value == symbolString).Key;
    }
}