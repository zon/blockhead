using MonoGame.Aseprite.Documents;

namespace Blockhead {

	public struct Player {
		public float X;
		public float Y;
		public float Width;
		public float Height;
		public float DX;
		public float DY;
		public float Grounded;
		public int Facing;
		public const float Bounce = 0.05f;
		public const float Gravity = 9.8f;
		public const float AccelTime = 0.5f;
		public const float MaxVelocity = 5;
		public const float Friction = 5 / AccelTime;
		public const float Accel = MaxVelocity * Friction;
		public const float Jump = -8;
		public const float MinDX = 0.1f;

		public static Player Create(float x, float y) {
			return new Player {
				X = x,
				Y = y,
				Width = 0.5f,
				Height = 0.9375f,
				Facing = 1
			};
		}

	}

}
