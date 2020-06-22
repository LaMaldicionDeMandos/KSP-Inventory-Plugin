using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class InventoryFactory : MonoBehaviour
    {
        private List<AvailablePart> EMPTY_LIST = new List<AvailablePart>();
        private static int LEFT_SCREEN_WEIGHT = 262;

        private static int BUTTONS_PANEL_HEIGHT = 60;
        private static int BUTTONS_PANEL_WEIGHT = 110;

        private List<AvailablePart> availableParts = new List<AvailablePart>();
        private Dictionary<PartCategories, List<AvailablePart>> partsByCategory = new Dictionary<PartCategories, List<AvailablePart>>();


        private Rect buttonsPanel;
        private Rect buttonsBox;

        private bool isFactoryOpen = false;
        private bool isInventoryOpen = false;

        GUIStyle boxStyle;

        EditorLogic editor;
        PartCategories currentCategory = PartCategories.Pods;

        FactoryPanel factoryPanel;
        InventoryPanel inventoryPanel;
        PartCategoryToolBox categoryTools;

        public void Start()
        {
            log("Factory loaded");
            editor = EditorLogic.fetch;
            LoadParts();
            CreateRects();
            AddListeners();
            editor.launchBtn.enabled = false;
        }

        private void AddListeners()
        {
            GameEvents.onEditorPartEvent.Add(PartEvent);
            GameEvents.FindEvent<EventData<PartCategories>>("onSelectCategory").Add(OnSelectCategory);
        }

        private void OnSelectCategory(PartCategories category)
        {
            currentCategory = category;
        }

        private void LoadParts()
        {
            PartLoader partLoader = PartLoader.Instance;
            if (!partLoader.IsReady()) partLoader.StartLoad();
            List<AvailablePart> parts = partLoader.loadedParts;
            parts.ForEach(part =>
            {
                PartCategories category = part.category;
                if (!partsByCategory.ContainsKey(part.category)) partsByCategory.Add(part.category, new List<AvailablePart>());
                partsByCategory[category].Add(part);
                availableParts.Add(part);
            });
        }

        private void CreateRects()
        {
            categoryTools = new PartCategoryToolBox(LEFT_SCREEN_WEIGHT + BUTTONS_PANEL_WEIGHT + 2, Screen.height - PartCategoryToolBox.SCREEN_HEIGHT);
            factoryPanel = new FactoryPanel(LEFT_SCREEN_WEIGHT + BUTTONS_PANEL_WEIGHT + 2, Screen.height - FactoryPanel.SCREEN_HEIGHT - PartCategoryToolBox.SCREEN_HEIGHT);
            inventoryPanel = new InventoryPanel(LEFT_SCREEN_WEIGHT + BUTTONS_PANEL_WEIGHT + 2, Screen.height - InventoryPanel.SCREEN_HEIGHT - PartCategoryToolBox.SCREEN_HEIGHT);
            buttonsPanel = new Rect(LEFT_SCREEN_WEIGHT, Screen.height - BUTTONS_PANEL_HEIGHT, BUTTONS_PANEL_WEIGHT, BUTTONS_PANEL_HEIGHT);
            buttonsBox = new Rect(0, 0, BUTTONS_PANEL_WEIGHT, BUTTONS_PANEL_HEIGHT);
        }

        void OnDestroy()
        {
            GameEvents.onEditorPartEvent.Remove(PartEvent);
            GameEvents.FindEvent<EventData<PartCategories>>("onSelectCategory").Remove(OnSelectCategory);

        }

        private void PartEvent(ConstructionEventType type, Part part)
        {
            log("Part Event: " + type + ", part: " + part.name + ", model name: " + part.PartValues);
            if (type == ConstructionEventType.PartCreated)
            {
                GameEvents.onEditorPartEvent.Fire(ConstructionEventType.PartDeleted, part);
            }

        }

        private void OnGUI()
        {
            GUIStyles();
            //ScreenMessages.PostScreenMessage("GUI!!", 1);
            GUIBuildButtons();
        }

        private void GUIBuildButtons()
        {
            GUILayout.BeginArea(buttonsPanel);
            GUI.Box(buttonsBox, "");
            GUILayout.BeginVertical();
            isInventoryOpen = GUILayout.Toggle(isInventoryOpen, "Inventario");
            isFactoryOpen = GUILayout.Toggle(isFactoryOpen, "Fabrica");

            GUILayout.EndVertical();
            GUILayout.EndArea();

            checkButtons();

        }

        private void checkButtons()
        {
            if (isFactoryOpen || isInventoryOpen) categoryTools.show();
            if (isFactoryOpen)
            {
                List<AvailablePart> parts = EMPTY_LIST;
                if (partsByCategory.ContainsKey(currentCategory)) parts = partsByCategory[currentCategory];
                factoryPanel.show(GetInstanceID(), parts);
            }
            if (isInventoryOpen)
            {
                inventoryPanel.show(GetInstanceID());
            }
        }

        private void log(string message)
        {
            Debug.Log("[INVENTORY] " + message);
        }

        void GUIStyles()
        {
            GUI.skin = HighLogic.Skin;
            GUI.skin.button.alignment = TextAnchor.MiddleCenter;

            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 11;
            boxStyle.padding.top = GUI.skin.box.padding.bottom = GUI.skin.box.padding.left = 5;
            boxStyle.alignment = TextAnchor.UpperLeft;
        }

    }
}
