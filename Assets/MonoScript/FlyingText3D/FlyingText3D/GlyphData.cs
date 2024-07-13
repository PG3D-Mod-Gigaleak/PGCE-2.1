using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlyingText3D
{
	public class GlyphData
	{
		private int _glyphIndex;

		private bool _isVisibleChar = false;

		private int _resolution;

		private float _scaleFactor;

		private float _extrudeDepth = 0f;

		private int _vertexCount;

		private int _triCount;

		private int _triCount2;

		private Vector3[] _baseVertices;

		private Vector3[] _vertices;

		private int[] _baseTriangles;

		private int[] _triangles;

		private int[] _triangles2;

		private bool _useSubmesh;

		private bool _useBack;

		private bool _triDataComputed = false;

		private int _frontTriIndex;

		private int _frontVertIndex;

		private int _xMin;

		private int _yMin;

		private int _xMax;

		private int _yMax;

		private int _unitsPerEm;

		private bool _reverse;

		private List<ContourData> _contourList;

		public int glyphIndex
		{
			get
			{
				return _glyphIndex;
			}
		}

		public bool isVisibleChar
		{
			get
			{
				return _isVisibleChar;
			}
		}

		public int resolution
		{
			get
			{
				return _resolution;
			}
		}

		public float scaleFactor
		{
			get
			{
				return _scaleFactor;
			}
		}

		public float extrudeDepth
		{
			get
			{
				return _extrudeDepth;
			}
		}

		public int vertexCount
		{
			get
			{
				return _vertexCount;
			}
		}

		public int triCount
		{
			get
			{
				return _triCount;
			}
		}

		public int triCount2
		{
			get
			{
				return _triCount2;
			}
		}

		public Vector3[] vertices
		{
			get
			{
				return _vertices;
			}
		}

		public int[] triangles
		{
			get
			{
				return _triangles;
			}
		}

		public int[] triangles2
		{
			get
			{
				return _triangles2;
			}
		}

		public bool useSubmesh
		{
			get
			{
				return _useSubmesh;
			}
		}

		public bool useBack
		{
			get
			{
				return _useBack;
			}
		}

		public bool triDataComputed
		{
			get
			{
				return _triDataComputed;
			}
		}

		public int frontVertIndex
		{
			get
			{
				return _frontVertIndex;
			}
		}

		public int xMin
		{
			get
			{
				return _xMin;
			}
		}

		public int yMin
		{
			get
			{
				return _yMin;
			}
		}

		public int xMax
		{
			get
			{
				return _xMax;
			}
		}

		public int yMax
		{
			get
			{
				return _yMax;
			}
		}

		public GlyphData(List<Vector2[]> pointsList, List<bool[]> onCurvesList, int xMin, int yMin, int xMax, int yMax, int unitsPerEm, int glyphIndex)
		{
			_glyphIndex = glyphIndex;
			if (pointsList != null)
			{
				_xMin = xMin;
				_yMin = yMin;
				_xMax = xMax;
				_yMax = yMax;
				_unitsPerEm = unitsPerEm;
				_contourList = SortPointsList(pointsList, onCurvesList);
				_isVisibleChar = true;
			}
			_scaleFactor = -1f;
			_resolution = -1;
		}

		private List<ContourData> SortPointsList(List<Vector2[]> pointsList, List<bool[]> onCurvesList)
		{
			List<ContourData> list = new List<ContourData>(pointsList.Count);
			for (int i = 0; i < pointsList.Count; i++)
			{
				list.Add(new ContourData(pointsList[i], onCurvesList[i]));
			}
			int count = list.Count;
			if (count > 1)
			{
				for (int j = 0; j < count; j++)
				{
					Vector2[] array = RenderContourPoints(list[j], 1, true);
					list[j].renderedPoints = array;
					float x = array[0].x;
					float x2 = array[0].x;
					float y = array[0].y;
					float y2 = array[0].y;
					int num = array.Length;
					for (int k = 1; k < num; k++)
					{
						if (array[k].x > x)
						{
							x = array[k].x;
						}
						else if (array[k].x < x2)
						{
							x2 = array[k].x;
						}
						if (array[k].y > y)
						{
							y = array[k].y;
						}
						else if (array[k].y < y2)
						{
							y2 = array[k].y;
						}
					}
					list[j].maxPoint = new Vector2(x, y);
					list[j].minPoint = new Vector2(x2, y2);
				}
				list.Sort(AreaCompare);
			}
			_reverse = !IsClockwise(list[count - 1].points);
			for (int l = 0; l < count; l++)
			{
				if (!_reverse)
				{
					list[l].side = (IsClockwise(list[l].points) ? Side.Out : Side.In);
					continue;
				}
				list[l].side = ((!IsClockwise(list[l].points)) ? Side.Out : Side.In);
				if (count > 1)
				{
					Array.Reverse(list[l].renderedPoints);
				}
			}
			if (count == 1)
			{
				return list;
			}
			List<ContourData> list2 = new List<ContourData>(count);
			for (int m = 0; m < list.Count; m++)
			{
				ContourData contourData = list[m];
				if (contourData.side != Side.Out)
				{
					continue;
				}
				list2.Add(contourData);
				list.RemoveAt(m--);
				for (int n = 0; n < list.Count; n++)
				{
					if (list[n].side == Side.In && PolyContainsPoint(contourData.renderedPoints, list[n].renderedPoints[0]))
					{
						list2.Add(list[n]);
						list.RemoveAt(n--);
					}
				}
				m = -1;
			}
			return list2;
		}

		private bool PolyContainsPoint(Vector2[] polyPoints, Vector2 p)
		{
			bool flag = false;
			int num = polyPoints.Length - 1;
			int num2 = polyPoints.Length;
			int num3 = 0;
			while (num3 < num2)
			{
				if (((polyPoints[num3].y <= p.y && p.y < polyPoints[num].y) || (polyPoints[num].y <= p.y && p.y < polyPoints[num3].y)) && p.x < (polyPoints[num].x - polyPoints[num3].x) * (p.y - polyPoints[num3].y) / (polyPoints[num].y - polyPoints[num3].y) + polyPoints[num3].x)
				{
					flag = !flag;
				}
				num = num3++;
			}
			return flag;
		}

		private int AreaCompare(ContourData a, ContourData b)
		{
			return ((a.maxPoint.x - a.minPoint.x) * (a.maxPoint.y - a.minPoint.y)).CompareTo((b.maxPoint.x - b.minPoint.x) * (b.maxPoint.y - b.minPoint.y));
		}

		private int XMaxCompare(InsideData a, InsideData b)
		{
			return (a.xMaxPoint.x < b.xMaxPoint.x).CompareTo(b.xMaxPoint.x < a.xMaxPoint.x);
		}

		private Vector2[] RenderContourPoints(ContourData contour, int resolution, bool initialTest)
		{
			_resolution = resolution;
			Vector2[] points = contour.points;
			bool[] onCurves = contour.onCurves;
			int num = points.Length;
			List<Vector2> list = new List<Vector2>(num * resolution);
			float num2 = (float)(_unitsPerEm / 2) / (float)(resolution + 2);
			Vector2 zero = Vector2.zero;
			int num3 = 0;
			int num4 = 0;
			while (num4 < num)
			{
				int num5 = num4 + 1;
				if (num5 == num)
				{
					num5 = 0;
				}
				Vector2 vector;
				Vector2 vector2;
				Vector2 vector3;
				if (onCurves[num4])
				{
					if (onCurves[num5])
					{
						list.Add(points[num4]);
						num4++;
						continue;
					}
					int num6 = num5 + 1;
					if (num6 == num)
					{
						num6 = 0;
					}
					vector = points[num4];
					vector2 = points[num5];
					vector3 = ((!onCurves[num6]) ? ((points[num5] + points[num6]) / 2f) : points[num6]);
					num3 = 2;
				}
				else
				{
					int num7 = num4 - 1;
					if (num7 == -1)
					{
						num7 = num - 1;
					}
					vector = (points[num7] + points[num4]) / 2f;
					vector2 = points[num4];
					vector3 = ((!onCurves[num5]) ? ((points[num4] + points[num5]) / 2f) : points[num5]);
					num3 = 1;
				}
				float magnitude = (vector - vector3).magnitude;
				int num8 = ((initialTest || magnitude < num2) ? 1 : ((int)(magnitude / num2)));
				float num9 = 0f;
				float num10 = 1f / (float)(num8 + 1);
				for (int i = 0; i <= num8; i++)
				{
					float num11 = 1f - num9;
					zero.x = num11 * num11 * vector.x + 2f * num9 * num11 * vector2.x + num9 * num9 * vector3.x;
					zero.y = num11 * num11 * vector.y + 2f * num9 * num11 * vector2.y + num9 * num9 * vector3.y;
					list.Add(zero);
					num9 += num10;
				}
				num4 += num3;
			}
			if (!initialTest)
			{
				int num12 = list.Count;
				num2 *= 4f;
				int num13 = 0;
				while (num13 < num12)
				{
					int num14 = num13 - 1;
					if (num14 < 0)
					{
						num14 = num12 - 1;
					}
					int num15 = num13 + 1;
					if (num15 > num12 - 1)
					{
						num15 = 0;
					}
					Vector2 vector = list[num14];
					Vector2 vector2 = list[num13];
					Vector2 vector3 = list[num15];
					if ((vector - vector3).magnitude > num2 && LineToPointSqrDistance(ref vector, ref vector3, ref vector2) < 0.1f)
					{
						list.RemoveAt(num13);
						num12--;
					}
					else
					{
						num13++;
					}
				}
			}
			Vector2[] array = list.ToArray();
			if (_reverse)
			{
				Array.Reverse(array);
			}
			return array;
		}

		public bool SetMeshData(int resolution)
		{
			_triDataComputed = false;
			List<Vector2[]> list = new List<Vector2[]>(_contourList.Count);
			int[] array = new int[_contourList.Count];
			Vector2[] array2 = new Vector2[_contourList.Count];
			for (int i = 0; i < _contourList.Count; i++)
			{
				if (_contourList[i].side == Side.Out)
				{
					list.Add(RenderContourPoints(_contourList[i], resolution, false));
					continue;
				}
				List<InsideData> list2 = new List<InsideData>();
				int num = i;
				for (; i < _contourList.Count; i++)
				{
					InsideData insideData = new InsideData();
					insideData.points = RenderContourPoints(_contourList[i], resolution, false);
					Vector2[] points = insideData.points;
					int xMaxVertex = 0;
					float x = points[0].x;
					float y = points[0].y;
					int num2 = points.Length;
					for (int j = 1; j < num2; j++)
					{
						if (points[j].x > x)
						{
							x = points[j].x;
							y = points[j].y;
							xMaxVertex = j;
						}
					}
					insideData.xMaxPoint = new Vector2(x, y);
					insideData.xMaxVertex = xMaxVertex;
					list2.Add(insideData);
					if (i + 1 == _contourList.Count || _contourList[i + 1].side == Side.Out)
					{
						list2.Sort(XMaxCompare);
						for (int k = 0; k < list2.Count; k++)
						{
							list.Add(list2[k].points);
							array[num] = list2[k].xMaxVertex;
							array2[num++] = list2[k].xMaxPoint;
						}
						break;
					}
				}
			}
			int[] array3 = new int[list.Count];
			List<int[]> list3 = new List<int[]>(list.Count);
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int l = 0; l < list.Count; l++)
			{
				if (_contourList[l].side == Side.Out && l > 0)
				{
					num6 += (num5 - 2) * 3;
					num5 = 0;
				}
				Vector2[] array4 = list[l];
				int num7 = array4.Length;
				if (array4.Length > 2 && array4[0] == array4[num7 - 1])
				{
					num7--;
				}
				if (num7 < 3)
				{
					list3.Add(null);
					continue;
				}
				array3[l] = num7;
				num4 += num7;
				if (_contourList[l].side == Side.Out)
				{
					num3 = 0;
				}
				int[] array5 = new int[num7];
				for (int m = 0; m < num7; m++)
				{
					array5[m] = m + num3;
				}
				num3 += num7;
				list3.Add(array5);
				num5 += num7;
				if (_contourList[l].side == Side.In)
				{
					num5 += 2;
				}
			}
			num6 += (num5 - 2) * 3;
			int num8 = num4;
			int num9 = num6;
			num4 *= 2;
			num6 *= 2;
			for (int n = 0; n < array3.Length; n++)
			{
				num4 += array3[n] * 4;
				num6 += array3[n] * 6;
			}
			if (num4 > 65534)
			{
				Debug.LogError("Too many points...resolution is too high or character is too complex");
				return false;
			}
			Vector3[] array6 = new Vector3[num4];
			int[] array7 = new int[num6];
			int num10 = 0;
			for (int num11 = 0; num11 < list.Count; num11++)
			{
				Vector2[] array8 = list[num11];
				int num12 = array3[num11];
				for (int num13 = 0; num13 < num12; num13++)
				{
					array6[num10].x = array8[num13].x;
					array6[num10++].y = array8[num13].y;
				}
			}
			int triIdx = 0;
			int triAdd = 0;
			if (!Triangulate.Compute(_contourList, list3, array3, array, array2, list, array7, ref triIdx, ref triAdd))
			{
				return false;
			}
			Array.Copy(array6, 0, array6, num8, num8);
			for (int num14 = 0; num14 < num9; num14 += 3)
			{
				array7[num14 + num9] = array7[num14 + 2] + num8;
				array7[num14 + num9 + 1] = array7[num14 + 1] + num8;
				array7[num14 + num9 + 2] = array7[num14] + num8;
			}
			int num15 = 0;
			int num16 = num8;
			int num17 = num8 * 2;
			float num18 = Mathf.Clamp(FlyingText.smoothingAngle, 0f, 180f);
			triIdx = num9 * 2;
			triAdd *= 2;
			int num19 = 0;
			int edgeTriCount = 0;
			Vector2 from = default(Vector2);
			Vector2 to = default(Vector2);
			for (int num20 = 0; num20 < list.Count; num20++)
			{
				if (array3[num20] < 3)
				{
					continue;
				}
				int num21 = array3[num20];
				int num22 = 0;
				int num23 = 0;
				int num24 = triAdd;
				int vCount = 0;
				for (int num25 = 0; num25 < num21; num25++)
				{
					num22 = ((num25 + 1 != num21) ? (num25 + 1) : 0);
					num23 = ((num22 + 1 != num21) ? (num22 + 1) : 0);
					float x2 = array6[num15 + num22].x;
					float y2 = array6[num15 + num22].y;
					from.x = array6[num15 + num25].x - x2;
					from.y = array6[num15 + num25].y - y2;
					to.x = array6[num15 + num23].x - x2;
					to.y = array6[num15 + num23].y - y2;
					array6[num17] = array6[num15 + num25];
					array6[num17 + 1] = array6[num16 + num25];
					if (vCount != 0)
					{
						AddTriangle(array7, ref triAdd, ref triIdx, ref edgeTriCount, ref vCount);
					}
					if (180f - Vector2.Angle(from, to) >= num18)
					{
						array6[num17 + 2] = array6[num15 + num22];
						array6[num17 + 3] = array6[num16 + num22];
						num17 += 4;
						vCount = 4;
						num19 += 4;
					}
					else
					{
						num17 += 2;
						vCount = 2;
						num19 += 2;
					}
				}
				if (vCount == 4)
				{
					AddTriangle(array7, ref triAdd, ref triIdx, ref edgeTriCount, ref vCount);
				}
				else
				{
					array7[triIdx] = triAdd;
					array7[triIdx + 1] = triAdd + 1;
					array7[triIdx + 2] = num24;
					array7[triIdx + 3] = triAdd + 1;
					array7[triIdx + 4] = num24 + 1;
					array7[triIdx + 5] = num24;
					triIdx += 6;
					triAdd += vCount;
					edgeTriCount += 6;
				}
				num10 += num21;
				num15 += num21;
				num16 += num21;
			}
			if (array6.Length != num8 * 2 + num19)
			{
				Array.Resize(ref array6, num8 * 2 + num19);
			}
			if (array7.Length != num9 * 2 + edgeTriCount)
			{
				Array.Resize(ref array7, num9 * 2 + edgeTriCount);
			}
			_baseVertices = array6;
			_baseTriangles = array7;
			_frontVertIndex = num8;
			_frontTriIndex = num9;
			_scaleFactor = -1f;
			return true;
		}

		private void AddTriangle(int[] meshTriangles, ref int triAdd, ref int triIdx, ref int edgeTriCount, ref int vCount)
		{
			meshTriangles[triIdx] = triAdd;
			meshTriangles[triIdx + 1] = triAdd + 1;
			meshTriangles[triIdx + 2] = triAdd + 2;
			meshTriangles[triIdx + 3] = triAdd + 1;
			meshTriangles[triIdx + 4] = triAdd + 3;
			meshTriangles[triIdx + 5] = triAdd + 2;
			triIdx += 6;
			triAdd += vCount;
			edgeTriCount += 6;
		}

		public int GetVertexCount(bool extrude, bool includeBackface)
		{
			if (!extrude)
			{
				return _frontVertIndex;
			}
			if (includeBackface)
			{
				return _baseVertices.Length;
			}
			return _baseVertices.Length - _frontVertIndex;
		}

		public void ScaleVertices(float scaleFactor, bool extrude, bool includeBackface)
		{
			if (!extrude)
			{
				CopyAndScale(scaleFactor, _frontVertIndex);
			}
			else if (includeBackface)
			{
				CopyAndScale(scaleFactor, _baseVertices.Length);
			}
			else
			{
				CopyAndScale(scaleFactor, _frontVertIndex);
				CopyAndScale(scaleFactor, _frontVertIndex * 2, _frontVertIndex, _baseVertices.Length - _frontVertIndex * 2);
			}
			_scaleFactor = scaleFactor;
		}

		private void CopyAndScale(float scaleFactor, int length)
		{
			for (int i = 0; i < length; i++)
			{
				_vertices[i].x = _baseVertices[i].x * scaleFactor;
				_vertices[i].y = _baseVertices[i].y * scaleFactor;
			}
		}

		private void CopyAndScale(float scaleFactor, int source, int dest, int length)
		{
			for (int i = 0; i < length; i++)
			{
				_vertices[dest + i].x = _baseVertices[source + i].x * scaleFactor;
				_vertices[dest + i].y = _baseVertices[source + i].y * scaleFactor;
			}
		}

		public void SetFrontTriData()
		{
			_triangles = new int[_frontTriIndex];
			Array.Copy(_baseTriangles, _triangles, _frontTriIndex);
			_triCount = _frontTriIndex;
			_vertices = new Vector3[_frontVertIndex];
			_vertexCount = _frontVertIndex;
			_scaleFactor = -1f;
			_triDataComputed = true;
		}

		public void SetFrontAndEdgeTriData(bool doSubmesh)
		{
			if (!doSubmesh)
			{
				_triangles = new int[_baseTriangles.Length - _frontTriIndex];
				Array.Copy(_baseTriangles, _triangles, _frontTriIndex);
				int num = _baseTriangles.Length;
				int frontTriIndex = _frontTriIndex;
				for (int i = _frontTriIndex * 2; i < num; i++)
				{
					_triangles[frontTriIndex++] = _baseTriangles[i] - _frontVertIndex;
				}
				_triCount = _triangles.Length;
			}
			else
			{
				_triangles = new int[_frontTriIndex];
				Array.Copy(_baseTriangles, _triangles, _frontTriIndex);
				_triCount = _frontTriIndex;
				_triangles2 = new int[_baseTriangles.Length - _frontTriIndex * 2];
				int num2 = _baseTriangles.Length;
				int num3 = 0;
				for (int j = _frontTriIndex * 2; j < num2; j++)
				{
					_triangles2[num3++] = _baseTriangles[j] - _frontVertIndex;
				}
				_triCount2 = _triangles2.Length;
			}
			_useSubmesh = doSubmesh;
			_vertices = new Vector3[_baseVertices.Length - _frontVertIndex];
			_vertexCount = _vertices.Length;
			_scaleFactor = -1f;
			_extrudeDepth = -1f;
			_triDataComputed = true;
		}

		public void SetTriData(bool doSubmesh)
		{
			if (!doSubmesh)
			{
				_triangles = _baseTriangles;
				_triCount = _triangles.Length;
			}
			else
			{
				_triangles = new int[_frontTriIndex * 2];
				Array.Copy(_baseTriangles, _triangles, _frontTriIndex * 2);
				_triCount = _triangles.Length;
				_triangles2 = new int[_baseTriangles.Length - _frontTriIndex * 2];
				Array.Copy(_baseTriangles, _frontTriIndex * 2, _triangles2, 0, _baseTriangles.Length - _frontTriIndex * 2);
				_triCount2 = _triangles2.Length;
			}
			_useSubmesh = doSubmesh;
			_vertices = new Vector3[_baseVertices.Length];
			_vertexCount = _vertices.Length;
			_scaleFactor = -1f;
			_extrudeDepth = -1f;
			_triDataComputed = true;
		}

		public void SetExtrudeDepth(float depth, bool includeBackface)
		{
			if (includeBackface)
			{
				int num = 0;
				int num2 = _frontVertIndex * 2;
				for (num = _frontVertIndex; num < num2; num++)
				{
					_vertices[num].z = depth;
				}
				num2 = _vertices.Length;
				for (num++; num < num2; num += 2)
				{
					_vertices[num].z = depth;
				}
			}
			else
			{
				int num3 = _vertices.Length;
				for (int i = _frontVertIndex + 1; i < num3; i += 2)
				{
					_vertices[i].z = depth;
				}
			}
			_extrudeDepth = depth;
			_useBack = includeBackface;
		}

		private float LineToPointSqrDistance(ref Vector2 p1, ref Vector2 p2, ref Vector2 p)
		{
			float sqrMagnitude = (p2 - p1).sqrMagnitude;
			if (sqrMagnitude == 0f)
			{
				return (p - p1).sqrMagnitude;
			}
			float num = Vector2.Dot(p - p1, p2 - p1) / sqrMagnitude;
			if (num < 0f)
			{
				return (p - p1).sqrMagnitude;
			}
			if (num > 1f)
			{
				return (p - p2).sqrMagnitude;
			}
			Vector2 vector = p1 + num * (p2 - p1);
			return (p - vector).sqrMagnitude;
		}

		public static bool IsClockwise(Vector2[] points)
		{
			int num = points.Length;
			float num2 = 0f;
			int num3 = num - 1;
			int num4 = 0;
			while (num4 < num)
			{
				num2 += points[num3].x * points[num4].y - points[num4].x * points[num3].y;
				num3 = num4++;
			}
			return num2 <= 0f;
		}
	}
}
