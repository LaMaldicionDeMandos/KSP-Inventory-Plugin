using System;
namespace inventory
{
    public class ConstructionState: State
    {
        public static string STATE_NAME = "building";

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

        public override string GetName()
        {
            return STATE_NAME;
        }

        public override void Load(ConfigNode node)
        {
            _startDate = Convert.ToDouble(node.GetValue("startDate"));
            _duration = Convert.ToDouble(node.GetValue("duration"));
        }

        public override void Save(ConfigNode node)
        {
            ConfigNode localNode = new ConfigNode(State.NODE_NAME);
            localNode.AddValue("startDare", _startDate);
            localNode.AddValue("duration", _duration);
            localNode.AddValue("name", GetName());
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
