using System;
namespace inventory
{
    public class PartStateFactory
    {
        public static State build(ConfigNode node)
        {
            string name = node.GetValue("name");
            if (name.Equals(ConstructionState.STATE_NAME)) return PartStateFactory.buildConstruction(node);
            if (name.Equals(IntoShipState.STATE_NAME)) return PartStateFactory.buildIntoShip(node);
            if (name.Equals(AvailableState.STATE_NAME)) return PartStateFactory.buildAvailable(node);

            return null;

        }

        private static ConstructionState buildConstruction(ConfigNode node)
        {
            ConstructionState state = new ConstructionState();
            return buildState<ConstructionState>(node, state);
        }

        private static IntoShipState buildIntoShip(ConfigNode node)
        {
            IntoShipState state = new IntoShipState();
            return buildState<IntoShipState>(node, state);
        }

        private static AvailableState buildAvailable(ConfigNode node)
        {
            AvailableState state = new AvailableState();
            return buildState<AvailableState>(node, state);
        }

        private static T buildState<T>(ConfigNode node, T state) where T: State
        {
            state.Load(node);
            return state;
        }
    }
}
