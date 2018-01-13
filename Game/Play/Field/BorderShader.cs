using System.Drawing;
using System.Linq;
using Framework.Object;
using Framework.Render;
using OpenTK;
using SpaceWar.Game.Play.Player;
using Zenseless.Geometry;
using Zenseless.OpenGL;

namespace SpaceWar.Game.Play.Field {

	public class BorderShader : RenderShaderComponent, UpdateComponent {

		public const string PLAYER_POSITION_ATTRIBUTE_NAME = "InPlayerPosition";
		public const string SHOW_DISTANCE_UNIFORM_NAME = "InShowDistance";
		public const string SHOW_COLOR_UNIFORM_NAME = "InShowColor";

		public const float showDistance = 0.1f;
		public static readonly Color showColor = Color.Red;

		public BorderShader(byte[] vertString, byte[] fragString, Box2D rect) :
			base(vertString, fragString, rect) {
		}

		public override void OnStart() {
			base.OnStart();
			SetUniform(SHOW_DISTANCE_UNIFORM_NAME, showDistance);
			SetUniform(SHOW_COLOR_UNIFORM_NAME, showColor.ToVector3().ToOpenTK());
		}

		public void Update() {
			var positions = PlayerHelper.GetPlayers().Select((player, i) => player.Transform.WorldPosition).ToArray();
			SetAttribute(PLAYER_POSITION_ATTRIBUTE_NAME, positions[0]);
		}
	}

}
