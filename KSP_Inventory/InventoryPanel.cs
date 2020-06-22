using System;
using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    public class InventoryPanel
    {
        public static string INVENTORY_TITLE = "Inventory";
        public static int SCREEN_HEIGHT = 250;
        public static int SCREEN_WEIGHT = 570;
        private static int PART_BUTTON_SIZE = 48;

        Rect panelScreen;

        public InventoryPanel(int x, int y)
        {
            panelScreen = new Rect(x, y, SCREEN_WEIGHT, SCREEN_HEIGHT);
        }

        public void show(int windowId)
        {
            GUILayout.Window(windowId, panelScreen, (id) => GUILayout.SelectionGrid(0, new List<GUIContent>().ToArray(), 10), INVENTORY_TITLE);
        }

    }
}
