using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;
using System.Text;

namespace AgentWatchFace1
{
    public class Program
    {
        static Bitmap _display;
        static Timer _updateClockTimer;

        public static void Main()
        {
            // initialize our display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            // display the time immediately
            UpdateTime(null);

            // obtain the current time
            DateTime currentTime = DateTime.Now;
            // set up timer to refresh time every minute
            TimeSpan dueTime = new TimeSpan(0, 0, 0, 0, 1000 - currentTime.Millisecond); // start timer at beginning of next second
            TimeSpan period = new TimeSpan(0, 0, 0, 1, 0); // update time every second
            _updateClockTimer = new Timer(UpdateTime, null, dueTime, period); // start our update timer

            // go to sleep; time updates will happen automatically every minute
            Thread.Sleep(Timeout.Infinite);
        }

        static void UpdateTime(object state)
        {
            // obtain the current time
            DateTime currentTime = DateTime.Now;
            // clear our display buffer
            _display.Clear();

            // add your watchface drawing code here
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            var h = ToBinary(currentTime.Hour);
            var m = ToBinary(currentTime.Minute);
            var s = ToBinary(currentTime.Second);
            _display.DrawText(h, fontNinaB, Color.White, 46, 38);
            _display.DrawText(m, fontNinaB, Color.White, 46, 58);
            _display.DrawText(s, fontNinaB, Color.White, 46, 78);

            // flush the display buffer to the display
            _display.Flush();
        }

        static string ToBinary(int number)
        {
            int remainder;
            string result = string.Empty;
            while (number > 0)
            {
                remainder = number % 2;
                number /= 2;
                result = remainder.ToString() + result;
            }
            return PadLeft(result, 6);
        }

        static string PadLeft(string s, int nCharsInTotal)
        {
            var padLength = nCharsInTotal - s.Length;
            var padding = new String('0', padLength);
            var padded = new StringBuilder(padding);
            padded.Append(s);
            return padded.ToString();
        }

    }
}
