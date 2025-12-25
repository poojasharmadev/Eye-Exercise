using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShapeCollection", menuName = "MemoryBlink/ShapeCollection")]
public class ShapeCollection : ScriptableObject
{
    public List<Sprite> shapeSprites; // Drag all shape sprites here
}
