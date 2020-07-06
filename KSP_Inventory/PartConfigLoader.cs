using System;
namespace inventory
{
    public class PartConfigLoader
    {
        private static string MODULE = "MODULE";
        private static string RESOURCE = "RESOURCE";

        // Recursos
        public static string ELECTRIC_CHARGE = "ElectricCharge";
        public static string MONOPROPELANT = "MonoPropellant";
        public static string LIQUID_FUEL = "LiquidFuel";
        public static string OXIDER = "Oxidizer";
        public static string ORE = "Ore";
        public static string XENON = "XenonGas";

        // Properties
        public static string MASS = "mass";
        public static string CRASH_TOLERANCE = "crashTolerance";
        public static string TEMP = "maxTemp";
        public static string CREW_CAPACITY = "CrewCapacity";
        public static string MINUMUN_CREW = "minimumCrew";
        public static string HAS_HIBERNATION = "hasHibernation";
        public static string HIBERNATION_MULTIPLIER = "hibernationMultiplier";
        public static string RATE = "rate";
        public static string AMOUNT = "amount";
        public static string MAX_AMOUNT = "maxAmount";
        public static string MIN_CREW = "minimumCrew";
        public static string TORKE = "PitchTorque";
        public static string SAS_LEVEL = "SASServiceLevel";
        public static string STORAGE_RANGE = "storageRange";

        // Modulos
        private static string COMMAND_MODULE = "ModuleCommand";
        private static string KERB_NET_ACCESS_MODULE = "ModuleKerbNetAccess";
        private static string DATA_TRANSMITTER_MODULE = "ModuleDataTransmitter";
        private static string PROBE_CONTROL_POINT_MODULE = "ModuleProbeControlPoint";
        private static string REACTION_WHEEL_MODULE = "ModuleReactionWheel";
        private static string SAS_MODULE = "ModuleSAS";
        private static string SCIENCE_CONTAINER_MODULE = "ModuleScienceContainer";
        private static string SCIENCE_EXPERIMENT_MODULE = "ModuleScienceExperiment";
        private static string SEAT_MODULE = "KerbalSeat";

        public static ConfigNode GetCommandModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, COMMAND_MODULE);
        }

        public static ConfigNode GetKerbNetAccessModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, KERB_NET_ACCESS_MODULE);
        }

        public static ConfigNode GetDataTransmitterModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, DATA_TRANSMITTER_MODULE);
        }

        public static ConfigNode GetProbeControlPointModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, PROBE_CONTROL_POINT_MODULE);
        }

        public static ConfigNode GetReactionWheelModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, REACTION_WHEEL_MODULE);
        }

        public static ConfigNode GetSASModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, SAS_MODULE);
        }

        public static ConfigNode GetScienceContainerModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, SCIENCE_CONTAINER_MODULE);
        }

        public static ConfigNode GetScienceExperimentModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, SCIENCE_EXPERIMENT_MODULE);
        }

        public static ConfigNode GetSeatModule(AvailablePart part)
        {
            return GetNodeByName(part.partConfig, MODULE, SEAT_MODULE);
        }


        public static double GetProperty(ConfigNode node, string propertyName) {
            return Double.Parse(node.GetValue(propertyName));
        }

        public static double GetProperty(AvailablePart part, string propertyName)
        {
            return GetProperty(part.partConfig, propertyName);
        }

        public static bool GetBooleanProperty(ConfigNode node, string propertyName)
        {
            string property = node.HasValue(propertyName) ? node.GetValue(propertyName) : "False";
            return property == "True";
        }

        public static bool HasProperty(ConfigNode node, string propertyName)
        {
            return node.HasValue(propertyName);
        }

        public static ConfigNode GetResource(ConfigNode node, string resourceName)
        {
            return GetNodeByName(node, RESOURCE, resourceName);
        }

        public static ConfigNode GetResource(AvailablePart part, string resourceName)
        {
            return GetResource(part.partConfig, resourceName);
        }

        private static ConfigNode GetNodeByName(ConfigNode node, string nodeType, string name)
        {
            ConfigNode[] nodes = node.GetNodes(nodeType);
            foreach (ConfigNode n in nodes)
            {
                if (n.GetValue("name") == name) return n;
            }
            return null;
        }
    }
}
