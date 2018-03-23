// Decompiled with JetBrains decompiler
// Type: fnsignTimewarp.Service1
// Assembly: fnsignTimewarp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C9A386C-1F2A-4302-B954-7649C97982B5
// Assembly location: D:\Development\Tipit\FNTech\console\console\fnsignTimewarp\fnsignTimewarp.exe

using schedInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceProcess;
using System.Timers;

namespace fnsignTimewarp
{
  public class Service1 : ServiceBase
  {
    private Timer timer = new Timer();
    private IContainer components = (IContainer) null;

    public Service1()
    {
      this.InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      this.timer.Elapsed += new ElapsedEventHandler(this.OnElapsedTime);
      this.timer.Interval = 60000.0;
      this.timer.Enabled = true;
      this.EventLog.WriteEntry("FNSIGN Timewarp Service Started...");
    }

    private void OnElapsedTime(object source, ElapsedEventArgs e)
    {
        try
        {
            this.updateTimes();
        }
        catch (Exception ex)
        {
            this.EventLog.WriteEntry("FNSIGN Timewarp Service Error: " + ex.Message + ex.ToString());      
        }
      
      this.EventLog.WriteEntry("FNSIGN Timewarp Service Processing...");
    }

    protected override void OnStop()
    {
    }

    private void updateTimes()
    {
      timewarp _timewarp = new timewarp();
      events _events = new events();

      foreach (Event @event in _events.all())
      {
        DateTime? nullable;
        int num;
        if (@event.timerun)
        {
          nullable = @event.timewarp;
          num = nullable.HasValue ? 1 : 0;
        }
        else
          num = 0;
        if (num != 0)
        {
            nullable = @event.last_timerun;
            var dif = (DateTime.Now - Convert.ToDateTime((object)@event.last_timerun));
            int minutes = !nullable.HasValue ? 1 : (dif.Minutes == 0 ? 1 : dif.Minutes);
            _timewarp.runtime(@event.id, minutes);
        }
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ServiceName = "FNSIGN Timewarp";
    }
  }
}
