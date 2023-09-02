using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class Alerts
    {
        private List<(Stopwatch stopwatch, string alert)> alerts = new List<(Stopwatch stopwatch, string alert)>();
        public List<string> GetAlerts()
        {
            List<(Stopwatch stopwatch, string alert)> alertToRemove = new List<(Stopwatch stopwatch, string alert)> ();
            foreach (var item in alerts)
            {
                if(item.stopwatch.ElapsedMilliseconds > 2000)
                {
                    alertToRemove.Add (item);
                }
            }
            foreach (var item in alertToRemove)
            { alerts.Remove (item); }
            List<String> result = new List<String>();
            foreach(var item in alerts)
            {
                result.Add(item.alert);
            }
            return result;
        }
        public void Add(string alert)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            alerts.Add((stopwatch, alert));
        }
        public void Clear()
        {
            alerts.Clear();
        }
    }
}