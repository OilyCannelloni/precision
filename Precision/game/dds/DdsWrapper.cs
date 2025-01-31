using System.Runtime.InteropServices;
using Precision.game.dds.models;
using Precision.game.elements.cards;

namespace Precision.game.dds;



[StructLayout(LayoutKind.Sequential)]
public struct DdsFutureTricks
{
    public int nNodes;
    public int nCards;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
    public DdsSuit[] suit;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
    public int[] rank;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
    public CardValue[] equals;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
    public int[] score;
}

[StructLayout(LayoutKind.Sequential)]
public struct DdsDealPbn
{
    public int trump;
    public int first;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public int[] currentTrickSuit;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public int[] currentTrickRank;
    
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
    public string remainCards;
}

[StructLayout(LayoutKind.Sequential)]
public struct DdsDeal
{
    [MarshalAs(UnmanagedType.I4)]
    public DdsSuit Trump;
    
    [MarshalAs(UnmanagedType.I4)]
    public DdsPosition TrickDealer;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public int[] CurrentTrickSuit;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public int[] CurrentTrickRank;
    
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public int[,] RemainingCards;
}


public static class DdsWrapper
{
    [DllImport("lib/dds.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern int CalcDDtablePBN(char[] tableDealPBN, IntPtr tablep);

    [DllImport("lib/dds.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ErrorMessage(int code, IntPtr message);
    
    [DllImport("lib/dds.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SolveBoard(ref DdsDeal ddsDeal, int target, int solutions, int mode, 
        ref DdsFutureTricks futureTricks, int thread);

    [DllImport("lib/dds.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int SolveBoardPBN(ref DdsDealPbn ddsDealPbn, int target, int solutions, int mode, 
        ref DdsFutureTricks futureTricks, int thread);
}