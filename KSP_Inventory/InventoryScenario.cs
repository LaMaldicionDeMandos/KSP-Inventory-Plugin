using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    [KSPScenario(ScenarioCreationOptions.AddToNewCareerGames | ScenarioCreationOptions.AddToExistingCareerGames, GameScenes.EDITOR, GameScenes.PSYSTEM, GameScenes.SPACECENTER, GameScenes.FLIGHT)]
    public class InventoryScenario : ScenarioModule
    {
        [KSPField(isPersistant = true, guiActive = false)]
        public string gameName;

        private List<PartItem> partItems;
        private List<AvailablePart> allParts;

        private ModuleDelegator moduleDelegator;

        public void Start()
        {
            gameName = HighLogic.CurrentGame.Title;
        }

        public override void OnSave(ConfigNode node)
        {
            partItems.ForEach(partItem => partItem.Save(node));
            base.OnSave(node);
            Log.log("Save Config node: " + node.ToString());
        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);
            Log.log("Load Config node: " + node.ToString());

            this.partItems = loadParts(node);

            if (!PartLoader.Instance.IsReady()) PartLoader.Instance.StartLoad();
            allParts = PartLoader.Instance.loadedParts;

            moduleDelegator = ModuleDelegator.create(this.partItems, this.allParts);
            startModules();
        }

        private void startModules()
        {
            moduleDelegator.startModules();
        } 

        private List<PartItem> loadParts(ConfigNode node)
        {
            ConfigNode[] itemNodes = node.GetNodes(PartItem.NODE_NAME);
            List<PartItem> partItems = new List<PartItem>();
            foreach (ConfigNode n in itemNodes)
            {
                PartItem item = new PartItem();
                item.Load(n);
                partItems.Add(item);
            }
            return partItems;
        }

        void OnGUI()
        {
            if (moduleDelegator != null) moduleDelegator.OnGUI(GetInstanceID());
        }

        public void OnDisable()
        {
            Log.log("Disable inventory escenario");
            moduleDelegator.OnDisable();
        }
    }
}
