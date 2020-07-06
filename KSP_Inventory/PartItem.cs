using System;
namespace inventory
{
    public class PartItem : IConfigNode
    {
        public static string NODE_NAME
        {
            get
            {
                return "PART_ITEM";
            }
        }

        private string _partName;
        public AvailablePart part;
        public State state;

        public PartItem()
        {
        }

        public PartItem(string partName, State state)
        {
            _partName = partName;
            this.state = state;
        }

        public PartItem(AvailablePart part, State state)
        {
            this.part = part;
            _partName = part.name;
            this.state = state;
        }

        public string partName
        {
            get
            {
                return part == null ? _partName : part.name;
            }
        }

        public void Load(ConfigNode node)
        {
            _partName = node.GetValue("partName");
            state = PartStateFactory.build(node.GetNode(State.NODE_NAME));
        }

        public void Save(ConfigNode node)
        {
            ConfigNode localNode = new ConfigNode(NODE_NAME);
            localNode.AddValue("partName", partName);
            state.Save(localNode);
            node.AddNode(localNode);
        }
    }
}
