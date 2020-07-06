using System;
namespace inventory
{
    public class PodFactoryCalculator
    {
        // All Factors are in munutes
        private static double COST_FACTOR = 1.8;
        private static double MASS_FACTOR = 360;
        private static double CRASH_TOLERANCE_FACTOR = 25.2;
        private static double TEMP_FACTOR = 0.36;
        private static double CREW_CAPACITY_FACTOR = 2160;
        private static double CHARGE_RATE_COMMAND_FACTOR = 14400;
        private static double HIBERNATION_COMMAND_FACTOR = 3600;
        private static double ELECTRIC_CHARGE_FACTOR = 7.2;
        private static double PROBE_CONTROL_POINT_FACTOR = 360;
        private static double TORKE_FACTOR = 25.2;
        private static double CHARGE_RATE_REACTION_WHEEL_FACTOR = 360;
        private static double SAS_LEVEL_FACTOR = 1800;
        private static double SCIENCE_CONTAINER_FACTOR = 1080;
        private static double SCIENCE_EXPERIMENT_FACTOR = 1800;
        private static double MONOPROPELANT_FACTOR = 72;
        private static double SEAT_FACTOR = 2520;

        private static double MASS_BASE = 1800;
        private static double MIN_CREW_COMMAND_BASE = 1800;
        private static double CHARGE_RATE_COMMAND_BASE = 2160;
        private static double HIBERNATION_COMMAND_BASE = 3600;
        private static double KERB_NET_ACCESS_BASE = 1080;
        private static double DATA_TRANSMITTER_MODULE_BASE = 360;
        private static double PROBE_CONTROL_POINT_MODULE_BASE = 2160;
        private static double CHARGE_RATE_REACTION_WHEEL_BASE = 360;

        public static double calculate(AvailablePart part)
        {
            double sum = calculateBasic(part);
            sum += calculateCommandModule(part);
            sum += calculateKerbNetAccessModule(part);
            sum += calculateDataTransmitterModule(part);
            sum += calculateProbeControlPointModule(part);
            sum += calculateReactionWheelModule(part);
            sum += calculateSASModule(part);
            sum += calculateScienceContainerModule(part);
            sum += calculateScienceExperimentModule(part);
            sum += calculateSeatModule(part);
            return sum;
        }

        private static double calculateBasic(AvailablePart part)
        {
            Double mass = PartConfigLoader.GetProperty(part, PartConfigLoader.MASS);
            Double cost = part.cost;
            Double crashTolerance = PartConfigLoader.GetProperty(part, PartConfigLoader.CRASH_TOLERANCE);
            Double maxTemp = PartConfigLoader.GetProperty(part, PartConfigLoader.TEMP);
            Double capacity = PartConfigLoader.GetProperty(part, PartConfigLoader.CREW_CAPACITY);
            ConfigNode electricResource = PartConfigLoader.GetResource(part, PartConfigLoader.ELECTRIC_CHARGE);
            Double electricCharge = PartConfigLoader.GetProperty(electricResource, PartConfigLoader.AMOUNT);
            ConfigNode monopropelantResource = PartConfigLoader.GetResource(part, PartConfigLoader.MONOPROPELANT);
            Double monopropelant = monopropelantResource != null ? PartConfigLoader.GetProperty(monopropelantResource, PartConfigLoader.AMOUNT) : 0;

            double sum = cost * COST_FACTOR;
            sum+= MASS_BASE - mass * MASS_FACTOR;
            sum+= crashTolerance * CRASH_TOLERANCE_FACTOR;
            sum+= maxTemp * TEMP_FACTOR;
            sum+= capacity * CREW_CAPACITY_FACTOR;
            sum += electricCharge * ELECTRIC_CHARGE_FACTOR;
            sum += monopropelant * MONOPROPELANT_FACTOR;

            Log.log("Basic: " + sum);

            return sum;
        }

        private static double calculateCommandModule(AvailablePart part)
        {
            ConfigNode commandModule = PartConfigLoader.GetCommandModule(part);
            Log.log("Command Module: " + commandModule);
            if (commandModule == null) return 0;
            Double minCrew = PartConfigLoader.GetProperty(commandModule, PartConfigLoader.MINUMUN_CREW);
            Log.log("Minimun Crew: " + minCrew);
            bool hasHibernation = PartConfigLoader.GetBooleanProperty(commandModule, PartConfigLoader.HAS_HIBERNATION);
            Log.log("Has Hibernation: " + hasHibernation);
            bool hasHibernationMultiplier = PartConfigLoader.HasProperty(commandModule, PartConfigLoader.HIBERNATION_MULTIPLIER);
            Log.log("Has Hibernation Multiplier: " + hasHibernationMultiplier);
            ConfigNode chargeResource = PartConfigLoader.GetResource(commandModule, PartConfigLoader.ELECTRIC_CHARGE);
            Double chargeRate = chargeResource != null ? PartConfigLoader.GetProperty(chargeResource, PartConfigLoader.RATE) : 1;

            double sum = 0;

            if (minCrew < 1) sum += MIN_CREW_COMMAND_BASE;

            if (!hasHibernation) return sum;

            sum += CHARGE_RATE_COMMAND_BASE - CHARGE_RATE_COMMAND_FACTOR * chargeRate;

            double hibernationMultiplier = hasHibernationMultiplier ? PartConfigLoader.GetProperty(commandModule, PartConfigLoader.HIBERNATION_MULTIPLIER) : 0.5;

            sum += HIBERNATION_COMMAND_BASE - HIBERNATION_COMMAND_FACTOR * hibernationMultiplier;

            Log.log("Command: " + sum);
            return sum;
        }

        private static double calculateKerbNetAccessModule(AvailablePart part)
        {
            ConfigNode module = PartConfigLoader.GetKerbNetAccessModule(part);
            double sum = module == null ? 0 : KERB_NET_ACCESS_BASE;
            Log.log("Kerb net Access: " + sum);
            return sum;
        }

        private static double calculateDataTransmitterModule(AvailablePart part)
        {
            ConfigNode module = PartConfigLoader.GetDataTransmitterModule(part);
            double sum = module == null ? 0 : DATA_TRANSMITTER_MODULE_BASE;
            Log.log("Data Transmitter: " + sum);
            return sum;
        }

        private static double calculateProbeControlPointModule(AvailablePart part)
        {
            ConfigNode module = PartConfigLoader.GetProbeControlPointModule(part);
            if (module == null) return 0;
            double sum = PROBE_CONTROL_POINT_MODULE_BASE - PROBE_CONTROL_POINT_FACTOR * PartConfigLoader.GetProperty(module, PartConfigLoader.MINUMUN_CREW);
            Log.log("Probe control point: " + sum);
            return sum;
        }

        private static double calculateReactionWheelModule(AvailablePart part)
        {
            ConfigNode module = PartConfigLoader.GetReactionWheelModule(part);
            if (module == null) return 0;

            Double torke = PartConfigLoader.GetProperty(module, PartConfigLoader.TORKE);
            ConfigNode electric = PartConfigLoader.GetResource(module, PartConfigLoader.ELECTRIC_CHARGE);
            Double electricCharge = electric == null ? 0 : PartConfigLoader.GetProperty(electric, PartConfigLoader.RATE);

            Double sum = TORKE_FACTOR * torke;
            sum += electricCharge > 0 ? CHARGE_RATE_REACTION_WHEEL_BASE - CHARGE_RATE_REACTION_WHEEL_FACTOR * electricCharge : 0;
            Log.log("ReactionWheel: " + sum);
            return sum;

        }

        private static double calculateSASModule(AvailablePart part)
        {
            ConfigNode module = PartConfigLoader.GetSASModule(part);
            if (module == null) return 0;
            double sum = SAS_LEVEL_FACTOR * PartConfigLoader.GetProperty(module, PartConfigLoader.SAS_LEVEL);
            Log.log("SAS: " + sum);
            return sum;
        }

        private static double calculateScienceContainerModule(AvailablePart part)
        {
            ConfigNode module = PartConfigLoader.GetScienceContainerModule(part);
            if (module == null) return 0;
            double sum = SCIENCE_CONTAINER_FACTOR * PartConfigLoader.GetProperty(module, PartConfigLoader.STORAGE_RANGE);
            Log.log("Science container: " + sum);
            return sum;
        }

        private static double calculateScienceExperimentModule(AvailablePart part)
        {
            ConfigNode module = PartConfigLoader.GetScienceExperimentModule(part);
            double sum = module == null ? 0 : SCIENCE_EXPERIMENT_FACTOR;
            Log.log("Science Experiment: " + sum);
            return sum;
        }

        private static double calculateSeatModule(AvailablePart part)
        {
            ConfigNode module = PartConfigLoader.GetSeatModule(part);
            double sum = module == null ? 0 : SEAT_FACTOR;
            Log.log("Seat: " + sum);
            return sum;
        }
    }
}
