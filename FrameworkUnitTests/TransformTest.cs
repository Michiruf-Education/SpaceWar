using System.Numerics;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vector2 = OpenTK.Vector2;

namespace FrameworkUnitTests {

	[TestClass]
	public class TransformTest {
		
		// This is not working because the camera accesses stuff form the game singleton
        /*
		[TestInitialize]
		public void Init() {
			// Create a campera and put it into the singleton for calculation of world position
			// ReSharper disable once ObjectCreationAsStatement
			new CameraComponent(true);
		}

		[TestCleanup]
		public void Clean() {
			CameraComponent.Active = null;
		}
        */

		[TestMethod]
		public void ConstructorTest() {
			var t = new Transform();

			// Initial values
			Assert.IsNull(t.GameObject);
			Assert.AreEqual((Matrix3x2) t.Transformation, Matrix3x2.Identity);
		}

		[TestMethod]
		public void LocalPositionTest() {
			var testPosition = new Vector2(-1f, 2f);
			var t = new Transform {LocalPosition = testPosition};
			var m = (Matrix3x2) t.Transformation;
			Assert.AreEqual(m.M31, -1f);
			Assert.AreEqual(m.M32, 2f);
			Assert.AreEqual(t.LocalPosition, testPosition);
		}

		[TestMethod]
		public void LocalRotationTest() {
			const float testRotation = 3f;
			var t = new Transform {LocalRotation = testRotation};
			// May test matrix values
			Assert.AreEqual(t.LocalRotation, testRotation);
		}

		[TestMethod]
		public void LocalScalingTest() {
			var testScaling = new Vector2(1.2f, 5.2f);
			var t = new Transform {LocalScaling = testScaling};
			// May test matrix values
			Assert.AreEqual(t.LocalScaling, testScaling);
		}

		// TODO Test modifying world would not be good yet, because it will be heavily changed due to the world and local
		// spaces separation that is needed because of GameObject parent-child structure
		// -> Not correct, because were only testing the transform unit itself. Can we do this without mocking?
	}

}
