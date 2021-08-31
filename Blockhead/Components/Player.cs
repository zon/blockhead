using MonoGame.Aseprite.Documents;

namespace Blockhead {

	public struct Player {
		public float X;
		public float Y;
		public float DX;
		public float DY;
		public AsepriteDocument Document;

		public const float Width = 0.8f;
		public const float Height = 0.8f;
		public const float Friction = 4f;
		public const float Bounce = 0.3f;
		public const float Accel = 5f;
	}

}
