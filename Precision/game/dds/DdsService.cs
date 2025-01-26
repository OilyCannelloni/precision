namespace Precision.game;


using System.Runtime.InteropServices;

public struct DdsDeal
{
    public int Trump;
    public int First;
    public int[] CurrentTrickSuit;
    public int[] CurrentTrickRank;
    public int[] RemaingCards;
}


public class DdsService
{
    [DllImport("lib/dds.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CalcDDtablePBN(char[] tableDealPBN, IntPtr tablep);

    [DllImport("lib/dds.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe int ErrorMessage(int code, IntPtr message);
}