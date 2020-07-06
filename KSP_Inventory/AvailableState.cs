using System;
namespace inventory
{
    public class AvailableState : State
    {
        public static string STATE_NAME = "available";

        public override string GetName()
        {
            return STATE_NAME;
        }

        public override void Load(ConfigNode node)
        {
        }

        public override void Save(ConfigNode node)
        {
            ConfigNode localNode = new ConfigNode(State.NODE_NAME);
            localNode.AddValue("name", GetName());
            node.AddNode(localNode);
        }
    }
}
