using System;
using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    public class FactoryPanel
    {
        public static string FACTORY_TITLE = "Factory";
        public static int SCREEN_HEIGHT = 250;
        public static int SCREEN_WEIGHT = 570;
        private static int PART_BUTTON_SIZE = 48;

        Dictionary<AvailablePart, GUIContent> contents = new Dictionary<AvailablePart, GUIContent>();
        Dictionary<GUIContent, AvailablePart> parts = new Dictionary<GUIContent, AvailablePart>();

        Rect panelScreen;

        int selectedItem = -1;

        public FactoryPanel(int x, int y)
        {
            panelScreen = new Rect(x, y, SCREEN_WEIGHT, SCREEN_HEIGHT);
        }

        public void show(int windowId, List<AvailablePart> parts)
        {
            GUILayout.Window(windowId, panelScreen, (id) => selectedItem = GUILayout.SelectionGrid(selectedItem, GetContents(parts).ToArray(), 10), FACTORY_TITLE);

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
    }
}
