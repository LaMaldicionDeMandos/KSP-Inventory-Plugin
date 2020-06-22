using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    [KSPScenario(ScenarioCreationOptions.AddToNewCareerGames | ScenarioCreationOptions.AddToExistingCareerGames, GameScenes.EDITOR, GameScenes.PSYSTEM, GameScenes.SPACECENTER, GameScenes.FLIGHT)]
    public class InventoryScenario : ScenarioModule
    {
        private static InventoryScenario _instance;
        public static InventoryScenario instance()
        {
            return _instance;
        }

        private List<AvailablePart> availableParts = new List<AvailablePart>();

        public void Start()
        {
            _instance = this;
            /*
            PartLoader partLoader = PartLoader.Instance;
            if (!partLoader.IsReady()) partLoader.StartLoad();
            List<AvailablePart> parts = partLoader.loadedParts;
            parts.ForEach(part =>
            {
                Debug.Log("[INVENTORY] Part: " + part.title + " - " + part.name);
                availableParts.Add(part);
            });
            parts.RemoveAll(part => true);
            */
        }

        public override void OnSave(ConfigNode node)
        {
            base.OnSave(node); 
        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
        }

        public List<AvailablePart> GetAvailableParts()
        {
            return availableParts;
        }

    }
}
