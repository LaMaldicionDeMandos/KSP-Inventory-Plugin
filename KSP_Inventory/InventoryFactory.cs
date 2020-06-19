using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    [KSPAddon(KSPAddon.Startup.EditorVAB, false)]
    public class InventoryFactory : MonoBehaviour
    {

        public void Start()
        {
            Debug.Log("[INVENTORY]Factory loaded");
            print("Factory Loaded");
            GameEvents.onEditorPartEvent.Add(PartEvent);
        }

        private void PartEvent(ConstructionEventType type, Part part)
        {
            Debug.Log("[INVENTORY] Part Event: " + type + ", part: " + part.name + ", model name: " + part.PartValues);

        }
    }
}
