using System;
namespace inventory
{
    public abstract class State: IConfigNode
    {
        public static string NODE_NAME
        {
            get
            {
                return "STATE";
            }
        }

        public abstract void Load(ConfigNode node);
        public abstract void Save(ConfigNode node);
        public abstract string GetName();
    }
}
