using System.Drawing;
using System.Linq;
using Framework.Camera;
using Framework.Object;
using Framework.Render;
using OpenTK;
using SpaceWar.Game.Play.Player;
using SpaceWar.Resources;
using Zenseless.Geometry;
using Zenseless.OpenGL;

namespace SpaceWar.Game.Play.Field {

	public class BorderShader : RenderShaderComponent, UpdateComponent {

		public const string PLAYER_POSITION_ATTRIBUTE_NAME = "InPlayerPosition";
		public const string SHOW_DISTANCE_UNIFORM_NAME = "InShowDistance";
		public const string SHOW_COLOR_UNIFORM_NAME = "InShowColor";
		public const string EASE_START_UNIFORM_NAME = "EaseStart";
		public const string EASE_END_UNIFORM_NAME = "EaseEnd";
		public const string EASE_P1_UNIFORM_NAME = "EaseP1";
		public const string EASE_P2_UNIFORM_NAME = "EaseP2";

		public const float SHOW_DISTANCE = 0.3f;
		public static readonly Color SHOW_COLOR = Color.White;
		public static readonly Vector2 EASE_START = new Vector2(0, 0);
		public static readonly Vector2 EASE_END = new Vector2(1, 1);
		public static readonly Vector2 EASE_P1 = new Vector2(0.335f, 0.0f);
		public static readonly Vector2 EASE_P2 = new Vector2(0.125f, 1.0f);

		public BorderShader(Box2D rect) :
			base(Resource.Border_vert, Resource.Border_frag, rect) {
		}

		public override void OnStart() {
			base.OnStart();
			SetUniform(SHOW_DISTANCE_UNIFORM_NAME, SHOW_DISTANCE);
			SetUniform(SHOW_COLOR_UNIFORM_NAME, SHOW_COLOR.ToVector3().ToOpenTK());
			SetUniform(EASE_START_UNIFORM_NAME, EASE_START);
			SetUniform(EASE_END_UNIFORM_NAME, EASE_END);
			SetUniform(EASE_P1_UNIFORM_NAME, EASE_P1);
			SetUniform(EASE_P2_UNIFORM_NAME, EASE_P2);
		}

		public void Update() {
			var offscreenVector = new Vector2(100f, 100f);

			var cameraPosition = CameraComponent.Active.Position;
			var positions = PlayerHelper.GetPlayers()
				.Select((player, i) =>
					// Currently the camera is included in the borders vertex parameters.
					// Because of this we need to revert this data or also add it to the player.
					// The camera data is inverted, so there is not a "+", but a "-"!
						player.Transform.WorldPosition - cameraPosition
				)
				.Concat(new[] {offscreenVector, offscreenVector, offscreenVector, offscreenVector})
				//.Take(4)
				.ToArray();
			SetUniform(PLAYER_POSITION_ATTRIBUTE_NAME + "0", positions[0]);
			SetUniform(PLAYER_POSITION_ATTRIBUTE_NAME + "1", positions[1]);
			SetUniform(PLAYER_POSITION_ATTRIBUTE_NAME + "2", positions[2]);
			SetUniform(PLAYER_POSITION_ATTRIBUTE_NAME + "3", positions[3]);
		}
	}

}
