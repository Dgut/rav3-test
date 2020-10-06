using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// transparent raycast target
public class EmptyGraphic : Graphic
{
	protected override void OnPopulateMesh( VertexHelper vh )
	{
		vh.Clear();
	}
}