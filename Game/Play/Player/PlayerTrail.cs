using System;
using System.Drawing;
using System.Text;
using Framework;
using Framework.Camera;
using Framework.Object;
using Framework.Render;
using Framework.Utilities;
using OpenTK.Graphics.OpenGL;
using SpaceWar.Resources;
using Zenseless.Geometry;
using Zenseless.HLGL;
using Zenseless.OpenGL;

namespace SpaceWar.Game.Play.Player {

	public class PlayerTrail : Component, RenderComponent {

		private readonly FBO fbo;
		private readonly Shader shader;

		private Player player;

		public PlayerTrail() {
			var window = Framework.Game.Instance.Window;
			fbo = new FBO(Texture2dGL.Create(window.Width, window.Height));
			fbo.Texture.WrapFunction = TextureWrapFunction.MirroredRepeat;
			try {
				shader = ShaderLoader.FromStrings(
					DefaultShader.VertexShaderScreenQuad,
					DefaultShader.FragmentShaderCopy);
			} catch (ShaderException e) {
				Console.WriteLine(e.ShaderLog);
			}
		}

		public override void OnStart() {
			base.OnStart();
			player = GameObject as Player;
		}

		public void Render() {
			var cameraTranslation = CameraComponent.Active.Position;
			GL.Translate(cameraTranslation.X, cameraTranslation.Y, 0);

			// Draw the normal stuff into the fbo
			fbo.Activate();
			player.GetComponent<RenderTextureComponent>().Render();
//			var v = new RenderTextureComponent(Resource.heart, Player.PLAYER_SIZE * 10f, Player.PLAYER_SIZE * 10f);
//			v.Render();
			fbo.Deactivate();

			GL.Translate(-cameraTranslation.X, -cameraTranslation.Y, 0);


			// Draw the fbo
			GL.Enable(EnableCap.Texture2D);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Blend);
			fbo.Texture.Activate();
			shader.Activate();
			DrawWindowFillingQuad();
			shader.Deactivate();
			fbo.Texture.Deactivate();
			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.Texture2D);
		}

		private static void DrawWindowFillingQuad() {
			// @see https://github.com/danielscherzer/Framework/blob/master/CG/Examples/
			// PostProcessingExample/PostProcessingVisual.cs
			GL.DrawArrays(PrimitiveType.Quads, 0, 4);
		}
	}

}
