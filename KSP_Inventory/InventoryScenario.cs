using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    [KSPScenario(ScenarioCreationOptions.AddToNewCareerGames | ScenarioCreationOptions.AddToExistingCareerGames, GameScenes.EDITOR, GameScenes.PSYSTEM, GameScenes.SPACECENTER, GameScenes.FLIGHT)]
    public class InventoryScenario : ScenarioModule
    {
        [KSPField(isPersistant = true, guiActive = false)]
        public string gameName;

        [KSPField(isPersistant = true, guiActive = false)]
        public int starts;

        public void Start()
        {
            gameName = HighLogic.CurrentGame.Title;
            starts++;
        }

        public override void OnSave(ConfigNode node)
        {
            ConstructionState state = new ConstructionState(Planetarium.GetUniversalTime(), 600);
            state.Save(node);
            base.OnSave(node);
            Debug.Log("[INVENTORY] Save Config node: " + node.ToString());

        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
            Debug.Log("[INVENTORY] Load Config node: " + node.ToString());
            ConfigNode[] states = node.GetNodes("STATE");
            foreach (ConfigNode n in states)
            {
                if (n.GetValue("name") == "building")
                {
                    ConstructionState state = new ConstructionState();
                    state.Load(n);
                    Debug.Log("[INVENTORY] Load state, startDate: " + state.startDate + " duration: " + state.duration);
                }
            }
        }
    }
}
