using System.Drawing;
using System.Linq;
using Framework.Object;
using Framework.Render;
using OpenTK;
using SpaceWar.Game.Play.Player;
using SpaceWar.Resources;
using Zenseless.Geometry;
using Zenseless.OpenGL;

namespace SpaceWar.Game.Play.Field {

	public class BorderShader : RenderShaderComponent {

		public const string PLAYER_POSITION_ATTRIBUTE_NAME = "InPlayerPosition";
		public const string SHOW_DISTANCE_UNIFORM_NAME = "InShowDistance";
		public const string SHOW_COLOR_UNIFORM_NAME = "InShowColor";

		public const float SHOW_DISTANCE = 0.3f;
		public static readonly Color SHOW_COLOR = Color.White;

		public BorderShader(Box2D rect) :
			base(Resource.Border_vert, Resource.Border_frag, rect) {
		}

		public override void OnStart() {
			base.OnStart();
			SetUniform(SHOW_DISTANCE_UNIFORM_NAME, SHOW_DISTANCE);
			SetUniform(SHOW_COLOR_UNIFORM_NAME, SHOW_COLOR.ToVector3().ToOpenTK());
		}

		public override void Update() {
			base.Update();
			var positions = PlayerHelper.GetPlayers().Select((player, i) => player.Transform.WorldPosition).ToArray();
			SetAttribute(PLAYER_POSITION_ATTRIBUTE_NAME, positions[0]);
		}
	}

}
