using System;
namespace inventory
{
    public class ConstructionState: State
    {
        private double _startDate;
        private double _duration;

        public ConstructionState(double startDate, double duration)
        {
            _startDate = startDate;
            _duration = duration;
        }

        public ConstructionState()
        {
        }

        public string GetName()
        {
            return "building";
        }

        public void Load(ConfigNode node)
        {
            _startDate = Convert.ToDouble(node.GetValue("startDate"));
            _duration = Convert.ToDouble(node.GetValue("duration"));
        }

        public void Save(ConfigNode node)
        {
            ConfigNode localNode = new ConfigNode("STATE");
            localNode.AddValue("startDare", _startDate);
            localNode.AddValue("duration", _duration);
            node.AddNode(localNode);
        }

        public double startDate
        {
            get {
                return _startDate;
            }

        }

        public double duration
        {
            get
            {
                return _duration;
            }
        }
    }
}
