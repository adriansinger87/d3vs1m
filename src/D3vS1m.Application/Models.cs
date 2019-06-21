namespace D3vS1m.Application
{
    public static class Models
    {

        public static string GetID(string model)
        {
            return model.Replace(" ", "_");
        }

        // runtime
        public static class Runtime
        {
            public const string Key = "D3vS1m runtime";
        }


        // antenna models
        public static class Antenna
        {
            public static class Flat
            {
                public const string Key = "flat antenna";
            }

            public static class Simple
            {
                public const string Key = "simple antenna";
            }

            public static class Spheric
            {
                public const string Key = "spheric antenna";
            }
        }

        // channel models
        public static class Channel
        {
            public static class AdaptedFriis
            {
                public const string Key = "adapted friis transmission";
            }
        }
        // device models

        // netowrk models
        public static class Network
        {
            public const string Key = "peer to peer network";
        }

        // communication models
        public static class Communication
        {
            public static class LrWpan
            {
                public const string Key = "wireless communication";
            }
        }

        // scene models
        public static class Scene
        {
            public const string Key = "invariant scene";
            public const string DefaultGeometry = "geometry";
        }

        // energy models
        public static class Energy
        {
            public static class Battery
            {
                public const string Key = "battery pack";
            }
        }
    }

}
