using D3vS1m.Application.Scene;
using D3vS1m.Application.Scene.Geometries;
using D3vS1m.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MSTests.Application
{
    [TestClass]
    public class SceneTests : TestBase
    {
        GeometryRepository _geometries;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _io = ArrangeIOController();
            _geometries = new GeometryRepository();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }



        [TestMethod]
        public void ImportGeometryFromObj()
        {
            // arrange
            var key = D3vS1m.Persistence.Constants.Wavefront.Key;
            var _setting = new FileSetting
            {
                Location = APP_LOCATION,
                Name = "WallTest5.obj"
            };

            // act
            var sceneRoot = _io.Importer(key)
                 .Setup(_setting)
                 .Import()
                 .As<Geometry>(new ObjAdapter());
            _geometries.Add(sceneRoot, _setting.Name);

            // assert
            Assert.IsNotNull(sceneRoot, "no imported object");

            var name = "Wand";
            var found = _geometries.FirstOrDefault(name, true);

            Assert.IsTrue(sceneRoot.Children.Count == 2, "wrong children found");
            Assert.IsNotNull(found, $"The geometry '{name}' is missing");

        }

        [TestMethod]
        public void TrySceneSimulator()
        {
            // arrange
            var sceneArgs = new InvariantSceneArgs();
            var sceneSim = new SceneSimulator(null).With(sceneArgs);

            // act
            sceneSim.Run();

            // assert

        }
    }
}
