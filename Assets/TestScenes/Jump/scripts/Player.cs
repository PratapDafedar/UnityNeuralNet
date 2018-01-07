using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	private const float timeBetweenDatasets = 0.3f;

	public SpawnManager groundTilePrefab;
    public GameObject currentGroundTile;
    public GameObject restartText;

    public float distanceInPercent;
	public float canJump;
	public NetLayer net;

    float countedTime = 0;

	private Vector3 initialPos;
	private Vector3 initialParentPos;
	private bool gameStarted;

    public void Start()
    {
        canJump = 0;
		initialPos = transform.localPosition;
		initialParentPos = transform.parent.position;
		groundTilePrefab.Reset ();
		gameStarted = true;
    }

    void Update()
    {
        countedTime += Time.deltaTime;

        //Move the parent (Camera + player)
        this.transform.parent.position += Vector3.right * 5F * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) //For supervised learning.
        {
            jump();
        }
        else
        {
            //Adding a new dataset
			if (countedTime > timeBetweenDatasets)// && !NetLayer.trained)
            {
                countedTime = 0;
                net.Train(canJump, 0);
            }
        }

        if (currentGroundTile == null)
            return;
		
        //Calculate the distance from player to the end of the current triggered platform in percent
        Vector3 startPointTile = currentGroundTile.transform.position - (Vector3.right * currentGroundTile.transform.localScale.x) / 2;
        Vector3 endPointTile = currentGroundTile.transform.position + (Vector3.right * currentGroundTile.transform.localScale.x) / 2;
        Vector3 platformLength = endPointTile - startPointTile;
        Vector3 distanceToEndOfPlatform = endPointTile - transform.position;
        distanceInPercent = distanceToEndOfPlatform.x / platformLength.x;

        checkForGameOver();
    }

    //Cube will trigger a platform and can jump again, also we also to transmit the currently triggered platform to the network
    public void OnTriggerEnter(Collider other)
    {
        canJump = 1;
        currentGroundTile = other.gameObject;
    }

    private void checkForGameOver()
    {
        //The player is basically game over
        if (transform.position.y < -5 && gameStarted)
        {
			gameStarted = false;
            restartText.SetActive(true);
			StartCoroutine (ResetGame());
        }
    }

	IEnumerator ResetGame ()
	{
		yield return new WaitForSeconds (1f);
		restartText.SetActive(false);
		transform.parent.position = initialParentPos;
		transform.localPosition = initialPos;
		groundTilePrefab.Reset ();
		gameStarted = true;
		gameObject.GetComponent<Rigidbody> ().Sleep ();
		canJump = 0;
		GameManager.Instance.Iteration++;
		GameManager.Instance.Score = 0;
	}

    public void jump()
    {
        if (canJump == 1)
        {
            net.Train(1, 1);

            //Jump
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 4500F);
            canJump = 0;
        }
    }
}