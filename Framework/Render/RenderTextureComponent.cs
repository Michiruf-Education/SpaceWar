using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;
using Zenseless.OpenGL;

namespace SpaceWar.Framework.Render {

	public class RenderTextureComponent : RenderComponent {

		private readonly Texture2dGL texture;

		public Box2D Rect { get; set; }

		[Obsolete("May be obsolete because we always need bounds (rect)?")]
		public RenderTextureComponent(string file) {
			texture = TextureLoader.FromFile(file) as Texture2dGL;
			if (texture == null) {
				throw new ArgumentException("Texture is not a 2dGL texture!");
			}
		}

		public RenderTextureComponent(string file, Box2D rect) : this(file) {
			Rect = rect;
		}

		public RenderTextureComponent(string file, int width, int height) : this(file) {
			// ReSharper disable PossibleLossOfFraction
			// Fraction loss is okay here because we also have the sizes in there
			Rect = new Box2D(-width / 2, -height / 2, width, height);
		}

		public override void Render() {
			// Color is multiplied with the texture color
			// White means no color change in the texture will be applied
			GL.Color3(Color.White);

			texture.Activate();
			GL.Begin(PrimitiveType.Quads);

			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(Rect.MinX, Rect.MinY);
			GL.TexCoord2(1.0f, 0.0f);
			GL.Vertex2(Rect.MaxX, Rect.MinY);
			GL.TexCoord2(1.0f, 1.0f);
			GL.Vertex2(Rect.MaxX, Rect.MaxY);
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(Rect.MinX, Rect.MaxY);

			GL.End();
			texture.Deactivate();
		}

		// TODO Ensure this deconstructor is called
		// @see: https://andrewlock.net/deconstructors-for-non-tuple-types-in-c-7-0/
		// Method drafted to not forget about resource cleanups!
		public void Deconstruct() {
			texture?.Dispose();
		}
	}

}
