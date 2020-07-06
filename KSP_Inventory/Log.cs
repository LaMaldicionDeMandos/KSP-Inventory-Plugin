using System;
using UnityEngine;
namespace inventory
{
    public class Log
    {
        public static void log(string message)
        {
            Debug.Log("[INVENTORY] " + message);
        }
    }
}
