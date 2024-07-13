using UnityEngine;

[AddComponentMenu("FlyingText3D/TextObjectData")]
public class TextObjectData : MonoBehaviour
{
	private float size;

	private float extrudeDepth;

	private int resolution;

	private float characterSpacing;

	private float lineSpacing;

	private float lineWidth;

	public void SetData(float size, float extrudeDepth, int resolution, float characterSpacing, float lineSpacing, float lineWidth)
	{
		this.size = size;
		this.extrudeDepth = extrudeDepth;
		this.resolution = resolution;
		this.characterSpacing = characterSpacing;
		this.lineSpacing = lineSpacing;
		this.lineWidth = lineWidth;
	}

	public void InitializeData(ref float size, ref float extrudeDepth, ref int resolution, ref float characterSpacing, ref float lineSpacing, ref float lineWidth)
	{
		size = this.size;
		extrudeDepth = this.extrudeDepth;
		resolution = this.resolution;
		characterSpacing = this.characterSpacing;
		lineSpacing = this.lineSpacing;
		lineWidth = this.lineWidth;
	}
}
