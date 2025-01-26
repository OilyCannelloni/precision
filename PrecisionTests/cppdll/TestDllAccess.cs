using System.Runtime.InteropServices;
using NUnit.Framework;
using Precision.game;

namespace PrecisionTests.cppdll;

public class TestDllAccess
{
    [Test]
    public void ErrorCode()
    {
        Console.WriteLine("start");
        
        var messageHandle = GCHandle.Alloc(new char[80], GCHandleType.Pinned);
        var messagePtr = messageHandle.AddrOfPinnedObject(); 
        DdsService.ErrorMessage(-8, messagePtr);
    
        var x = Marshal.PtrToStringAnsi(messagePtr);
        Console.WriteLine(x);

        messageHandle.Free();
    }
    
    [Test]
    public void DdTable()
    {
        var dealStr = "W:T.KQ985.86.A9752 K3.AT7.QJT9753.3 AJ842.4.AK.KJ864 Q9765.J632.42.QT\0";
        var deal = new char[80];
        for (var i = 0; i < dealStr.Length; i++)
        {
            deal[i] = dealStr[i];
        }
        
        Console.WriteLine(deal);

        
        var resultHandle = GCHandle.Alloc(new int[20], GCHandleType.Pinned);
        var ptr = resultHandle.AddrOfPinnedObject();

        DdsService.CalcDDtablePBN(deal, ptr);

        var ret = new int[20];
        Marshal.Copy(ptr, ret, 0, 20);
        Console.WriteLine(string.Join(", ", ret));
        
        resultHandle.Free();
    }

    [Test]
    public void SolveBoard()
    {
        
    }
    
}