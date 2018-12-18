using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BPNet;
namespace testsx
{
	public partial class Form1 : Form
	{
		#region 变量
		/// <summary>
		/// 笔画时间(暂时用处不大)
		/// </summary>
		private static int DrawTime = 0;

		/// <summary>
		/// 存放笔画数据的缓冲区
		/// </summary>
		private List<List<Point>> ArrayPtBuff = new List<List<Point>>();

		/// <summary>
		/// 当前正在画的这一笔的缓冲区
		/// </summary>
		private List<Point> PtBuff = null;
		#endregion

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//加载之前采集到一半的数据接着采集
			//TrainData.LoadData("Data.json");
		}

		public void Clear()
		{
			DrawTime = 0;
			PtBuff = null;
			ArrayPtBuff.Clear();
			pictureBox1.Invalidate();
		}

		#region 根据屏幕触摸反馈,记录笔画数据
		private int OldX = 0, OldY = 0;

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			OldX = e.X;
			OldY = e.Y;
			PtBuff = new List<Point>();
			ArrayPtBuff.Add(PtBuff);
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && PtBuff != null)
			{
				DrawTime++;
				PtBuff.Add(new Point(e.X, e.Y));
				pictureBox1.Invalidate();
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
		}
		#endregion

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
			e.Graphics.DrawString("手写区域", new Font("黑体", 48, FontStyle.Bold), Brushes.Gray, new PointF(75, 135));
			#region 画框线
			e.Graphics.DrawRectangle(new Pen(Color.Black, 1), 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
			var pen = new Pen(Color.Gray, 1);
			pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
			pen.DashPattern = new float[] { 10, 2 };
			e.Graphics.DrawLine(pen, new Point(0, 0), new Point(pictureBox1.Width, pictureBox1.Height));
			e.Graphics.DrawLine(pen, new Point(pictureBox1.Width, 0), new Point(0, pictureBox1.Height));
			e.Graphics.DrawLine(pen, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));
			e.Graphics.DrawLine(pen, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
			#endregion
			if (ArrayPtBuff.Count <= 0) return;
			foreach (var lp in ArrayPtBuff)
			{
				if (lp.Count <= 1) continue;
				e.Graphics.DrawLines(new Pen(Color.Red, 2), lp.ToArray());
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MatrixData.Clear();
			MessageBox.Show("OK");
			return;
		}

		private void caiji_Click(object sender, EventArgs e)
		{
			var pointdd = new Point[ArrayPtBuff.Count][];
			for (int i = 0; i < ArrayPtBuff.Count; i++)
			{
				pointdd[i] = ArrayPtBuff[i].ToArray();
			}

			MatrixData.RecordOne(pointdd);

			Clear();

			MessageBox.Show("采集成功!");
		}

		MatrixData MatrixData = new MatrixData();

		private void save_Click(object sender, EventArgs e)
		{
			MatrixData.SaveData("Data.json");
			MessageBox.Show("保存成功!");
		}

		private static double[][,] GetMatrix(string file)
		{
			var traindata = new MatrixData();
			traindata.LoadData(file);
			return traindata.ToDoubleMatrix(new Size(16, 16));
		}

		/// <summary>
		/// 获取1维的矩阵
		/// </summary>
		/// <param name="td"></param>
		/// <returns></returns>
		private double[,] GetDim1Matrix(MatrixData td)
		{
			var matrix = td.ToDoubleMatrix(new Size(16, 16));
			var dim1matrix = new double[matrix.Length, matrix[0].Length];
			for (int i = 0; i < dim1matrix.GetLength(0); i++)
			{
				var buff = new List<double>();
				int point = 0;
				for (int x = 0; x < matrix[i].GetLength(0); x++)
				{
					for (int y = 0; y < matrix[i].GetLength(1); y++)
					{
						dim1matrix[i, point] = matrix[i][x, y];
						point++;
					}
				}
			}
			return dim1matrix;
		}

		/// <summary>
		/// 字典里每个字的数据
		/// </summary>
		public class CharData
		{
			/// <summary>
			/// 字符
			/// </summary>
			public char TheChar = ' ';
			/// <summary>
			/// 手写编码
			/// </summary>
			public string[] Code = new string[] { };
			/// <summary>
			/// 距离
			/// </summary>
			public double distance = 0;
		}

		List<CharData> Dict = null;


		private void InitDict()
		{
			if (Dict == null)
			{
				Dict = new List<CharData>();
				var conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Dict.mdb"); //Jet OLEDB:Database Password=
				var cmd = conn.CreateCommand();

				cmd.CommandText = "select * from T_hz where FrequencyOfUse>0";
				conn.Open();
				var dr = cmd.ExecuteReader();
				var dt = new DataTable();
				if (dr.HasRows)
				{
					for (int i = 0; i < dr.FieldCount; i++)
					{
						dt.Columns.Add(dr.GetName(i));
					}
					dt.Rows.Clear();
				}
				while (dr.Read())
				{
					DataRow row = dt.NewRow();
					for (int i = 0; i < dr.FieldCount; i++)
					{
						row[i] = dr[i];
					}
					dt.Rows.Add(row);
				}
				cmd.Dispose();
				conn.Close();
				foreach (DataRow item in dt.Rows)
				{
					Dict.Add(new CharData() { TheChar = item["汉字"].ToString()[0], Code = new string[] { item["笔顺"].ToString() } });
				}

			}
		}
		
		//识别
		private void label3_Click(object sender, EventArgs e)
		{
			#region 准备字典
			InitDict();
			var dict = Dict.ToArray();
			#endregion

			#region 把每个笔画录入矩阵数据
			var pointdd = new Point[1][];
			var traindata = new MatrixData();
			for (int i = 0; i < ArrayPtBuff.Count; i++)
			{
				pointdd[0] = ArrayPtBuff[i].ToArray();
				traindata.RecordOne(pointdd);
			}
			#endregion

			//识别的数据
			var test = GetDim1Matrix(traindata);
			//笔画识别结果
			var sortresult = "";

			#region 让神经网络加载训练结果并进行识别
			var hbpn = new HBPN(File.ReadAllText("GData.json"));
			for (int i = 0; i < test.GetLength(0); i++)
			{
				var sim0 = hbpn.sim(test.GetColumn(i));
				int like = 0;
				var distant = double.MaxValue;
				for (int k = 0; k < sim0.Length; k++)
				{
					if (Math.Abs(sim0[k] - 1) > distant) continue;
					distant = Math.Abs(sim0[k] - 1);
					like = k;
				}

				//result += "㇐㇑㇀㇏㇕㇗㇡"[like];
				sortresult += "123455555555555"[like];
			}
			#endregion

			#region 这里计算字典中每个字与识别结果的相似度
			for (int i = 0; i < dict.Length; i++)
			{
				dict[i].distance = EditorDistance.Levenshtein(dict[i].Code[0], sortresult);
			}
			#endregion

			#region 根据相似度,对字典进行排序
			for (int i = 0; i < dict.Length; i++)
			{
				for (int j = i + 1; j < dict.Length; j++)
				{

					if (dict[i].distance < dict[j].distance)
					{
						var temp = dict[i];
						dict[i] = dict[j];
						dict[j] = temp;
					}
				}
			}
			#endregion

			//字符识别结果
			var result = "";

			#region 根据相似度排序
			for (int i = 0; i < 20; i++)
			{
				result += dict[i].TheChar.ToString();
			}
			#endregion
			
			MessageBox.Show(result, sortresult);

			Clear();
			traindata.Clear();
		}

		private void ClearButt_Click(object sender, EventArgs e)
		{
			Clear();
		}

	}
}
