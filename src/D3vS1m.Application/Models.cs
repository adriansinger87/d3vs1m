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
            public static string Key => "D3vS1m runtime";
        }


        // antenna models
        public static class Antenna
        {
            public static class Flat
            {
                public static string Key => "flat antenna";
            }

            public static class Simple
            {
                public static string Key => "simple antenna";
            }

            public static class Spheric
            {
                public static string Key => "spheric antenna";
            }
        }

        // channel models
        public static class Channel
        {
            public static class AdaptedFriis
            {
                public static string Key => "adapted friis transmission";
            }
        }
        // device models

        // netowrk models
        public static class Network
        {
            public static string Key => "peer to peer network";
        }

        // communication models
        public static class Communication
        {
            public static class LrWpan
            {
                public static string Key => "wireless communication";
            }
        }

        // scene models
        public static class Scene
        {
            public static string Key => "invariant scene";
            public static string DefaultGeometry => "geometry";
        }

        // energy models
        public static class Energy
        {
            public static class Battery
            {
                public static string Key => "battery pack";
            }
        }
    }

}
