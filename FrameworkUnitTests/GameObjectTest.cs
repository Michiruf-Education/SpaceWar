﻿using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrameworkUnitTests {

	[TestClass]
	public class GameObjectTest {

		[TestMethod]
		public void ConstructorTest() {
			// Test constructor initialized variables
			var go1 = new GameObject();
			Assert.IsNotNull(go1.Transform);
			Assert.IsNotNull(go1.Children);
			Assert.IsNotNull(go1.Components);
			Assert.IsTrue(go1.IsEnabled);
			Assert.IsFalse(go1.IsUiElement);
			Assert.AreSame(go1.Transform.GameObject, go1);

			var go2 = new GameObject(true);
			Assert.IsTrue(go2.IsUiElement);
		}

		[TestMethod]
		public void ParentTest() {
			var go1 = new GameObject(true);
			var go2 = new GameObject();
			go1.AddChild(go2);
			Assert.AreEqual(go2.Parent, go1);
		}

		[TestMethod]
		public void ChildrenTest() {
			var go1 = new GameObject(true);
			var go2 = new GameObject();
			go1.AddChild(go2);
			Assert.IsTrue(go1.Children.Contains(go2));
		}

		[TestMethod]
		public void UiElementInheritanceTest() {
			var go1 = new GameObject(true);
			var go2 = new GameObject();
			go1.AddChild(go2);
			Assert.IsTrue(go2.IsUiElement);
		}
	}

}
