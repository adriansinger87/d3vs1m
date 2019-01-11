using D3vS1m.Application.Scene;
using D3vS1m.Application.Scene.Geometries;
using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence;
using D3vS1m.Persistence.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Application
{
    [TestClass]
    public class SceneTests : BaseTests
    {
        IOControllable _io;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _io = new IOController();
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
            var _setting = new FileSettings
            {
                Location = base.DataDirectory,
                Name = "WallTest5.obj"
            };

            // act
            var sceneRoot = _io.Importer(ImportTypes.WavefrontObj)
                 .Setup(_setting)
                 .Import()
                 .CastTo<Geometry>(new ObjCasting());
            sceneRoot.Name = _setting.Name;

            // assert
            var name = "Wand";
            var wall = sceneRoot.FirstByName(name, true);
            Assert.IsNotNull(sceneRoot, "no imported object");
            Assert.IsTrue(sceneRoot.Children.Count == 2, "wrong children found");
            Assert.IsNotNull(wall, $"The geometry '{name}' is missing");

        }

        [TestMethod]
        public void TrySceneSimulator()
        {
            // arrange
            var sceneArgs = new InvariantSceneArgs();
            var sceneSim = new SceneSimulator().With(sceneArgs);

            // act
            sceneSim.Run();

            // assert
            
        }
    }
}
