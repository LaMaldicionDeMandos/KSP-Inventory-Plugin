using System;
namespace inventory
{
    public class IntoShipState: State
    {
        public static string STATE_NAME = "ship";

        private string _shipName;

        public IntoShipState(string shipName)
        {
            _shipName = shipName;
        }

        public IntoShipState()
        {
        }

        public override string GetName()
        {
            return STATE_NAME;
        }

        public override void Load(ConfigNode node)
        {
            _shipName = node.GetValue("shipName");
        }

        public override void Save(ConfigNode node)
        {
            ConfigNode localNode = new ConfigNode(State.NODE_NAME);
            localNode.AddValue("shipName", _shipName);
            localNode.AddValue("name", GetName());
            node.AddNode(localNode);
        }

        public string shipName
        {
            get
            {
                return _shipName;
            }
        }
    }
}
