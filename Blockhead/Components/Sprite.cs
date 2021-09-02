using MonoGame.Aseprite.Documents;
using Basegame.Client;
using Microsoft.Xna.Framework;

namespace Blockhead {

	public struct Sprite {
		public AsepriteDocument Document;
		public AsepriteTag Tag;
		public int FrameIndex;
		public float FrameRate;
		public float Interval;

		public static Sprite Create(AsepriteDocument document, string tag) {
			return new Sprite {
				Document = document,
				Tag = document.Tags[tag],
				FrameRate = 0.08f,
			};
		}

		public Rectangle GetFrame() {
			return Document.Frames[FrameIndex].ToRectangle();
		}

		public void Play(string tag) {
			Tag = Document.Tags[tag];
			FrameIndex = 0;
			Interval = 0;
		}

		public void Update(float dt) {
			Interval += dt;
			while (Interval > 1) {
				Interval -= 1;
				FrameIndex = (FrameIndex + 1) % (Tag.From - Tag.To + 1);
			} 
		}


	}

}