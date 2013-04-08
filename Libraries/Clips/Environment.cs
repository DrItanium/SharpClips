using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Libraries.Clips
{
  public class ClipsEnvironment
  {
    [DllImport("libclips.so")]
      private static extern IntPtr CreateEnvironment();
    [DllImport("libclips.so")]
      private static extern void DestroyEnvironment(IntPtr envPointer);
    [DllImport("libclips.so")]
      private static extern IntPtr EnvAssertString(IntPtr envPointer, 
          [MarshalAs(UnmanagedType.LPStr)]
          string message);
    [DllImport("libclips.so")]
      private static extern IntPtr EnvMakeInstance(IntPtr envPointer, 
          [MarshalAs(UnmanagedType.LPStr)]
          string instance);
    [DllImport("libclips.so")]
      private static extern int EnvEval(IntPtr envPointer,
          [MarshalAs(UnmanagedType.LPStr)]
          string statement, 
          IntPtr dataObjectPtr);
    private static int EnvEval(IntPtr envPointer, string statement, DataObject dObject)
    {
        int size = Marshal.SizeOf(typeof(DATA_OBJECT));
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(dObject.NativeReference, ptr, false);
        int result = EnvEval(envPointer, statement, ptr);
        Marshal.FreeHGlobal(ptr);
        ptr = IntPtr.Zero;
        return result;
    }
    [DllImport("libclips.so")]
      private static extern int EnvBuild(IntPtr envPointer,
          [MarshalAs(UnmanagedType.LPStr)]
          string statement);
    private IntPtr environmentPointer;
    public ClipsEnvironment()
    {
      environmentPointer = CreateEnvironment();
    }
    ~ClipsEnvironment() 
    {
      DestroyEnvironment(environmentPointer);
    }
    public void AssertString(string fact)
    {
      EnvAssertString(environmentPointer, fact);
    }
    public void MakeInstance(string instance) 
    {
      EnvMakeInstance(environmentPointer, instance);
    }
    public bool Eval(string statement)
    {
      DataObject tmp = new DataObject();
      return Eval(statement, tmp);
    }
    public bool Eval(string statement, DataObject dObject)
    {
      return EnvEval(environmentPointer, statement, dObject) != 0;
    }
    public bool Build(string statement)
    {
      return EnvBuild(environmentPointer, statement) != 0;
    }
  }
}
