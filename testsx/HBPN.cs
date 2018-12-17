using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testsx
{
	/// <summary>
	/// 存档文件结构
	/// </summary>
	internal class StringPack
	{
		public double[][] W = null;
		public double[][] V = null;
		public double[] B1 = null;
		public double[] B2 = null;
		public int iN = 0, hN = 0, oN = 0;
		public double iR = 0;
	}

	/// <summary>
	/// 半个BP神经网络,只保留预测用的部分.
	/// </summary>
	public class HBPN
	{
		public HBPN(string json)
		{
			var stringpack = JSON.Deserialize<StringPack>(json);

			b1 = stringpack.B1;
			b2 = stringpack.B2;
			inNum = stringpack.iN;
			hideNum = stringpack.hN;
			outNum = stringpack.oN;
			in_rate = stringpack.iR;

			x = new double[inNum];
			x1 = new double[hideNum];
			x2 = new double[outNum];

			o1 = new double[hideNum];
			o2 = new double[outNum];

			w = new double[inNum, hideNum];
			v = new double[hideNum, outNum];

			b1 = new double[hideNum];
			b2 = new double[outNum];

			w = new double[stringpack.W.Length, stringpack.W[0].Length];
			v = new double[stringpack.V.Length, stringpack.V[0].Length];
			for (int i = 0; i < stringpack.W.Length; i++)
			{
				for (int j = 0; j < w.GetLength(1); j++) w[i, j] = stringpack.W[i][j];
			}
			for (int i = 0; i < stringpack.V.Length; i++)
			{
				for (int j = 0; j < v.GetLength(1); j++) v[i, j] = stringpack.V[i][j];
			}
		}

		public int inNum;//输入节点数
		int hideNum;//隐层节点数
		public int outNum;//输出层节点数

		//public int sampleNum;//样本总数
		private double[] x;//输入节点的输入数据
		private double[] x1;//隐层节点的输出
		private double[] x2;//输出节点的输出
		private double[] o1;//隐层的输入
		private double[] o2;//输出层的输入
		private double[,] w;//权值矩阵w
		private double[,] v;//权值矩阵V
		
		private double[] b1;//隐层阈值矩阵
		private double[] b2;//输出层阈值矩阵
		
		double in_rate;//归一化比例系数

		private System.Web.Script.Serialization.JavaScriptSerializer JSON = new System.Web.Script.Serialization.JavaScriptSerializer();

		//数据仿真函数
		public double[] sim(double[] psim) //in_rate inNum HideNum outNum 
		{
			for (int i = 0; i < inNum; i++) x[i] = psim[i] / in_rate;

			for (int j = 0; j < hideNum; j++)
			{
				o1[j] = 0.0;
				for (int i = 0; i < inNum; i++) o1[j] = o1[j] + w[i, j] * x[i];
				x1[j] = 1.0 / (1.0 + Math.Exp(-o1[j] - b1[j]));
			}
			for (int k = 0; k < outNum; k++)
			{
				o2[k] = 0.0;
				for (int j = 0; j < hideNum; j++) o2[k] = o2[k] + v[j, k] * x1[j];
				x2[k] = 1.0 / (1.0 + Math.Exp(-o2[k] - b2[k]));
				x2[k] = in_rate * x2[k];
			}

			return x2;
		} //end sim
	}
}
