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
      private static extern IntPtr EnvEval(IntPtr envPointer,
          [MarshalAs(UnmanagedType.LPStr)]
          string statement);
    [DllImport("libclips.so")]
      private static extern IntPtr EnvBuild(IntPtr envPointer,
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
    public void Eval(string statement)
    {
      EnvEval(environmentPointer, statement);
    }
    public void Build(string statement)
    {
      EnvBuild(environmentPointer, statement);
    }
  }
}
