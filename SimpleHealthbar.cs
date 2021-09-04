using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;


namespace LansSimpleHealthbar
{
	public class SimpleHealthbar : Mod
	{
		public SimpleHealthbar()
		{

		}
	}

	public class SimpleHealthbarPlayer : ModPlayer
	{
		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			var num25 = Main.myPlayer;
			base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);

			int num26 = 0;
			if (Main.HealthBarDrawSettings == 1)
			{
				num26 = 10;
			}
			else if (Main.HealthBarDrawSettings == 2)
			{
				num26 = -20;
			}
			int x = (int)(Main.player[num25].position.X + (float)(Main.player[num25].width / 2));
			int y = (int)(Main.player[num25].position.Y + (float)Main.player[num25].height + Main.player[num25].gfxOffY);
			int dy = y + num26;
			int health = Main.player[num25].statLife;
			int maxHealth = Main.player[num25].statLifeMax2;
			var brightness = Lighting.Brightness((int)(x / 16f), (int)(y / 16f));

			if (Main.player[num25].active && !Main.player[num25].ghost && !Main.player[num25].dead)
			{
				if (Main.player[num25].statLife != Main.player[num25].statLifeMax2)
				{
					Main.instance.DrawHealthBar(x, dy, health, maxHealth, brightness, 1f);
				}

				dy += 10;
				if (GetInstance<Config>().ShowMana)
				{
					if (Main.player[num25].statMana != Main.player[num25].statManaMax2)
					{
						DrawManaBar(x, dy, Main.player[num25].statMana, Main.player[num25].statManaMax2, 1f);
					}
				}
				if (GetInstance<Config>().ShowPotionSickness)
				{
					if (Main.player[num25].statLife != Main.player[num25].statLifeMax2 || GetInstance<Config>().ShowPotionSicknessAtFullHealth)
					{
						for (int i = 0; i < Main.player[num25].buffType.Length; i++)
						{
							if (Main.player[num25].buffType[i] == BuffID.PotionSickness)
							{
								DrawPotionSickness(x, dy, Main.player[num25].buffTime[i]);
							}
						}
					}
				}
			}

		}

		public void DrawPotionSickness(float X, float Y, int left)
		{
			if (left > 0)
			{

				int offsetX = -50;
				int offsetY = -10;
				var pos = new Vector2(X + offsetX - Main.screenPosition.X, Y + offsetY - Main.screenPosition.Y);
				Main.spriteBatch.Draw(TextureAssets.Buff[BuffID.PotionSickness].Value, new Vector2(X+offsetX - Main.screenPosition.X, Y+offsetY - Main.screenPosition.Y), 
					null, new Color(1, 1, 1, 0.1f), 0f, Vector2.Zero, 24/32f, SpriteEffects.None, 0f);


				string text = "" + (left / 60);

				var stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, Vector2.One);
				int stringOffsetX = (int) ((24- stringSize.X) /2f);
				
				int stringOffsetY = (int)((24 - stringSize.Y)/2f)+4;

				Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.ItemStack.Value, text, pos.X+stringOffsetX, pos.Y + stringOffsetY, Color.Yellow, Color.Black, new Vector2(0.3f), 1f);
			}
		}

		public void DrawManaBar(float X, float Y, int Mana, int MaxMana, float alpha, float scale = 1f)
		{
			if (Mana <= 0)
			{
				return;
			}
			float num = (float)Mana / (float)MaxMana;
			if (num > 1f)
			{
				num = 1f;
			}
			int num2 = (int)(36f * num);
			float num3 = X - 18f * scale;
			float num4 = Y;
			if (Main.player[Main.myPlayer].gravDir == -1f)
			{
				num4 -= Main.screenPosition.Y;
				num4 = Main.screenPosition.Y + (float)Main.screenHeight - num4;
			}
			float num5 = 0f;
			float num6 = 255f;
			num -= 0.1f;
			float num7;
			float num8;

			num8 = 255f;
			num7 = 255f * (1f - num);

			float darkr = 255f - 0.8f * (255f * (1 - num));
			float darkb = 255f - 0.8f * (255f * (1 - num));

			num7 = System.Math.Min(num7, darkr);
			num8 = System.Math.Min(num8, darkb);
			/*
			if ((double)num > 0.5)
			{
				num8 = 255f;
				num7 = 255f * (1f - num) * 2f;
			}
			else
			{
				num7 = 255f - 0.5f*(255f* (1-num));
				num8 = 255f - 0.5f * (255f * (1 - num));
			}*/
			float num9 = 0.95f;
			num8 = num8 * alpha * num9;
			num7 = num7 * alpha * num9;
			num6 = num6 * alpha * num9;
			if (num8 < 0f)
			{
				num8 = 0f;
			}
			if (num8 > 255f)
			{
				num8 = 255f;
			}
			if (num7 < 0f)
			{
				num7 = 0f;
			}
			if (num7 > 255f)
			{
				num7 = 255f;
			}
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			if (num6 > 255f)
			{
				num6 = 255f;
			}
			Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color((int)((byte)num7), (int)((byte)num5), (int)((byte)num8), (int)((byte)num6));
			if (num2 < 3)
			{
				num2 = 3;
			}
			if (num2 < 34)
			{
				if (num2 < 36)
				{
					Main.spriteBatch.Draw(TextureAssets.Hb2.Value, new Vector2(num3 - Main.screenPosition.X + (float)num2 * scale, num4 - Main.screenPosition.Y), 
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(2, 0, 2, TextureAssets.Hb2.Value.Height)), color, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);
				}
				if (num2 < 34)
				{
					Main.spriteBatch.Draw(TextureAssets.Hb2.Value, new Vector2(num3 - Main.screenPosition.X + (float)(num2 + 2) * scale, num4 - Main.screenPosition.Y),
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num2 + 2, 0, 36 - num2 - 2, TextureAssets.Hb2.Value.Height)), color, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);
				}
				if (num2 > 2)
				{
					Main.spriteBatch.Draw(TextureAssets.Hb1.Value, new Vector2(num3 - Main.screenPosition.X, num4 - Main.screenPosition.Y), 
						new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, num2 - 2, TextureAssets.Hb1.Value.Height)), color, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);
				}
				Main.spriteBatch.Draw(TextureAssets.Hb1.Value, new Vector2(num3 - Main.screenPosition.X + (float)(num2 - 2) * scale, num4 - Main.screenPosition.Y), 
					new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(32, 0, 2, TextureAssets.Hb1.Value.Height)), color, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);
				return;
			}
			if (num2 < 36)
			{
				Main.spriteBatch.Draw(TextureAssets.Hb2.Value, new Vector2(num3 - Main.screenPosition.X + (float)num2 * scale, num4 - Main.screenPosition.Y), 
					new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num2, 0, 36 - num2, TextureAssets.Hb2.Value.Height)), color, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);
			}
			Main.spriteBatch.Draw(TextureAssets.Hb1.Value, new Vector2(num3 - Main.screenPosition.X, num4 - Main.screenPosition.Y), 
				new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, num2, TextureAssets.Hb1.Value.Height)), color, 0f, new Vector2(0f, 0f), scale, SpriteEffects.None, 0f);

		}
	}
}