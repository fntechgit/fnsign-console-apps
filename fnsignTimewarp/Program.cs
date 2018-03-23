// Decompiled with JetBrains decompiler
// Type: fnsignTimewarp.Program
// Assembly: fnsignTimewarp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C9A386C-1F2A-4302-B954-7649C97982B5
// Assembly location: D:\Development\Tipit\FNTech\console\console\fnsignTimewarp\fnsignTimewarp.exe

using System.ServiceProcess;

namespace fnsignTimewarp
{
  internal static class Program
  {
    private static void Main()
    {
      ServiceBase.Run(new ServiceBase[1]
      {
        (ServiceBase) new Service1()
      });
    }
  }
}
