using System;
using System.Collections.Generic;

namespace inventory
{
    public class EditorModuleDelegator: ModuleDelegator
    {
        private BuildingScenario buildingModule;
        
        public EditorModuleDelegator(List<PartItem> partItems, List<AvailablePart> allParts): base(partItems, allParts)
        {

        }

        public override void startModules()
        {
            buildingModule = new BuildingScenario(partItems, allParts);
        }

        public override void OnGUI(int windowId)
        {
            buildingModule.OnGUI(windowId);
        }
    }
}
