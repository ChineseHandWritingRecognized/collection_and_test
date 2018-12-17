using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace testsx
{
	static class Program
	{
		public static HexForm HexForm = new HexForm();
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			HexForm.Hide();
			//ConsoleDebugger.ShowUIntArray("测试", new uint[] { 1, 255, 43, 66554 });
			/*
			
			f.Add("测试数据1", new byte[] { 0x12, 0xD4, 0x5A, 0x25, 0x6F, 0x33 });
			f.Add("测试数据2", new byte[] { 0x25, 0x6F, 0x33 });
			f.Remove(f.First());
			while (!f.IsDisposed())
			{
				Application.DoEvents();
				Thread.Sleep(100);
			}
			return;
			// */
			//Console.Title = "";
			//Console.WriteLine("123");
			new Form1().ShowDialog();
		}
	}
}