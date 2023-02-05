using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;

namespace LanguageSchool
{
    public partial class Service
    {
        // конвертим в минуты
        public float Duration 
        {

            get
            {
                float time = DurationInSeconds / 60;
                return time;
            }


        }
       

        public SolidColorBrush colorBrush
        {
            get
            {
                if(Discount != null)
                {
                    SolidColorBrush colorBrushDiscountDone = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                    return colorBrushDiscountDone;

                }
                else
                {
                    SolidColorBrush colorBrushNotDiscount = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    return colorBrushNotDiscount;
                }
              
            }
        } 
         public double ActualPrice
        {
            get
            {
              
                if (Discount != null)
                {

                    double ammount = Convert.ToDouble(Cost) - Convert.ToDouble(Cost) / 100 *    Convert.ToDouble(Discount) ;
                    return ammount;
                }
                else
                {
                    return Convert.ToDouble(Cost);
                }
            
            }
        }

    }
}
