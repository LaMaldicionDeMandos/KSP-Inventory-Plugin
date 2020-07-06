using System;
using System.Collections.Generic;

namespace inventory
{
    public class EmptyModuleDelegator : ModuleDelegator
    {
        public EmptyModuleDelegator(List<PartItem> partItems, List<AvailablePart> allParts): base(partItems, allParts)
        {
        }

        public override void OnGUI(int windowId)
        {
        }

        public override void startModules()
        {
        }
    }
}
