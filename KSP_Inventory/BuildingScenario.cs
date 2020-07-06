using System;
using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    public class BuildingScenario
    {
        private List<PartItem> partItems;
        private List<AvailablePart> allParts;

        MissingPartsDialog missingPartDialog;
        List<AvailablePart> missingParts = new List<AvailablePart>();
        bool sholdShowDialog;
        EditorLogic editor;
        

        public BuildingScenario(List<PartItem> partItems, List<AvailablePart> allParts)
        {
            this.partItems = partItems;
            this.allParts = allParts;

            editor = EditorLogic.fetch;
            missingPartDialog = new MissingPartsDialog((int)(Screen.width*0.21), (int)(Screen.height*0.14));
            AddListeners();
        }

        public void OnGUI(int windowId)
        {
            if (sholdShowDialog) missingPartDialog.show(windowId, missingParts);
        }

        public void OnDisable()
        {
            GameEvents.onEditorPartEvent.Remove(OnPartEvent);
        }

        private void AddListeners()
        {
            editor.launchBtn.enabled = true;
            GameEvents.onEditorPartEvent.Add(OnPartEvent);
        }

        private void OnPartEvent(ConstructionEventType eventType,  Part part)
        {
            if (eventType == ConstructionEventType.PartCreated)
            {
                applyPartCreated(part);
            }
            if (eventType == ConstructionEventType.PartDropped)
            {
                //TODO Es cuando la dejo tirada
            }
            if (eventType == ConstructionEventType.PartAttached)
            {
                applyPartAttached(part);
            }
            if (eventType == ConstructionEventType.PartDeleted)
            {
                //TODO Es cuando la borro
            }
        }

        private void applyPartCreated(Part part)
        {
            AvailablePart availablePart = FindAvailablePartByName(part.name);
            if (availablePart.category != PartCategories.Pods) return;
            if (editor.ship.parts.Count == 0)
            {
                Log.log("Attaching root component " + availablePart.title);
                if (NoAvailablePartsByName(part.name)) NewMissingPart(availablePart);
                
            }
        }

        private void applyPartAttached(Part part)
        {
            AvailablePart availablePart = FindAvailablePartByName(part.name);
            Log.log("Attaching component " + availablePart.title);
            if (NoAvailablePartsByName(part.name)) NewMissingPart(availablePart);
        }

        private bool NoAvailablePartsByName(string name)
        {
            return FilterAvailablePartByName(name).Count == 0;
        }

        private List<PartItem> FilterAvailablePartByName(string name)
        {
            return partItems
                .FindAll((item) => item.state.GetName().Equals(AvailableState.STATE_NAME))
                .FindAll((item) => item.partName.Equals(name));
        }

        private AvailablePart FindAvailablePartByName(string name)
        {
            Log.log("Find part " + name);
            return allParts.Find((part) => part.name == name);
        }

        private void NewMissingPart(AvailablePart part)
        {
            missingParts.Add(part);
            sholdShowDialog = true;
            editor.launchBtn.enabled = false;
        }
    }


}
