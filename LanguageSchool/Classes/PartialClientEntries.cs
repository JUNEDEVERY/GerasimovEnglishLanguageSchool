using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LanguageSchool
{
    public partial class ClientService
    {
        public SolidColorBrush solidColorBrush // если осталось до начала сеанса меньше часа красим в красный
        {
            get
            {
                int hours = 1;

                TimeSpan dateTime = StartTime - DateTime.Now;
                TimeSpan timeHours = TimeSpan.FromHours(hours);
                if (dateTime < timeHours)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.Black;
                }

            }
        }
        public string TimeLeft
        {
            get
            {
               
                

                TimeSpan dateTime = StartTime - DateTime.Now;
                int hours = 0;
                int minute = 0;
                hours += Convert.ToInt32(dateTime.ToString("hh"));
                minute = Convert.ToInt32(dateTime.ToString("mm"));
                if(StartTime.Date == DateTime.Now.Date)
                {
                    return dateTime.Hours + " ч. " + dateTime.Minutes + " мин.";
                }
                else
                {
                    if(dateTime.Days > 0)
                    {
                        hours += Convert.ToInt32(dateTime.Days) * 24;

                    }
                    return hours + " ч. " + minute + " м.";
                }
            }
        }
    }
}
