using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testsx
{
	public class EditorDistance
	{
		/// <summary>
		/// 比较两个字符串的相似度，并返回相似率。
		/// </summary>
		/// <param name="str1"></param>
		/// <param name="str2"></param>
		/// <returns></returns>
		public static double Levenshtein(string str1, string str2)
		{
			var char1 = str1.ToCharArray();
			var char2 = str2.ToCharArray();
			//计算两个字符串的长度。
			int len1 = char1.Length;
			int len2 = char2.Length;
			//建二维数组，比字符长度大一个空间
			var dif = new int[len1 + 1, len2 + 1];
			//赋初值
			for (int a = 0; a <= len1; a++) dif[a, 0] = a;
			for (int a = 0; a <= len2; a++) dif[0, a] = a;
			//计算两个字符是否一样，计算左上的值
			int temp;
			for (int i = 1; i <= len1; i++)
			{
				for (int j = 1; j <= len2; j++)
				{
					temp = (char1[i - 1] == char2[j - 1]) ? 0 : 1;
					//取三个值中最小的
					dif[i, j] = new[] { dif[i - 1, j - 1] + temp, dif[i, j - 1] + 1, dif[i - 1, j] + 1 }.Min();
				}
			}
			//计算相似度
			double similarity = 1 - ((double)dif[len1, len2] / Math.Max(len1, len2));
			return similarity;
		}
	}
}