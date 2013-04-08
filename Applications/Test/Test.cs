using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Linq;
using Libraries.Clips;

public static class Program 
{
  public static void Main(string[] args)
  {
    ClipsEnvironment env = new ClipsEnvironment();
    env.AssertString("(I am fucking awesome)");
    env.Eval("(options)");
  }
}

