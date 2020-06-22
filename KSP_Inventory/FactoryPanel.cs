using System;
using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    public class FactoryPanel
    {
        public static string FACTORY_TITLE = "Factory";
        public static int SCREEN_HEIGHT = 300;
        public static int SCREEN_WEIGHT = 570;
        private static int PART_BUTTON_SIZE = 48;

        RectOffset buttonsPadding = new RectOffset(8, 8, 8, 8);
        RectOffset buttonsMaging = new RectOffset(2, 2, 2, 2);

        Dictionary<AvailablePart, GUIContent> contents = new Dictionary<AvailablePart, GUIContent>();
        Dictionary<GUIContent, AvailablePart> parts = new Dictionary<GUIContent, AvailablePart>();

        Rect panelScreen;

        int selectedItem = -1;

        GUIStyle buttonStyle;

        public FactoryPanel(int x, int y)
        {
            panelScreen = new Rect(x, y, SCREEN_WEIGHT, SCREEN_HEIGHT);
        }

        public void show(int windowId, List<AvailablePart> parts)
        {
            GUILayout.Window(windowId, panelScreen, showParts(parts), FACTORY_TITLE);

        }

        private GUI.WindowFunction showParts(List<AvailablePart> parts)
        {
            return (id) =>
            {
                applyButtonStyle();
                List<GUIContent> contents = GetContents(parts);
                int rows = contents.Count / 10 + 1;
                int partIndex = 0;
                GUILayout.BeginVertical();
                for (int row = 0; row < rows; row++)
                {
                    GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
                    for (int i = 0; i < 10 && partIndex < contents.Count; i++, partIndex++)
                    {
                        GUILayout.Button(contents[partIndex], buttonStyle, GUILayout.ExpandWidth(false));
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            };
        }

        private List<GUIContent> GetContents(List<AvailablePart> parts)
        {
            return parts.ConvertAll(GetContent);
        }

        private GUIContent GetContent(AvailablePart part)
        {
            if (contents.ContainsKey(part)) return contents[part];
            return createContent(part);
        }

        private GUIContent createContent(AvailablePart part)
        {
            PartIconTexture partIcon = new PartIconTexture(part, PART_BUTTON_SIZE);
            contents.Add(part, partIcon.content);
            return partIcon.content;
        }

        private void applyButtonStyle()
        {
            if (buttonStyle == null) buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = buttonStyle.focused.textColor = Color.white;
            buttonStyle.hover.textColor = buttonStyle.active.textColor = Color.yellow;
            buttonStyle.onNormal.textColor = buttonStyle.onFocused.textColor = buttonStyle.onHover.textColor = buttonStyle.onActive.textColor = Color.green;
            buttonStyle.margin = buttonsMaging;
            buttonStyle.padding = buttonsPadding;
        }

        internal void show(int v1, bool v2)
        {
            throw new NotImplementedException();
        }
    }
}
