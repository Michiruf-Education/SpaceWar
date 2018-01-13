using System;
using System.Text;
using Framework.Object;
using Framework.Utilities;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;
using Zenseless.HLGL;
using Zenseless.OpenGL;

namespace Framework.Render {

	public class RenderShaderComponent : Component, RenderComponent {

		private readonly IShader shader;

		public Box2D Rect { get; set; }

		public RenderShaderComponent(byte[] vertString, byte[] fragString, Box2D rect) {
			shader = ShaderLoader.FromStrings(
				Encoding.Default.GetString(vertString),
				Encoding.Default.GetString(fragString));
			Rect = rect;
		}

		public RenderShaderComponent(byte[] vertString, byte[] fragString, float width, float height) :
			this(vertString, fragString, new Box2D(-width / 2, -height / 2, width, height)) {
		}
		
		// NOTE Maybe extract the methods into a Binder-class?

		public RenderShaderComponent SetAttribute(string name, Vector2 value) {
			var position = shader.GetResourceLocation(ShaderResourceType.Attribute, name);
			GL.VertexAttrib2(position, value);
			return this;
		}

		public RenderShaderComponent SetUniform(string name, float value) {
			var position = shader.GetResourceLocation(ShaderResourceType.Uniform, name);
			GL.Uniform1(position, value);
			return this;
		}

		public RenderShaderComponent SetUniform(string name, Vector3 value) {
			var position = shader.GetResourceLocation(ShaderResourceType.Uniform, name);
			GL.Uniform3(position, value);
			return this;
		}

		public override void OnDestroy() {
			base.OnDestroy();
			shader?.Dispose();
		}

		public void Render() {
			var matrix = GameObject.Transform.GetTransformationMatrixCached(!GameObject.IsUiElement);
			var minXminY = FastVector2Transform.Transform(Rect.MinX, Rect.MinY, matrix);
			var maxXminY = FastVector2Transform.Transform(Rect.MaxX, Rect.MinY, matrix);
			var maxXmaxY = FastVector2Transform.Transform(Rect.MaxX, Rect.MaxY, matrix);
			var minXmaxY = FastVector2Transform.Transform(Rect.MinX, Rect.MaxY, matrix);

			shader.Activate();
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
			shader.Deactivate();
		}
	}

}
