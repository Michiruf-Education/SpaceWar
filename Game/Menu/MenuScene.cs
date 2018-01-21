using System.Drawing;
using System.Windows.Forms;
using Framework;
using Framework.Camera;
using Framework.Sound;
using OpenTK;
using SpaceWar.Game.Play;
using SpaceWar.Resources;
using Zenseless.Geometry;
using Button = Framework.Widget.Button;

namespace SpaceWar.Game.Menu {

	public class MenuScene : Scene {

		public override void OnStart() {
			base.OnStart();

			// Camera
			Spawn(new DefaultCameraGameObject());

			// Sinpleplayer button
			var singleplayerButton = new Button(
				new Box2D(-0.2f, -0.05f, 0.4f, 0.1f),
				"Singleplayer",
				Options.DEFAULT_FONT,
				Brushes.Black,
				Color.DarkGray,
				() => Framework.Game.Instance.ShowScene(new PlayScene()));
			singleplayerButton.Transform.WorldPosition = new Vector2(0, 0.15f);
			Spawn(singleplayerButton);

			// Coop button
			var coopButton = new Button(
				new Box2D(-0.2f, -0.05f, 0.4f, 0.1f),
				"CO-OP",
				Options.DEFAULT_FONT,
				Brushes.Black,
				Color.DarkGray,
				() => Framework.Game.Instance.ShowScene(new PlayScene(2)));
			coopButton.Transform.LocalPosition = new Vector2(0, 0);
			Spawn(coopButton);

			// Quit button
			var quitButton = new Button(
				new Box2D(-0.2f, -0.05f, 0.4f, 0.1f),
				"Quit",
				Options.DEFAULT_FONT,
				Brushes.Black,
				Color.DarkGray,
				() => Framework.Game.Instance.Close());
			quitButton.Transform.LocalPosition = new Vector2(0, -0.15f);
			Spawn(quitButton);

			// Licenses button
			var licensesButton = new Button(
				new Box2D(-0.1f, -0.025f, 0.2f, 0.05f),
				"View Licenses",
				Options.DEFAULT_FONT,
				Brushes.Black,
				Color.Gray,
				() => {
					new Form {
						Width = 500,
						Height = 700,
						FormBorderStyle = FormBorderStyle.FixedDialog,
						Text = String.LicenseHeadline,
						StartPosition = FormStartPosition.CenterScreen,
						Controls = {
							new Label {
								Top = 20,
								Left = 20,
								Width = 460,
								Text = String.LicenseContent
							},
							new System.Windows.Forms.Button {
								Top = 620,
								Left = 20,
								Width = 445,
								Text = String.LicenseClose,
								DialogResult = DialogResult.OK
							}
						}
					}.ShowDialog();
				});
			licensesButton.Transform.LocalPosition = new Vector2(0, -0.35f);
			Spawn(licensesButton);

			// Play the menu sound
			AudioPlayer.Get().Play(new Sound(Resource.MenuSound, SoundFormat.Mp3)
				.Volume(Options.SOUND_BACKGROUND_VOLUME)
				.Repeat(true)
				.StartSeek(2));
		}

		public override void OnDestroy() {
			AudioPlayer.Get().StopAll();
			base.OnDestroy();
		}
	}

}
