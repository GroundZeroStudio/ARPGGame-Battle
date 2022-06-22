using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class WayLine
	{
		/// <summary>
		/// 路线路径点
		/// </summary>
		public Vector3[] Points { get; set; }

		public WayLine(int pointCount)
        {
			Points = new Vector3[pointCount];
        }
	}
}

