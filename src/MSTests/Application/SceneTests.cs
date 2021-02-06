using System.IO;
using System.Linq;
using D3vS1m.Application.Scene;
using D3vS1m.Application.Scene.Geometries;
using D3vS1m.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MSTests.Application
{
    [TestClass]
    public class SceneTests : TestsBase
    {
        GeometryRepository _geometries;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

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
            var file = Path.Combine(APP_LOCATION, "WallTest5.obj");

            // act
            var sceneRoot = new ObjReader(file).Read().First();
            _geometries.Add(sceneRoot);
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
