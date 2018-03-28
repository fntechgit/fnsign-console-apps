using schedInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fnsignTimewarpConsole
{
    internal class Program
    {
        private static void Main(string[] args)
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
    }
}
