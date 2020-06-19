using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    [KSPScenario(ScenarioCreationOptions.AddToNewCareerGames | ScenarioCreationOptions.AddToExistingCareerGames, GameScenes.EDITOR, GameScenes.PSYSTEM, GameScenes.SPACECENTER, GameScenes.FLIGHT)]
    public class InventoryScenario : ScenarioModule
    {
        public void Update()
        {
            ScreenMessages.PostScreenMessage("                El escenario está activo!!", 1);
        }


        public override void OnAwake()
        {
            base.OnAwake();
            ScreenMessages.PostScreenMessage("                El escenario Vive!!", 3);
            
        }



        public override void OnSave(ConfigNode node)
        {

            base.OnSave(node);
            ScreenMessages.PostScreenMessage("        OnSave", 3);
            print("Saved o sea anda");
          
            List<AvailablePart> parts = new List<AvailablePart>();
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
