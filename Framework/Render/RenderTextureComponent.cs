using System;
using System.Drawing;
using Framework.Algorithms;
using OpenTK.Graphics.OpenGL;
using Framework.Debug;
using Framework.Object;
using Zenseless.Geometry;
using Zenseless.HLGL;
using Zenseless.OpenGL;

namespace Framework.Render {

	public class RenderTextureComponent : Component, RenderComponent {

		private readonly ITexture texture;

		public Box2D Rect { get; set; }

		[Obsolete("May be obsolete because we always need bounds (rect)?")]
		private RenderTextureComponent(Bitmap image) {
			texture = TextureLoader.FromBitmap(image);
			if (texture == null) {
				throw new ArgumentException("Texture is not a 2dGL texture!");
			}

			Lifecycle.onDestroy += () => texture?.Dispose();
		}

		public RenderTextureComponent(Bitmap image, Box2D rect) : this(image) {
			Rect = rect;
		}

		public RenderTextureComponent(Bitmap image, float width, float height) :
			this(image, new Box2D(-width / 2, -height / 2, width, height)) {
		}

		public void Render() {
			// @see: https://github.com/danielscherzer/Framework/blob/72fec5c85e6f21b868c41141ed1c8105f5252e5e/CG/Examples/TextureExample/TextureExample.cs
			//GL.Clear(ClearBufferMask.ColorBufferBit);
			//GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			//GL.Enable(EnableCap.Blend); // for transparency in textures we use blending

			// TODO Currently we don't know why we need this?
			// Got from:
			// @see: https://github.com/danielscherzer/Framework/blob/72fec5c85e6f21b868c41141ed1c8105f5252e5e/CG/Examples/TextureExample/TextureExample.cs
			GL.Enable(EnableCap.Texture2D); //todo: only for non shader pipeline relevant -> remove at some point

			texture.Activate();
			GL.Begin(PrimitiveType.Quads);

			// Color is multiplied with the texture color
			// White means no color change in the texture will be applied
			GL.Color3(Color.White);
			if (FrameworkDebugMode.IsEnabled) {
				GL.Color3(Color.LightGray);
			}

			var matrix = GameObject.Transform.GetTransformationMatrixCached(!GameObject.IsUiElement);
			var minXminY = FastVector2Transform.Transform(Rect.MinX, Rect.MinY, matrix);
			var maxXminY = FastVector2Transform.Transform(Rect.MaxX, Rect.MinY, matrix);
			var maxXmaxY = FastVector2Transform.Transform(Rect.MaxX, Rect.MaxY, matrix);
			var minXmaxY = FastVector2Transform.Transform(Rect.MinX, Rect.MaxY, matrix);

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
			
			GL.Disable(EnableCap.Texture2D);
		}
	}

}
