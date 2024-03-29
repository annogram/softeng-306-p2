﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///<summary>
/// This class is respnsible for the grids in the level select screen
///</summary>

public class LevelGrids : MonoBehaviour {

	public int col, row;

	// Update is called once per frame
	void Update () {

		RectTransform parent = gameObject.GetComponent<RectTransform> ();
		GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup> ();

		grid.cellSize = new Vector2 (parent.rect.width / col - 5, parent.rect.height / row - 5);

	}
}
