using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Globalization;

namespace testsx
{
	/// <summary>
	/// 用于表示一个矩形4条边的位置
	/// </summary>
	[Serializable]
	public class Side
	{
		[Browsable(true)]
		public int Left;

		[Browsable(true)]
		public int Top;
		[Browsable(true)]
		public int Right;

		[Browsable(true)]
		public int Bottom;

		public Side()
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static class Extends
	{
		/// <summary>
		/// 按角度计算的Acos
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static double Acos(this double d)
		{
			return Math.Acos(d) * 180 / Math.PI;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="arr"></param>
		/// <param name="another_column_index"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public static T[] GetColumn<T>(this T[,] arr, int another_column_index, int column = 1)
		{
			var subarr = new T[arr.GetLength(column)];
			for (int i = 0; i < subarr.Length; i++)
			{
				if (column == 0) subarr[i] = arr[i, another_column_index];
				if (column == 1) subarr[i] = arr[another_column_index, i];
			}
			return subarr;
		}

		/// <summary>
		/// 按角度计算的Sin
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static double Sin(this double d)
		{
			var ret = Math.Sin((d * Math.PI) / 180);
			return ret;
		}

		/// <summary>
		/// 按角度计算的Cos
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static double Cos(this double d)
		{
			return Math.Cos((d * Math.PI) / 180);
		}


		/// <summary>
		/// 寻边
		/// </summary>
		/// <param name="strokes"></param>
		/// <returns></returns>
		public static Side FindSide(this Point[][] strokes)
		{
			var side = new Side();
			if (strokes == null) return null;
			if (strokes.Length <= 0) return null;
			side.Left = int.MaxValue;
			side.Top = int.MaxValue;
			side.Bottom = int.MinValue;
			side.Right = int.MinValue;
			foreach (var points in strokes)
			{
				foreach (var point in points)
				{
					var x = point.X;
					var y = point.Y;
					if (x == 0xFFFF) continue;
					if (y == 0xFFFF) break;
					if (x < side.Left) side.Left = x;
					if (x > side.Right) side.Right = x;
					if (y < side.Top) side.Top = y;
					if (y > side.Bottom) side.Bottom = y;
				}
			}
			return side;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		public static double Distance(Point p1, Point p2)
		{
			var distant_x = Math.Pow(p2.X - p1.X, 2);//两点在横轴上的距离
			var distant_y = Math.Pow(p2.Y - p1.Y, 2);//两点在竖轴上的距离

			return Math.Sqrt(distant_x + distant_y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static UInt16 LowWord(this UInt32 u32)
		{
			return (UInt16)u32;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static UInt16 HighWord(this UInt32 u32)
		{
			return (UInt16)(u32 >> 16);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static class HandWritingRecognition
	{
		/// <summary>
		/// 寻边
		/// </summary>
		/// <param name="strokes"></param>
		/// <param name="rect"></param>
		/// <returns></returns>
		public static bool FindSide(uint[] strokes, ref Side side)
		{
			if (strokes == null) return false;
			if (strokes.Length <= 0) return false;
			if (side == null) side = new Side();
			side.Left = int.MaxValue;
			side.Top = int.MaxValue;
			side.Bottom = int.MinValue;
			side.Right = int.MinValue;
			foreach (var item in strokes)
			{
				var x = item.LowWord();
				var y = item.HighWord();
				if (x == 0xFFFF) continue;
				if (y == 0xFFFF) break;
				if (x < side.Left) side.Left = x;
				if (x > side.Right) side.Right = x;
				if (y < side.Top) side.Top = y;
				if (y > side.Bottom) side.Bottom = y;
			}
			return true;
		}

		/// <summary>
		/// 这个Function还没看懂是做啥的. 似乎与 缩放、切割 图形有关
		/// </summary>
		/// <param name="strokes"></param>
		/// <param name="target_width"></param>
		/// <param name="target_height"></param>
		/// <param name="rect"></param>
		/// <returns></returns>
		public static bool Sub_10006E80(ref uint[] strokes, int target_width, int target_height, ref Side pixelside)
		{
			if (strokes == null) return false;
			if (strokes.Length <= 0) return false;
			if (pixelside == null) FindSide(strokes, ref pixelside);

			var pixelsize = new Size(pixelside.Right - pixelside.Left, pixelside.Bottom - pixelside.Top);
			var dataside = new Side() { Left = pixelside.Left * 2, Top = pixelside.Top * 2 };
			//这里也许是在尝试把图片拉成正方形
			if (pixelsize.Width < pixelsize.Height)
			{
				dataside.Left += pixelsize.Width - pixelsize.Height;
				pixelsize.Width = pixelsize.Height;
			}
			else if (pixelsize.Width > pixelsize.Height)
			{
				dataside.Top += pixelsize.Height - pixelsize.Width;
				pixelsize.Height = pixelsize.Width;
			}
			else if (pixelsize.Width == 0 && pixelsize.Height == 0)
			{
				pixelsize.Width = 4;
				pixelsize.Height = 4;
				dataside.Left = -4;
				dataside.Top = -4;
			}
			var datasize = new Size(2 * pixelsize.Width, 2 * pixelsize.Height);
			var data_target_size = new Size(2 * target_width - 2, 2 * target_height - 2);
			int quad_width = 4 * pixelsize.Width;
			for (int i = 0; strokes[i].HighWord() != 0xFFFF; i++)
			{
				UInt32 highword = strokes[i].HighWord();
				UInt32 lowword = strokes[i].LowWord();
				if (lowword == 0xFFFF) continue;
				lowword = (UInt32)((datasize.Width + data_target_size.Width * (2 * lowword - dataside.Left)) / quad_width);
				highword = (UInt32)((datasize.Height + data_target_size.Height * (2 * highword - dataside.Top)) / (2 * datasize.Height));
				strokes[i] = ((highword << 16) + lowword);
			}

			return true;
		}

		/// <summary>
		/// 似乎是用于把笔画拉正的?
		/// </summary>
		/// <param name="strokes"></param>
		/// <returns></returns>
		public static bool Sub_10006E20(ref uint[] strokes)
		{
			if (strokes == null) return false;
			if (strokes.Length <= 0) return false;
			var point = new Point(strokes[0].LowWord(), strokes[0].HighWord());
			var previouspoint = new Point(0, 0xFFFF);
			int v7 = 0;
			for (int i = 1; point.Y != 0xFFFF; i++)
			{
				if (point.X != previouspoint.X || point.Y != previouspoint.Y)
				{
					strokes[v7] = (UInt32)((strokes[v7].HighWord() << 16) + point.X);
					v7++;
				}
				previouspoint.X = point.X;
				previouspoint.Y = point.Y;
				point.X = strokes[i].LowWord();
				point.Y = strokes[i].HighWord();
			}
			strokes[strokes.Length - 1] = 0xFFFFFFFF;
			return true;
		}

		/// <summary>
		/// 未解码
		/// </summary>
		/// <param name="strokes"></param>
		/// <returns></returns>
		public static bool Sub_10006FC0(ref uint[] strokesdata, ref Side pixelside)
		{
			if (strokesdata == null) return false;
			if (strokesdata.Length <= 0) return false;
			pixelside = new Side();
			var strokes = new Point(strokesdata[0].LowWord(), strokesdata[0].HighWord());
			for (int i = 1; strokesdata[i] != 0xFFFFFFFF; i++)
			{
				var next_strokes = new Point(strokesdata[i].LowWord(), strokesdata[i].HighWord());
				if (next_strokes.X != -1 && next_strokes.Y != -1)
				{

				}
			}
			return true;
		}

		public class SomeClass
		{
			public short a;
			public int b;
			public SomeClass PreviouNode = null;
			public SomeClass NextNode = null;
		}

		/// <summary>
		/// Sub_10006AB0 创建了一个奇怪的循环链表,用途不明
		/// </summary>
		/// <param name="a1"></param>
		/// <param name="a2"></param>
		/// <returns></returns>
		public static void MakeSomeChain(ref SomeClass[] a1)
		{
			for (int i = 0; i < a1.Length; i++)
			{
				a1[i] = new SomeClass();
				if (i > 0)
				{
					a1[i - 1].NextNode = a1[i];
					a1[i].PreviouNode = a1[i - 1];
				}
				a1[i].a = -1;
				a1[i].b = -1;
			}
			a1.Last().NextNode = a1[0];
			a1[0].PreviouNode = a1.Last();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strokes"></param>
		/// <param name="target_width"></param>
		/// <param name="target_height"></param>
		/// <param name="pixelside"></param>
		public static bool TranslationStrokes(ref uint[] strokesdata, int target_width, int target_height, ref Side pixelside)
		{
			if (strokesdata == null) return false;
			if (strokesdata.Length <= 0) return false;
			Sub_10006E80(ref strokesdata, 96, 96, ref pixelside);
			Sub_10006E20(ref strokesdata);
			Side sidedata = null;
			FindSide(strokesdata, ref sidedata);
			if (5 * (sidedata.Right - sidedata.Left + 1) < 96 || 5 * (sidedata.Bottom - sidedata.Top + 1) < 96)
			{
				pixelside = null;
				Sub_10006E80(ref strokesdata, target_width, target_width, ref pixelside);
				return Sub_10006E20(ref strokesdata);
			}
			else
			{//这部分未解码

			}
			return true;

		}

		public static string Recongition(uint[] strokesdata)
		{
			Side pixelside = null;
			TranslationStrokes(ref strokesdata, 64, 64, ref pixelside);
			var chain1 = new SomeClass[101];
			MakeSomeChain(ref chain1);
			return "";
		}

	}//End Class
}
