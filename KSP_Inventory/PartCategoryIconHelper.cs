using System;
using System.Collections.Generic;

namespace inventory
{
    public class PartCategoryIconHelper
    {
        private Dictionary<PartCategories, string> iconNames = new Dictionary<PartCategories, string>();
        public PartCategoryIconHelper()
        {
            IconLoader.Load();
            iconNames.Add(PartCategories.Aero, "stockIcon_aerodynamics");
            iconNames.Add(PartCategories.Cargo, "stockIcon_cargo");
            iconNames.Add(PartCategories.Communication, "stockIcon_communication");
            iconNames.Add(PartCategories.Control, "stockIcon_cmdctrl");
            iconNames.Add(PartCategories.Coupling, "stockIcon_coupling");
            iconNames.Add(PartCategories.Electrical, "stockIcon_electrical");
            iconNames.Add(PartCategories.Engine, "stockIcon_engine");
            iconNames.Add(PartCategories.FuelTank, "stockIcon_fueltank");
            iconNames.Add(PartCategories.Ground, "stockIcon_ground");
            iconNames.Add(PartCategories.none, "stockIcon_fallback");
            iconNames.Add(PartCategories.Payload, "stockIcon_payload");
            iconNames.Add(PartCategories.Pods, "stockIcon_pods");
            iconNames.Add(PartCategories.Propulsion, "stockIcon_propulsion");
            iconNames.Add(PartCategories.Robotics, "R&D_node_icon_robotics");
            iconNames.Add(PartCategories.Science, "stockIcon_science");
            iconNames.Add(PartCategories.Structural, "stockIcon_structural");
            iconNames.Add(PartCategories.Thermal, "stockIcon_thermal");
            iconNames.Add(PartCategories.Utility, "stockIcon_utility");
        }

        public RUI.Icons.Selectable.Icon GetIcon(PartCategories category)
        {
            return IconLoader.GetIcon(iconNames[category]);
        }
    }
}
