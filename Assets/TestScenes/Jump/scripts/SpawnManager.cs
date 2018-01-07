using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	private const int POOL_SIZE = 4;
	public GameObject tilePrefab;
	public List<TileController> tiles;
	public float tileSpace = 1f;

	public Camera cam;
	private int tileCounter;
	private int currentTileIndex;

	void Start ()
	{
		tiles = new List<TileController> ();
		for (int i = 0; i < POOL_SIZE; i++)
		{
			TileController tileClone = Clone(tilePrefab);
			tiles.Add (tileClone);
			Next ();
		}
		tilePrefab.SetActive (false);
	}

	void Update ()
	{
		TileController tileClone = tiles[currentTileIndex % POOL_SIZE];
		if (cam.transform.position.x > tileClone.transform.position.x) {
			currentTileIndex++;

			if (currentTileIndex > 1) {
				GameManager.Instance.Score++;
			}
			if (currentTileIndex > 2) {
				Next ();
			}	
		}
	}

	public void Reset ()
	{
		tileCounter = 0;
		currentTileIndex = 0;
		for (int i = 0; i < POOL_SIZE; i++)
		{
			Next ();
		}
	}

	void Next ()
	{
		TileController tileClone = tiles[tileCounter % POOL_SIZE];
		Vector3 pos = tileClone.transform.localPosition;
		pos.x = tilePrefab.transform.position.x + tileCounter * tileClone.transform.localScale.x + tileCounter * tileSpace + Random.Range (tileSpace / 2f, tileSpace);
		tileClone.transform.localPosition = pos;
		tileCounter++;
	}

	public TileController Clone (GameObject tile)
	{
		GameObject newGround = Instantiate (tile);
		newGround.name = "tile";
		newGround.transform.parent = transform;
		newGround.SetActive (true);
		TileController tileClone = newGround.GetComponent<TileController> ();
		tileClone.Init (this);
		return tileClone;
	}
}
