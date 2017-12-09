using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBombs : MonoBehaviour {

    public GameObject bombTemplate;
    public Dictionary<GameObject, float> positionedExplosivs;
    public float maxPlaceableExplosivs;
    public float maximumExplosivScale;
    public float scalingFactorMultiplier;
    public Sprite scenerySprite; // bounds of placing bombs;


    private float buttonPressDuration;
    private GameObject currentPlaceableExplosiv;
    private Rect sceeneryRect;

	// Use this for initialization
	void Start ()
    {
        positionedExplosivs = new Dictionary<GameObject, float>();
        buttonPressDuration = 0f;
        currentPlaceableExplosiv = Instantiate(bombTemplate, Vector2.zero, Quaternion.identity ) as GameObject;
        currentPlaceableExplosiv.SetActive(false);

	}

    private void OnDisable()
    {
        Destroy(currentPlaceableExplosiv);
    }

    private const int MouseLeftButton = 0;

    // Update is called once per frame
    void Update ()
    {
       /* if (scenerySprite.rect.Contains(Input.mousePosition)) {
            return;
        }*/

        /*if(positionedExplosivs.Count == maxPlaceableExplosivs )
        {
            // only "start film scene" button can be pressed?
        }*/
        if (Input.GetMouseButtonDown(MouseLeftButton))
        {
            StartPlacing();
        }

		if (Input.GetMouseButton(MouseLeftButton))
        {
            UpdateExplosiveIndicator();  
        }
        if (Input.GetMouseButtonUp(MouseLeftButton))
        {
            InstantiateExplosive();
        }
	}

    private void StartPlacing()
    {
        currentPlaceableExplosiv.SetActive(true);
        buttonPressDuration = 0f;
    }

    private void InstantiateExplosive()
    {
        Vector2 explosivPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject explosiv = Instantiate(bombTemplate, explosivPosition, Quaternion.identity) as GameObject;
        explosiv.transform.localScale = currentPlaceableExplosiv.transform.localScale;
        positionedExplosivs.Add(explosiv, explosiv.transform.localScale.x);

        Debug.Log("Explosiv placed at " + explosivPosition + " with intensity " + buttonPressDuration);
        buttonPressDuration = 0f;
        currentPlaceableExplosiv.SetActive(false);
    }

    private void UpdateExplosiveIndicator()
    {
        Vector2 currentMousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        currentPlaceableExplosiv.transform.position = currentMousePosition;
        float scalingFactor = Mathf.PingPong(buttonPressDuration * scalingFactorMultiplier, maximumExplosivScale) + 1;
        currentPlaceableExplosiv.transform.localScale = new Vector2(scalingFactor, scalingFactor);
        buttonPressDuration += Time.deltaTime;

    }
}
