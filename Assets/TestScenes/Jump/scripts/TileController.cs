using UnityEngine;

public class TileController : MonoBehaviour
{
	public GameObject tile;

	private SpawnManager manager;

	public void Init (SpawnManager manager)
	{
		this.manager = manager;
	}
}
