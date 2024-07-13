using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlyingText3D
{
	public class Triangulate
	{
		public static bool Compute(List<ContourData> contourList, List<int[]> vertexList, int[] vertexCounts, int[] xMaxVertices, Vector2[] xMaxPoints, List<Vector2[]> pointsList, int[] meshTriangles, ref int triIdx, ref int triAdd)
		{
			Vector2 rhs = default(Vector2);
			for (int i = 0; i < pointsList.Count; i++)
			{
				if (contourList[i].side == Side.In || vertexCounts[i] < 3)
				{
					continue;
				}
				int[] array = vertexList[i];
				int num = vertexCounts[i];
				int num2 = vertexCounts[i];
				bool flag = false;
				Vector2[] array2 = null;
				if (i + 1 < pointsList.Count && contourList[i + 1].side == Side.In)
				{
					flag = true;
					for (int j = i + 1; j < pointsList.Count && contourList[j].side != Side.Out; j++)
					{
						num2 += vertexCounts[j];
					}
					array2 = new Vector2[num2];
					int num3 = vertexCounts[i];
					Array.Copy(pointsList[i], array2, num3);
					for (int k = i + 1; k < pointsList.Count && contourList[k].side != Side.Out; k++)
					{
						Array.Copy(pointsList[k], 0, array2, num3, vertexCounts[k]);
						num3 += vertexCounts[k];
					}
				}
				else
				{
					array2 = pointsList[i];
				}
				if (flag)
				{
					for (i++; i < pointsList.Count; i++)
					{
						if (contourList[i].side == Side.Out)
						{
							i--;
							break;
						}
						if (vertexCounts[i] < 3)
						{
							continue;
						}
						int num4 = xMaxVertices[i];
						int[] array3 = vertexList[i];
						int num5 = vertexCounts[i];
						Vector2 a = xMaxPoints[i];
						Vector2 vector = new Vector2(999999f, a.y);
						Vector2 b = Vector2.zero;
						float x = a.x;
						float num6 = float.MaxValue;
						int num7 = 0;
						int num8 = num - 1;
						for (int l = 0; l < num; l++)
						{
							Vector2 vector2 = array2[array[l]];
							Vector2 vector3 = array2[array[num8]];
							if (vector2.x >= x || vector3.x >= x)
							{
								if (vector2.y == a.y)
								{
									float num9 = (a.x - vector2.x) * (a.x - vector2.x);
									if (num9 <= num6 && Vector3.Cross(vector2 - vector3, a - vector2).z < 0f)
									{
										b = vector2;
										num6 = num9;
										num7 = l;
									}
								}
								else if ((vector3.y - a.y) * (vector2.x - a.x) > (vector2.y - a.y) * (vector3.x - a.x) != (vector3.y - vector.y) * (vector2.x - vector.x) > (vector2.y - vector.y) * (vector3.x - vector.x) && (vector2.y - a.y) * (vector.x - a.x) > (vector.y - a.y) * (vector2.x - a.x) != (vector3.y - a.y) * (vector.x - a.x) > (vector.y - a.y) * (vector3.x - a.x) && Vector3.Cross(vector2 - vector3, a - vector2).z < 0f)
								{
									rhs.x = 0f - (vector.y - a.y);
									rhs.y = vector.x - a.x;
									float num10 = Vector2.Dot(a - vector2, rhs) / Vector2.Dot(vector3 - vector2, rhs);
									Vector2 vector4 = vector2 + (vector3 - vector2) * num10;
									float sqrMagnitude = (a - vector4).sqrMagnitude;
									if (sqrMagnitude <= num6)
									{
										b = vector4;
										num6 = sqrMagnitude;
										num7 = ((!(vector2.x >= x)) ? num8 : l);
									}
								}
							}
							num8 = l;
						}
						vector = array2[array[num7]];
						float y;
						float y2;
						if (b.y < vector.y)
						{
							y = b.y;
							y2 = vector.y;
						}
						else
						{
							y = vector.y;
							y2 = b.y;
						}
						num6 = float.MaxValue;
						for (int m = 0; m < num; m++)
						{
							Vector2 vector2 = array2[array[m]];
							if (m == num7 || vector2.x < x || vector2.y > y2 || vector2.y < y)
							{
								continue;
							}
							num8 = m - 1;
							if (num8 < 0)
							{
								num8 = num - 1;
							}
							int num11 = m + 1;
							if (num11 == num)
							{
								num11 = 0;
							}
							if (IsReflex(ref array2[array[num8]], ref array2[array[m]], ref array2[array[num11]]) && PointInsideTriangle(ref vector2, ref a, ref b, ref vector))
							{
								float sqrMagnitude2 = (a - vector2).sqrMagnitude;
								if (sqrMagnitude2 <= num6)
								{
									num6 = sqrMagnitude2;
									num7 = m;
								}
							}
						}
						Array.Resize(ref array, num + num5 + 2);
						Array.Copy(array, num7 + 1, array, num7 + num5 + 3, num - num7 - 1);
						if (num4 == 0)
						{
							Array.Copy(array3, 0, array, num7 + 1, num5);
						}
						else
						{
							Array.Copy(array3, num4, array, num7 + 1, num5 - num4);
							Array.Copy(array3, 0, array, num7 + (num5 - num4) + 1, num4);
						}
						array[num7 + num5 + 1] = array3[num4];
						array[num7 + num5 + 2] = array[num7];
						num += num5 + 2;
					}
				}
				if (num > 3)
				{
					int[] array4 = new int[num];
					int num12 = 0;
					int num13 = 0;
					int num14 = num - 1;
					for (int n = 0; n < num; n++)
					{
						int num15 = n + 1;
						if (num15 > num - 1)
						{
							num15 = 0;
						}
						if (IsReflex(ref array2[array[num14]], ref array2[array[n]], ref array2[array[num15]]))
						{
							array4[num12++] = array[n];
							num13++;
						}
						num14 = n;
					}
					int num16 = 0;
					int num17 = -1;
					while (true)
					{
						if (++num17 == num)
						{
							Debug.LogWarning("Couldn't complete triangulation");
							return false;
						}
						if (num16 == num)
						{
							num16 = 0;
						}
						int num18 = array[num16];
						Vector2 b2 = array2[num18];
						int num19 = num16 - 1;
						if (num19 < 0)
						{
							num19 = num - 1;
						}
						int num20 = array[num19];
						Vector2 a2 = array2[num20];
						int num21 = num16 + 1;
						if (num21 > num - 1)
						{
							num21 = 0;
						}
						int num22 = array[num21];
						Vector2 c = array2[num22];
						if (IsReflex(ref a2, ref b2, ref c))
						{
							num16++;
							continue;
						}
						int num23 = -1;
						int num24 = -1;
						for (int num25 = 0; num25 < num13; num25++)
						{
							int num26 = array4[num25];
							if (num26 == num20)
							{
								num23 = num25;
								continue;
							}
							if (num26 == num22)
							{
								num24 = num25;
								continue;
							}
							if (num26 == num18 || !PointInsideTriangle(ref array2[num26], ref a2, ref b2, ref c))
							{
								continue;
							}
							goto IL_07d8;
						}
						num17 = -1;
						if (--num19 < 0)
						{
							num19 = num - 1;
						}
						Vector2 a3 = array2[array[num19]];
						if (++num21 > num - 1)
						{
							num21 = 0;
						}
						Vector2 c2 = array2[array[num21]];
						meshTriangles[triIdx] = num20 + triAdd;
						meshTriangles[triIdx + 1] = num18 + triAdd;
						meshTriangles[triIdx + 2] = num22 + triAdd;
						triIdx += 3;
						if (num16 < num - 1)
						{
							Array.Copy(array, num16 + 1, array, num16, --num - num16);
						}
						else
						{
							num--;
						}
						if (num == 3)
						{
							break;
						}
						if (num23 != -1 && !IsReflex(ref a3, ref a2, ref c))
						{
							if (num23 < num13 - 1)
							{
								Array.Copy(array4, num23 + 1, array4, num23, num13 - num23 - 1);
							}
							num13--;
							if (num24 > num23)
							{
								num24--;
							}
						}
						if (num24 != -1 && !IsReflex(ref a2, ref c, ref c2))
						{
							if (num24 < num13 - 1)
							{
								Array.Copy(array4, num24 + 1, array4, num24, num13 - num24 - 1);
							}
							num13--;
						}
						continue;
						IL_07d8:
						num16++;
					}
					meshTriangles[triIdx] = array[0] + triAdd;
					meshTriangles[triIdx + 1] = array[1] + triAdd;
					meshTriangles[triIdx + 2] = array[2] + triAdd;
					triIdx += 3;
				}
				else
				{
					meshTriangles[triIdx] = triAdd;
					meshTriangles[triIdx + 1] = triAdd + 1;
					meshTriangles[triIdx + 2] = triAdd + 2;
					triIdx += 3;
					num2 = 3;
				}
				triAdd += num2;
			}
			return true;
		}

		private static bool IsReflex(ref Vector2 a, ref Vector2 b, ref Vector2 c)
		{
			Vector2 lhs = new Vector2(0f - (b.y - a.y), b.x - a.x);
			Vector2 rhs = new Vector2(c.x - b.x, c.y - b.y);
			return Vector2.Dot(lhs, rhs) > 0f;
		}

		private static bool PointInsideTriangle(ref Vector2 p, ref Vector2 a, ref Vector2 b, ref Vector2 c)
		{
			float num = p.x - a.x;
			float num2 = p.y - a.y;
			bool flag = (b.x - a.x) * num2 - (b.y - a.y) * num > 0f;
			if ((c.x - a.x) * num2 - (c.y - a.y) * num > 0f == flag)
			{
				return false;
			}
			if ((c.x - b.x) * (p.y - b.y) - (c.y - b.y) * (p.x - b.x) > 0f != flag)
			{
				return false;
			}
			return true;
		}
	}
}
