using System;
using UnityEngine;

namespace inventory
{
    public class PartCategoryToolBox
    {

        public static int SCREEN_HEIGHT = 40;
        public static int SCREEN_WEIGHT = 570;

        private PartCategoryIconHelper iconHelper;

        Rect panelScreen;
        RectOffset buttonsPadding = new RectOffset(1, 1, 1, 1);
        RectOffset buttonsMaging = new RectOffset(0, 0, 0, 0);
        GUIStyle mySty;

        public PartCategoryToolBox(int x, int y)
        {
            iconHelper = new PartCategoryIconHelper();
            panelScreen = new Rect(x, y, SCREEN_WEIGHT, SCREEN_HEIGHT);
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
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Pods).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.FuelTank).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Engine).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Control).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Structural).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Robotics).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Coupling).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Payload).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Aero).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Ground).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Thermal).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Electrical).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Communication).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Science).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Cargo).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Utility).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.Button(iconHelper.GetIcon(PartCategories.Propulsion).iconNormal, mySty, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}
