using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    [KSPScenario(ScenarioCreationOptions.AddToNewCareerGames | ScenarioCreationOptions.AddToExistingCareerGames, GameScenes.EDITOR, GameScenes.PSYSTEM, GameScenes.SPACECENTER, GameScenes.FLIGHT)]
    public class InventoryScenario : ScenarioModule
    {

        public override void OnSave(ConfigNode node)
        {

            base.OnSave(node);
            ScreenMessages.PostScreenMessage("        OnSave", 3);
            print("Saved o sea anda");

            List<AvailablePart> parts = PartLoader.Instance.loadedParts;
            parts.ForEach(delegate (AvailablePart part)
            {
                print(part.name);
            });
            print("imprimio algo?");
            
        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
            ScreenMessages.PostScreenMessage("        OnLoad", 3);
        }

    }
}
