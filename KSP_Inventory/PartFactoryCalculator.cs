using System;
namespace inventory
{
    public class PartFactoryCalculator
    {
        public static double calculate(AvailablePart part)
        {
            if (PartCategories.Pods == part.category)
            {
                return PodFactoryCalculator.calculate(part);
            }

            if (PartCategories.FuelTank == part.category)
            {
                return TankFuelFactoryCalculator.calculate(part);
            }
            return 0;
        }

        public static string parse(AvailablePart part)
        {
            return parse(calculate(part));
        }

        public static string parse(double minutes)
        {
            string time = "";
            minutes = Math.Round(minutes);
            if (minutes < 60) return "" + minutes + "m";
            int min = (int)(minutes % 60);
            if (min > 0) time+= min + "m";
            int res = (int)(minutes / 60);
            if (res == 0) return time;
            int hours = res % 6;
            res = (int)(res / 6);
            time = "" + hours + "h " + time;
            if (res == 0) return time;
            int days = res % 365;
            res = (int)(res / 365);
            time = "" + days + "d " + time;
            if (res == 0) return time;
            return "" + res + "y " + time;
        }
    }
}
