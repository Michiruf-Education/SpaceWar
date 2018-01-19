using System;
using System.Drawing;
using Framework.Object;
using Framework.Utilities;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;
using Zenseless.HLGL;
using Zenseless.OpenGL;

namespace Framework.Render {

	public class RenderTextureComponent : Component, RenderComponent {

		private readonly ITexture texture;

		public Box2D Rect { get; set; }
		public Color ColorFilter { get; set; } = Color.White;

		public RenderTextureComponent(Bitmap image, Box2D rect) {
			texture = TextureLoader.FromBitmap(image);
			if (texture == null) {
				throw new ArgumentException("Texture is not a 2dGL texture!");
			}
			Rect = rect;
		}

		public RenderTextureComponent(Bitmap image, float width, float height) :
			this(image, new Box2D(-width / 2, -height / 2, width, height)) {
		}

		public RenderTextureComponent(ITexture texture, Box2D rect) {
			this.texture = texture;
			Rect = rect;
		}

		public RenderTextureComponent(ITexture texture, float width, float height) :
			this(texture, new Box2D(-width / 2, -height / 2, width, height)) {
		}

		public RenderTextureComponent SetColorFilter(Color color) {
			ColorFilter = color;
			return this;
		}

		public override void OnDestroy() {
			base.OnDestroy();
			texture?.Dispose();
		}

		public void Render() {
			// Relevant for non shader pipelines
			// Got from: https://github.com/danielscherzer/Framework/blob/72fec5c85e6f21b868c41141ed1c8105f5252e5e/CG/
			// ... Examples/TextureExample/TextureExample.cs
			GL.Enable(EnableCap.Texture2D);

			// Enable blending for transparency in textures
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Blend);

			// Color is multiplied with the texture color
			// White means no color change in the texture will be applied
			GL.Color3(ColorFilter);

			var matrix = GameObject?.Transform?.GetTransformationMatrixCached(!GameObject.IsUiElement) ?? 
			             System.Numerics.Matrix3x2.Identity;
			var minXminY = FastVector2Transform.Transform(Rect.MinX, Rect.MinY, matrix);
			var maxXminY = FastVector2Transform.Transform(Rect.MaxX, Rect.MinY, matrix);
			var maxXmaxY = FastVector2Transform.Transform(Rect.MaxX, Rect.MaxY, matrix);
			var minXmaxY = FastVector2Transform.Transform(Rect.MinX, Rect.MaxY, matrix);

			texture.Activate();
			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(minXminY);
			GL.TexCoord2(1.0f, 0.0f);
			GL.Vertex2(maxXminY);
			GL.TexCoord2(1.0f, 1.0f);
			GL.Vertex2(maxXmaxY);
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(minXmaxY);
			GL.End();
			texture.Deactivate();

			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.Texture2D);
		}
	}

}
