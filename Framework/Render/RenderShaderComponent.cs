using System;
using System.Drawing;
using System.Text;
using Framework.Camera;
using Framework.Object;
using Framework.Utilities;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;
using Zenseless.HLGL;
using Zenseless.OpenGL;

namespace Framework.Render {

	public class RenderShaderComponent : Component, RenderComponent, UpdateComponent {

		public const string CAMERA_POSITION_ATTRIBUTE = "InCameraPosition";
		public const string CAMERA_MATRIX_ATTRIBUTE = "InCameraMatrix";
		public const string CAMERA_MATRIX_INVERTED_ATTRIBUTE = "InCameraMatrixInverted";

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

		public RenderShaderComponent SetAttribute(string name, Action<int> setAction) {
			shader.Activate();
			var position = shader.GetResourceLocation(ShaderResourceType.Attribute, name);
			// Do only if found
			if (position != -1) {
				setAction?.Invoke(position);
			}
			shader.Deactivate();
			return this;
		}

		public RenderShaderComponent SetAttribute(string name, Vector2 value) {
			return SetAttribute(name, position => GL.VertexAttrib2(position, value));
		}

		public RenderShaderComponent SetUniform(string name, Action<int> setAction) {
			shader.Activate();
			var position = shader.GetResourceLocation(ShaderResourceType.Uniform, name);
			// Do only if found
			if (position != -1) {
				setAction?.Invoke(position);
			}
			shader.Deactivate();
			return this;
		}

		public RenderShaderComponent SetUniform(string name, float value) {
			return SetUniform(name, position => GL.Uniform1(position, value));
		}

		public RenderShaderComponent SetUniform(string name, Vector3 value) {
			return SetUniform(name, position => GL.Uniform3(position, value));
		}

		public override void OnDestroy() {
			base.OnDestroy();
			shader?.Dispose();
		}

		public void Render() {
			// Enable blending for transparency in textures
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Blend);

			// Color is multiplied with the texture color
			// White means no color change in the texture will be applied
			GL.Color3(Color.White);

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

			GL.Disable(EnableCap.Blend);
		}

		public virtual void Update() {
			// Assign the camera position for easy access
			SetAttribute(CAMERA_POSITION_ATTRIBUTE, position => {
				GL.VertexAttrib2(position, CameraComponent.Active.Position);
			});

			// NOTE Do this whenever needed because it did not work immediately
			// Assign the camera matrix for full access
			//SetAttribute(CAMERA_MATRIX_ATTRIBUTE, position => {
			//	var m = CameraComponent.ActiveCameraMatrixInverted.ToOpenTKMatrix();
			//	GL.VertexAttrib2(position, m.Row0);
			//	GL.VertexAttrib2(position + 2, m.Row1);
			//	GL.VertexAttrib2(position + 4, m.Row2);
			//});
			//SetAttribute(CAMERA_MATRIX_INVERTED_ATTRIBUTE, position => {
			//});
		}
	}

}
