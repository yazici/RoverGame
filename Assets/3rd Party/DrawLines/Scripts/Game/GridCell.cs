﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

///Developed by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

[DisallowMultipleComponent]
public class GridCell : MonoBehaviour
{       
		/// <summary>
		/// The color of the top background.
		/// </summary>
		public Color topBackgroundColor;
	public Color myPairColor;
	public Vector2 myPairSize;
	public bool PulsateGrid;
	public bool pulseUp;
	public float pulseTime;
	public Color pulseColor;
	private Color startColor;
	private float currentTime01;
	private Image gridImage;


		/// <summary>
		/// Whether the GridCell is used.
		/// </summary>
		public bool currentlyUsed;

		/// <summary>
		/// Whether the GridCell is empty.
		/// </summary>
		public bool isEmpty = true;

		/// <summary>
		/// The index of the GridCell in the Grid.
		/// </summary>
		public int index;

		/// <summary>
		/// The index of the traget(partner).
		/// </summary>
		public int tragetIndex = -1;

		/// <summary>
		/// The index of the grid line.
		/// </summary>
		public int gridLineIndex = -1;

		/// <summary>
		/// The index of the element(dots) pair.
		/// </summary>
		public int elementPairIndex = -1;

	/// <summary>
	/// The correlated Ingredient for this grid's type.
	/// </summary>
	public Ingredient gridIngredient;

		/// <summary>
		/// The surrounded adjacents of the GridCell.
		/// (Contains the indexes of the adjacents (neighbours))
		/// </summary>
		public List<int> adjacents = new List<int> ();

		/// <summary>
		/// Define the adjacents of the GridCell.
		/// </summary>
		/// <param name="i">The index of the Row such that 0=< i < NumberOfRows </para>.</param>
		/// <param name="j">The index of the Column such that 0=< j < NumberOfColumns .</param>
		public void DefineAdjacents (int i, int j)
		{
				if (adjacents == null) {
						adjacents = new List<int> ();
				}

				AddAdjacent (new Vector2 (i, j + 1));//Right Adjacent
				AddAdjacent (new Vector2 (i, j - 1));//Left Adjacent
				AddAdjacent (new Vector2 (i - 1, j));//Upper Adjacent
				AddAdjacent (new Vector2 (i + 1, j));//Lower Adjacent
		}

		/// <summary>
		/// Adds the adjacent index (GridCell index) to the Adjacents List.
		/// </summary>
		/// <param name="adjacent">Adjacent vector (i,j).</param>
		private void AddAdjacent (Vector2 adjacent)
		{
				if ((adjacent.x >= 0 && adjacent.x < PuzzleManager.numberOfRows) && (adjacent.y >= 0 && adjacent.y < PuzzleManager.numberOfColumns)) {
						adjacents.Add ((int)(adjacent.x * PuzzleManager.numberOfColumns + adjacent.y));//Convert from (i,j) to Array index
				}
		}

		/// <summary>
		/// Check if the given adjacent index is one of the Adjacents or Not.
		/// </summary>
		/// <param name="adjacentIndex">an Adjacent index.</param>
		public bool OneOfAdjacents (int adjacentIndex)
		{
				if (adjacents == null) {
						return false;
				}

				if (adjacents.Contains (adjacentIndex)) {
						return true;
				}
				return false;
		}

		/// <summary>
		/// Reset Attributes
		/// </summary>
		public void Reset ()
		{
				currentlyUsed = false;
				if (isEmpty) {
						tragetIndex = -1;
						gridLineIndex = -1;
				}
		}

	void Start(){
		gridImage = gameObject.GetComponent<Image> ();
		startColor = gridImage.color;
		pulseUp = true;
	}

	void Update(){
		if (PulsateGrid) {
			if (pulseUp) {
				if (currentTime01 < pulseTime) {
					currentTime01 += Time.unscaledDeltaTime;
					float lerp = currentTime01 / pulseTime;
					gridImage.color = Color.Lerp (startColor, pulseColor, lerp);
				} else {
					currentTime01 = 0.0f;
					pulseUp = false;
				}
			} else {
				if (currentTime01 < pulseTime) {
					currentTime01 += Time.unscaledDeltaTime;
					float lerp = currentTime01 / pulseTime;
					gridImage.color = Color.Lerp (pulseColor, startColor, lerp);
				} else {
					currentTime01 = 0.0f;
					pulseUp = true;
					PulsateGrid = false;
				}
			}
		}
	}
}