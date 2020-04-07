namespace D3vS1m.Application
{
    public static class Models
    {
        // runtime
        public static class Runtime
        {
            public const string Key = "rt";
            public const string Name = "D3vS1m runtime";
        }


        // antenna models
        public static class Antenna
        {
            public static class Flat
            {
                public const string Key = "fla";
                public const string Name = "flat antenna";
            }

            public static class Simple
            {
                public const string Key = "sia";
                public const string Name = "simple antenna";
            }

            public static class Spheric
            {
                public const string Key = "spa";
                public const string Name = "spheric antenna";
            }
        }

        // channel models
        public static class Channel
        {
            public static class AdaptedFriis
            {
                public const string Key = "aft";
                public const string Name = "adapted friis transmission";
            }
        }
        // device models
        public static class Devices
        {
            public const string Key = "dev";
            public const string Name = "basic device";
        }

        // netowrk models
        public static class Network
        {
            public const string Key = "ppn";
            public const string Name = "peer to peer network";
        }

        // communication models
        public static class Communication
        {
            public static class LrWpan
            {
                public const string Key = "com";
                public const string Name = "wireless communication";
            }
        }

        // scene models
        public static class Scene
        {
            public const string Key = "sce";
            public const string Name = "invariant scene";

            public static class Materials
            {
                public const string Key = "mat";
                public const string DefaultGeometry = "geometry";
            }           
        }

        // energy models
        public static class Energy
        {
            public static class Battery
            {
                public const string Key = "bat";
                public const string Name = "battery pack";
            }
        }
    }

}
