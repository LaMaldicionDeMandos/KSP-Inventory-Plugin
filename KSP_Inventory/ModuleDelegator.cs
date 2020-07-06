using System;
using System.Collections.Generic;

namespace inventory
{
    public abstract class ModuleDelegator
    {
        protected List<PartItem> partItems;
        protected List<AvailablePart> allParts;

        public static ModuleDelegator create(List<PartItem> partItems, List<AvailablePart> allParts)
        {
            if (HighLogic.LoadedSceneIsEditor) return new EditorModuleDelegator(partItems, allParts);

            return new EmptyModuleDelegator(partItems, allParts);
        }
        
        public ModuleDelegator(List<PartItem> partItems, List<AvailablePart> allParts)
        {
            this.partItems = partItems;
            this.allParts = allParts;
        }

        public abstract void startModules();
        public void OnDisable() { }

        public abstract void OnGUI(int windowId);
    }
}
