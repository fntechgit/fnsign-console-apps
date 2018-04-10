// Decompiled with JetBrains decompiler
// Type: fnsign_updater.Program
// Assembly: fnsign_updater, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B4A7353-9C3D-47CE-B61E-39E1659151FD
// Assembly location: D:\Development\Tipit\FNTech\console\console\fnsignUpdate\fnsignUpdate\fnsign_updater.exe

using schedInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fnsign_updater
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      events events = new events();
      twitter twitter = new twitter();
      templates templates = new templates();
      terminals terminals = new terminals();
      Console.WriteLine("######### BEGIN FNSIGN UPDATER v.1.0 #########");
      Console.WriteLine("");
      Console.WriteLine("######### GETTING EVENTS THAT NEED TO BE UPDATED #########");
      Console.WriteLine("");
      List<Event> eventList = events.need_updating()
          .Where<Event>((Func<Event, bool>)(x => !string.IsNullOrEmpty(x.api_key) && !string.IsNullOrEmpty(x.url)))
          .ToList<Event>();
      Console.WriteLine(eventList.Count.ToString() + " need updating....");
      Console.WriteLine("");
      sessions sessions = new sessions();
      foreach (Event ev in eventList)
      {
        Console.WriteLine("Finding Sessions for " + ev.title);
        Console.WriteLine("");
        List<Session> sessionList = sessions.getSessionsFromAPI(ev.url, ev.api_key, ev.api_type);
        Console.WriteLine(sessionList.Count.ToString() + " Sessions Found...");
        Console.WriteLine("");
        foreach (Session s in sessionList)
          sessions.add(s, ev.id);

        //Remove those sessions which stopped coming from api
        IList<string> incomingSessionIds = sessionList.Select(x => x.event_key).ToList();
        List<Session> notComingAnymoreSessions = sessions.by_event(ev.id).Where(x => !incomingSessionIds.Contains(x.event_key)).ToList<Session>();
        foreach (var item in notComingAnymoreSessions)
        {
            sessions.delete(int.Parse(item.id));
        }

        Console.WriteLine("");
        Console.WriteLine("Finding Tweets for Global Event Tag...");
        Console.WriteLine("");
        if (!string.IsNullOrEmpty(ev.t_username))
        {
          Console.WriteLine("Finding Tweets for @" + ev.t_username);
          Console.WriteLine("");
          twitter.fetch(ev.t_username, 50, true, ev.id, 0);
        }
        if (ev.hashtags != null)
        {
          foreach (string hashtag in ev.hashtags)
          {
            Console.WriteLine("Finding Tweets for #" + hashtag);
            Console.WriteLine("");
            twitter.fetch(hashtag, 50, ev.id, 0);
          }
        }
        Console.WriteLine("Now let's check for the templates associated with " + ev.title);
        Console.WriteLine("");
        foreach (Template template in templates.all_by_event(ev.id))
        {
          if (!string.IsNullOrEmpty(template.t_username))
          {
            Console.WriteLine("Fetching Twitter records for @" + template.t_username);
            Console.WriteLine("");
            twitter.fetch(template.t_username, 50, true, ev.id, template.id);
          }
          if (template.hashtags != null)
          {
            Console.WriteLine("Fetching Records for Hashtags...");
            Console.WriteLine("");
            foreach (string hashtag in template.hashtags)
            {
              Console.WriteLine("Fetching tweets for #" + hashtag);
              Console.WriteLine("");
              twitter.fetch(hashtag, 50, ev.id, template.id);
            }
          }
        }
        Console.WriteLine("");
        Console.WriteLine("Updating Event Last Updated Time...");
        events.update(ev);
        Console.WriteLine("Complete...");
        Console.WriteLine("Moving to next event...");
        Console.WriteLine("");
      }
      Console.WriteLine("Check for Offline Terminals...");
      Console.WriteLine("");
      List<Terminal> source = terminals.offline_terminals();
      if (source.Count<Terminal>() > 0)
      {
        Console.WriteLine(source.Count<Terminal>().ToString() + " Offline, updating their status...");
        Console.WriteLine("");
        foreach (Terminal terminal in source)
        {
          Console.WriteLine("Taking " + terminal.title + " Offline");
          Console.WriteLine("");
          terminals.offline(terminal.id);
        }
      }
      else
      {
        Console.WriteLine("All Terminals are Online...");
        Console.WriteLine("");
      }
      Console.WriteLine("######### FNSIGN v.1.0 UPDATER COMPLETE #########");
    }
  }
}
