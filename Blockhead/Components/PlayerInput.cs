namespace Blockhead {

	public struct PlayerInput {
		public int MoveX;
		public int MoveY;
		public float Jump;

		public const float Flex = 0.2f;

		public static PlayerInput Create() {
			return new PlayerInput {
				Jump = float.MaxValue
			};
		}

	}

}
