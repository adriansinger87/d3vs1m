namespace D3vS1m.Application
{
    public static class Models
    {

        public static string GetID(string model)
        {
            return model.Replace(" ", "_");
        }

        // runtime
        public static string Runtime => "D3vS1m runtime";

        // antenna models
        public static string FlatAntenna => "flat antenna";
        public static string SimpleAntenna => "simple antenna";
        public static string SphericAntenna => "spheric antenna";

        // channel models
        public static string AdaptedFriisTransmission => "adapted friis transmission";

        // device models

        // netowrk models
        public static string PeerToPeerNetwork => "peer to peer network";

        // communication models
        public static string WirelessCommunication => "wireless communication";

        // scene models
        public static string InvariantScene => "invariant scene";
        public static string DefaultGeometry => "geometry";

        // energy models
        public static string BatteryPack => "battery pack";
    }

}
