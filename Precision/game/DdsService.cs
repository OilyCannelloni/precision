namespace Precision.game;


using System.Runtime.InteropServices;

public struct DDS_ddTableDealPBN 
{
    public IntPtr cards;
}

public struct DDS_ddTableResults
{
    public IntPtr resTable;
}


public class DdsService
{
    [DllImport("lib/dds.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CalcDDtablePBN(char[] tableDealPBN, IntPtr tablep);

    [DllImport("lib/dds.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern unsafe int ErrorMessage(int code, IntPtr message);
}