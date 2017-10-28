using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using SpaceWar.Framework.Debug;
using SpaceWar.Framework.Object;
using Zenseless.Geometry;
using Zenseless.OpenGL;

namespace SpaceWar.Framework.Render {

	public class RenderTextureComponent : Component, RenderComponent {

		private readonly Texture2dGL texture;

		public Box2D Rect { get; set; }

		[Obsolete("May be obsolete because we always need bounds (rect)?")]
		private RenderTextureComponent(string file) {
			texture = TextureLoader.FromFile(file) as Texture2dGL;
			if (texture == null) {
				throw new ArgumentException("Texture is not a 2dGL texture!");
			}

			Lifecycle.onDestroy += () => texture?.Dispose();
		}

		public RenderTextureComponent(string file, Box2D rect) : this(file) {
			Rect = rect;
		}

		public RenderTextureComponent(string file, float width, float height) :
			this(file, new Box2D(-width / 2, -height / 2, width, height)) {
		}

		public virtual void Render() {
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

			var minXminY = GameObject.Transform.CalculatePointPosition(Rect.MinX, Rect.MinY);
			var maxXminY = GameObject.Transform.CalculatePointPosition(Rect.MaxX, Rect.MinY);
			var maxXmaxY = GameObject.Transform.CalculatePointPosition(Rect.MaxX, Rect.MaxY);
			var minXmaxY = GameObject.Transform.CalculatePointPosition(Rect.MinX, Rect.MaxY);

			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(minXminY.X, minXminY.Y);
			GL.TexCoord2(1.0f, 0.0f);
			GL.Vertex2(maxXminY.X, maxXminY.Y);
			GL.TexCoord2(1.0f, 1.0f);
			GL.Vertex2(maxXmaxY.X, maxXmaxY.Y);
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(minXmaxY.X, minXmaxY.Y);

			GL.End();
			texture.Deactivate();
		}
	}

}
