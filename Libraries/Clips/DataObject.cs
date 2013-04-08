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
  [StructLayout(LayoutKind.Sequential)]
    public struct DATA_OBJECT 
    {
      IntPtr supplementalInfo;
      ushort type;
      IntPtr value;
      long begin;
      long end;
      IntPtr next;
    }
  public class DataObject 
  {
    private DATA_OBJECT backingStore;
    public DATA_OBJECT NativeReference { get { return backingStore; } }
    public DataObject(DATA_OBJECT obj)
    {
      backingStore = obj;
    }
    public DataObject() { }
  }
}
