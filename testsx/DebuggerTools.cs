using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.Menu;

namespace testsx
{
	public class ConsoleDebugger
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="data"></param>
		public static void ShowUIntArray(string name, uint[] data)
		{
			Console.WriteLine($"{name} = {{");
			foreach (var item in data)
			{
				var hex = String.Format("{0:X}", item).PadLeft(8, '0');
				var outtext = $" 0x{hex} ({item}) ,";
				Console.WriteLine(outtext);
			}
			Console.WriteLine($"}} = {name}");
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public class DataItem : IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		internal MenuItem MenuItem = null;
		/// <summary>
		/// 
		/// </summary>
		public string Name = null;
		/// <summary>
		/// 
		/// </summary>
		public byte[] Buff = null;
		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			Buff = null;
			Name = null;
			MenuItem.Dispose();
		}
	}

	public class HexForm : IDisposable, ICollection<DataItem>
	{
		/// <summary>
		/// 界面
		/// </summary>
		Form ThisForm = null;
		/// <summary>
		/// 显示控件
		/// </summary>
		ByteViewer HexEditor = null;
		/// <summary>
		/// 主菜单
		/// </summary>
		MainMenu ThisMenu = null;
		/// <summary>
		/// 数据列表子菜单
		/// </summary>
		MenuItem DataMenuCollection = null;

		/// <summary>
		/// 
		/// </summary>
		public HexForm()
		{
			ThisMenu = new MainMenu();
			DataMenuCollection = new MenuItem() { Text = "数据" };
			ThisMenu.MenuItems.Add(DataMenuCollection);
			ThisForm = new Form()
			{
				Width = 650,
				Height = 800,
				MaximizeBox = false,
				FormBorderStyle = FormBorderStyle.FixedSingle,
				Menu = ThisMenu,
			};
			ThisForm.FormClosing += ThisForm_FormClosing;
			ThisForm.FormClosed += ThisForm_FormClosed;
			HexEditor = new ByteViewer()
			{
				Dock = DockStyle.Fill
			};

			var data = new byte[0];
			HexEditor.SetBytes(data);

			ThisForm.Controls.Add(HexEditor);
			ThisForm.Show();
		}

		private void ThisForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
		}

		private void ThisForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		public void Hide()
		{
			if (IsDisposed()) return;
			ThisForm.Hide();
		}

		public void Show()
		{
			if (IsDisposed()) return;
			ThisForm.Show();
		}

		public bool Visible
		{
			get
			{
				if (IsDisposed()) return false;
				return ThisForm.Visible;
			}
			set
			{
				if (IsDisposed()) return;
				ThisForm.Visible = value;
			}
		}

		private void Menuitem_Click(object sender, EventArgs e)
		{
			var dataitem = ((sender as MenuItem)?.Tag as DataItem);
			if (dataitem == null) return;
			HexEditor.SetBytes(dataitem.Buff);
		}

		#region IDisposable
		public void Dispose()
		{
			if (IsDisposed()) return;
			//清空数据
			Clear();

			//释放控件
			HexEditor.Dispose();
			HexEditor = null;

			DataMenuCollection.Dispose();
			DataMenuCollection = null;

			ThisMenu.Dispose();
			ThisMenu = null;

			//释放界面
			ThisForm.Dispose();
			ThisForm = null;
		}

		public bool IsDisposed()
		{
			return ThisForm == null;
		}
		#endregion

		#region ICollection
		public void Add(string name, byte[] buff)
		{
			Add(new DataItem() { Name = name, Buff = buff });
		}

		public int Count => DataMenuCollection.MenuItems.Count;

		public bool IsReadOnly => false;

		public void Add(DataItem item)
		{
			var menuitem = new MenuItem()
			{
				Tag = item,
				Text = item.Name,
			};
			menuitem.Click += Menuitem_Click;
			item.MenuItem = menuitem;
			DataMenuCollection.MenuItems.Add(menuitem);
		}

		public void Clear()
		{
			var datas = (from MenuItem menu in DataMenuCollection.MenuItems select menu.Tag as DataItem);
			//清空菜单列表
			DataMenuCollection.MenuItems.Clear();
			//释放每一个菜单
			foreach (var item in datas) item.Dispose();
		}

		public bool Contains(DataItem item)
		{
			foreach (var dataitem in (from MenuItem menu in DataMenuCollection.MenuItems select menu.Tag as DataItem)) if (dataitem == item) return true;
			return false;
		}

		public void CopyTo(DataItem[] array, int arrayindex)
		{
			var datas = (from MenuItem menu in DataMenuCollection.MenuItems select menu.Tag as DataItem).Skip(arrayindex).Take(array.Length).ToArray();
			for (int i = 0; i < array.Length; i++) array[i] = datas[i];
		}

		public IEnumerator<DataItem> GetEnumerator() => (from MenuItem menu in DataMenuCollection.MenuItems select menu.Tag as DataItem).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => (from MenuItem menu in DataMenuCollection.MenuItems select menu.Tag).GetEnumerator();

		public bool Remove(DataItem item)
		{
			foreach (var dataitem in (from MenuItem menu in DataMenuCollection.MenuItems select menu.Tag as DataItem))
			{
				if (dataitem != item) continue;
				DataMenuCollection.MenuItems.Remove(dataitem.MenuItem);
				dataitem.Dispose();
				return true
				;
			}
			return false;
		}
		#endregion
	}//End Class
}
