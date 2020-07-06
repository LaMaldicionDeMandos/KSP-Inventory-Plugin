using System;
using System.Collections.Generic;
using UnityEngine;
namespace inventory
{
    public class MissingPartsDialog
    {
        private static string TITLE = "Missing Parts";
        private static int WINDOW_TITLE_HEIGHT = 25;
        public static int WINDOW_HEIGHT = 600;
        public static int WINDOW_WIDTH = 250;

        private int x, y;
        
 
        private Rect windowRect;

        private GUIStyle boxStyle;
        private GUIStyle partNameLabelStyle;
        private GUIStyle buildingTimeLabelStyle;

        private Vector2 scrollPosition = Vector2.zero;

        public MissingPartsDialog(): this((Screen.width - WINDOW_WIDTH) / 2, (Screen.height - WINDOW_HEIGHT) / 2){}

        public MissingPartsDialog(int x, int y)
        {
            this.x = x;
            this.y = y;
            createRects();
        }

        public void show(int windowId, List<AvailablePart> parts)
        {
            GUIStyles();
            GUILayout.Window(windowId, windowRect, OnGUI(parts), TITLE, GUI.skin.window);
        }

        private void createRects()
        {
            windowRect = new Rect(x, y, WINDOW_WIDTH, WINDOW_HEIGHT);
        }

        private void GUIStyles()
        {
            GUI.skin = HighLogic.Skin;
            if (boxStyle == null) createStyles();

        }

        private void createStyles()
        {
            foreach(GUIStyle style in HighLogic.Skin.customStyles)
            {
                Log.log("Custom Styles " + style.name);
            }

            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.padding = new RectOffset(8, 8, 8, 8);
            boxStyle.margin = new RectOffset(2, 2, 2, 2);

            partNameLabelStyle = new GUIStyle(GUI.skin.label);
            partNameLabelStyle.fontSize = 12;
            partNameLabelStyle.normal.textColor = XKCDColors.Yellowish;

            buildingTimeLabelStyle = new GUIStyle(GUI.skin.label);
            buildingTimeLabelStyle.fontSize = 11;
            buildingTimeLabelStyle.normal.textColor = XKCDColors.Aqua;
        }

        private GUI.WindowFunction OnGUI(List<AvailablePart> parts)
        {
            return (id) => {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar);
                GUILayout.BeginVertical();
                parts.ForEach((part) =>
                {
                    GUILayout.BeginHorizontal(boxStyle);
                    GUILayout.Box(PartIcon.GetPartIcon(part).texture, GUILayout.ExpandWidth(false));
                    GUILayout.BeginVertical();
                    GUILayout.Label(part.title, partNameLabelStyle);
                    GUILayout.Label("Estimated Time: " + PartFactoryCalculator.parse(part), buildingTimeLabelStyle);
                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();
                });
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
            };
        }
    }
}
