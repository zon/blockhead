using MonoGame.Aseprite.Documents;

namespace Blockhead {

	public struct Player {
		public float X;
		public float Y;
		public float Width;
		public float Height;
		public float DX;
		public float DY;
		public const float Friction = 4f;
		public const float Bounce = 0.3f;
		public const float Accel = 5f;

		public static Player Create(float x, float y) {
			return new Player {
				X = x,
				Y = y,
				Width = 0.5f,
				Height = 0.9375f
			};
		}

	}

}
