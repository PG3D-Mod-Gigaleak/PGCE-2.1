using UnityEngine;

namespace FlyingText3D
{
	public class ContourData
	{
		public Vector2 maxPoint;

		public Vector2 minPoint;

		public Vector2[] points;

		public bool[] onCurves;

		public Vector2[] renderedPoints;

		public Side side;

		public ContourData(Vector2[] points, bool[] onCurves)
		{
			this.points = points;
			this.onCurves = onCurves;
		}
	}
}
