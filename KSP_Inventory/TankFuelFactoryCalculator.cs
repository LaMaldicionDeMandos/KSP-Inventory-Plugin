using System;
namespace inventory
{
    public class TankFuelFactoryCalculator
    {

        private static double COST_FACTOR = 0.24;
        private static double MASS_FACTOR = 360;
        private static double CRASH_TOLERANCE_FACTOR = 25.2;
        private static double TEMP_FACTOR = 0.36;
        private static double LIQUID_FUEL_FACTOR = 0.53;
        private static double OXIDER_FACTOR = 0.44;
        private static double MONOPROPELANT_FACTOR = 7.2;
        private static double ORE_FACTOR = 0.72;
        private static double XENON_FACTOR = 0.72;

        private static double MASS_BASE = 3600;
        private static double ORE_BASE = 1080;
        private static double XENON_BASE = 1800;

        public static double calculate(AvailablePart part)
        {
            double sum = calculateBasic(part);
            sum += calculateLiquidFuel(part);
            sum += calculateMonopropelant(part);
            sum += calculateOre(part);

            return sum;
        }

        private static double calculateBasic(AvailablePart part)
        {
            Double mass = PartConfigLoader.GetProperty(part, PartConfigLoader.MASS);
            Double cost = part.cost;
            Double crashTolerance = PartConfigLoader.GetProperty(part, PartConfigLoader.CRASH_TOLERANCE);
            Double maxTemp = PartConfigLoader.GetProperty(part, PartConfigLoader.TEMP);

            double sum = cost * COST_FACTOR;
            sum += MASS_BASE - mass * MASS_FACTOR;
            sum += crashTolerance * CRASH_TOLERANCE_FACTOR;
            sum += maxTemp * TEMP_FACTOR;

            Log.log("Basic: " + sum);

            return sum;
        }

        private static double calculateLiquidFuel(AvailablePart part)
        {
            ConfigNode liquidFuelResource = PartConfigLoader.GetResource(part, PartConfigLoader.LIQUID_FUEL);
            ConfigNode oxiderResource = PartConfigLoader.GetResource(part, PartConfigLoader.OXIDER);

            double liquidFuel = liquidFuelResource != null ? PartConfigLoader.GetProperty(liquidFuelResource, PartConfigLoader.AMOUNT) : 0;
            double oxider = oxiderResource != null ? PartConfigLoader.GetProperty(oxiderResource, PartConfigLoader.AMOUNT) : 0;

            return liquidFuel * LIQUID_FUEL_FACTOR + oxider * OXIDER_FACTOR;
        }

        private static double calculateMonopropelant(AvailablePart part)
        {
            ConfigNode monopropelantResource = PartConfigLoader.GetResource(part, PartConfigLoader.MONOPROPELANT);

            double monopropelant = monopropelantResource != null ? PartConfigLoader.GetProperty(monopropelantResource, PartConfigLoader.AMOUNT) : 0;

            return monopropelant * MONOPROPELANT_FACTOR;
        }

        private static double calculateOre(AvailablePart part)
        {
            ConfigNode oreResource = PartConfigLoader.GetResource(part, PartConfigLoader.ORE);

            double ore = oreResource != null ? PartConfigLoader.GetProperty(oreResource, PartConfigLoader.MAX_AMOUNT) : 0;

            return oreResource != null ? ORE_BASE + ore * ORE_FACTOR : 0;
        }

        private static double calculateXenon(AvailablePart part)
        {
            ConfigNode xenonResource = PartConfigLoader.GetResource(part, PartConfigLoader.XENON);

            double xenon = xenonResource != null ? PartConfigLoader.GetProperty(xenonResource, PartConfigLoader.AMOUNT) : 0;

            return xenonResource != null ? XENON_BASE + xenon * XENON_FACTOR : 0;
        }
    }
}
