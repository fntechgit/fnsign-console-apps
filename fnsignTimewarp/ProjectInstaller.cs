// Decompiled with JetBrains decompiler
// Type: fnsignTimewarp.ProjectInstaller
// Assembly: fnsignTimewarp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C9A386C-1F2A-4302-B954-7649C97982B5
// Assembly location: D:\Development\Tipit\FNTech\console\console\fnsignTimewarp\fnsignTimewarp.exe

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace fnsignTimewarp
{
  [RunInstaller(true)]
  public class ProjectInstaller : Installer
  {
    private IContainer components = (IContainer) null;
    private ServiceProcessInstaller serviceProcessInstaller1;
    private ServiceInstaller serviceInstaller1;

    public ProjectInstaller()
    {
      this.InitializeComponent();
    }

    private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.serviceProcessInstaller1 = new ServiceProcessInstaller();
      this.serviceInstaller1 = new ServiceInstaller();
      this.serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
      this.serviceProcessInstaller1.Password = (string) null;
      this.serviceProcessInstaller1.Username = (string) null;
      this.serviceProcessInstaller1.AfterInstall += new InstallEventHandler(this.serviceProcessInstaller1_AfterInstall);
      this.serviceInstaller1.Description = "Updates the correct time based off of the starting time entered into FNSIGN.";
      this.serviceInstaller1.DisplayName = "FNSIGN Timewarp Service";
      this.serviceInstaller1.ServiceName = "Service1";
      this.Installers.AddRange(new Installer[2]
      {
        (Installer) this.serviceProcessInstaller1,
        (Installer) this.serviceInstaller1
      });
    }
  }
}
