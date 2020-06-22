using System;
using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    public class PartCategoryToolBox
    {

        public static EventData<PartCategories> onSelectCategory = new EventData<PartCategories>("onSelectCategory");

        public static int SCREEN_HEIGHT = 40;
        public static int SCREEN_WEIGHT = 570;

        private static List<PartCategories> categories = new List<PartCategories>(new PartCategories[] {
            PartCategories.Pods,
            PartCategories.FuelTank,
            PartCategories.Engine,
            PartCategories.Control,
            PartCategories.Structural,
            PartCategories.Robotics,
            PartCategories.Coupling,
            PartCategories.Payload,
            PartCategories.Aero,
            PartCategories.Ground,
            PartCategories.Thermal,
            PartCategories.Electrical,
            PartCategories.Communication,
            PartCategories.Science,
            PartCategories.Cargo,
            PartCategories.Utility,
            PartCategories.Propulsion
        });

        private PartCategoryIconHelper iconHelper;

        Rect panelScreen;
        RectOffset buttonsPadding = new RectOffset(1, 1, 1, 1);
        RectOffset buttonsMaging = new RectOffset(0, 0, 0, 0);
        private List<GUIContent> buttons;

        GUIStyle mySty;

        private int selectedItem = 0;

        public PartCategoryToolBox(int x, int y)
        {
            iconHelper = new PartCategoryIconHelper();
            panelScreen = new Rect(x, y, SCREEN_WEIGHT, SCREEN_HEIGHT);
            buttons = buildButtons();
        }

        public void show()
        {
            if (mySty == null) mySty = new GUIStyle(GUI.skin.button);
            mySty.normal.textColor = mySty.focused.textColor = Color.white;
            mySty.hover.textColor = mySty.active.textColor = Color.yellow;
            mySty.onNormal.textColor = mySty.onFocused.textColor = mySty.onHover.textColor = mySty.onActive.textColor = Color.green;
            mySty.margin = buttonsMaging;
            mySty.padding = buttonsPadding;
            GUILayout.BeginArea(panelScreen);
            GUILayout.BeginHorizontal();
            int oldSelectedItem = selectedItem;
            selectedItem = GUILayout.SelectionGrid(selectedItem, buttons.ToArray(), 17, mySty);
            if (selectedItem != oldSelectedItem) PartCategoryToolBox.onSelectCategory.Fire(categories[selectedItem]);
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private List<GUIContent> buildButtons()
        {
            List<GUIContent> buttons = new List<GUIContent>();
            buttons.Add(buildButton(PartCategories.Pods));
            buttons.Add(buildButton(PartCategories.FuelTank));
            buttons.Add(buildButton(PartCategories.Engine));
            buttons.Add(buildButton(PartCategories.Control));
            buttons.Add(buildButton(PartCategories.Structural));
            buttons.Add(buildButton(PartCategories.Robotics));
            buttons.Add(buildButton(PartCategories.Coupling));
            buttons.Add(buildButton(PartCategories.Payload));
            buttons.Add(buildButton(PartCategories.Aero));
            buttons.Add(buildButton(PartCategories.Ground));
            buttons.Add(buildButton(PartCategories.Thermal));
            buttons.Add(buildButton(PartCategories.Electrical));
            buttons.Add(buildButton(PartCategories.Communication));
            buttons.Add(buildButton(PartCategories.Science));
            buttons.Add(buildButton(PartCategories.Cargo));
            buttons.Add(buildButton(PartCategories.Utility));
            buttons.Add(buildButton(PartCategories.Propulsion));
            return buttons;
        }

        private GUIContent buildButton(PartCategories category)
        {
            Texture texture = iconHelper.GetIcon(category).iconNormal;
            string title = PartCategories.Pods.Description();
            return new GUIContent(texture, title);
        }
    }
}
