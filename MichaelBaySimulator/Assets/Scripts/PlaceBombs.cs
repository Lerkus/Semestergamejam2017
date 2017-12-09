using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBombs : MonoBehaviour
{

    public GameObject bombTemplate;
    public Dictionary<GameObject, float> positionedExplosivs;
    public float maxPlaceableExplosivs;
    public float maximumExplosivScale;
    public float scalingFactorMultiplier;
    public Sprite scenerySprite; // bounds of placing bombs;
    public GameObject bombController;
    public Pause pauseController;

    public float _ExplosivRadiusTweaker = 1;
    public float _TriggerRadiusTweaker = 1;

    private float buttonPressDuration;
    private GameObject currentPlaceableExplosiv;

    private Rect sceeneryRect;
    private bool onBomb;


    // Use this for initialization
    void Start()
    {
        onBomb = false;
        positionedExplosivs = new Dictionary<GameObject, float>();
        buttonPressDuration = 0f;
        currentPlaceableExplosiv = Instantiate(bombTemplate, Vector2.zero, Quaternion.identity) as GameObject;
        currentPlaceableExplosiv.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(currentPlaceableExplosiv);
    }

    private const int MouseLeftButton = 0;

    // Update is called once per frame
    void Update()
    {

        if (pauseController.paused)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {

            if (Input.GetMouseButtonDown(MouseLeftButton))
            {
                RaycastHit2D hitted = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (hitted.collider != null && hitted.transform.tag.Equals("base"))
                {
                    Debug.Log("Clicked on Bomb");
                    onBomb = true;
                    return;
                }
                StartPlacing();
            }

            if (Input.GetMouseButton(MouseLeftButton) && !onBomb)
            {
                UpdateExplosiveIndicator();
            }
            if (Input.GetMouseButtonUp(MouseLeftButton))
            {
                if (onBomb)
                {
                    onBomb = false;
                    return;
                }
                InstantiateExplosive();
            }
        }
        else
        {
            currentPlaceableExplosiv.SetActive(false);
        }

    }

    private void StartPlacing()
    {
        currentPlaceableExplosiv.SetActive(true);
        buttonPressDuration = 0f;
    }

    private void InstantiateExplosive()
    {
        if (currentPlaceableExplosiv.activeSelf == false)
            return;
        Vector2 explosivPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject explosiv = Instantiate(bombTemplate, explosivPosition, Quaternion.identity) as GameObject;
        explosiv.transform.localScale = currentPlaceableExplosiv.transform.localScale;
        explosiv.GetComponent<Bomb_slave>()._BombRadius = currentPlaceableExplosiv.GetComponent<Bomb_slave>()._BombRadius;
        explosiv.GetComponent<Bomb_slave>()._ExplosionRadius = currentPlaceableExplosiv.GetComponent<Bomb_slave>()._ExplosionRadius;
        positionedExplosivs.Add(explosiv, explosiv.transform.localScale.x);

        Debug.Log("Explosiv placed at " + explosivPosition + " with intensity " + buttonPressDuration);
        buttonPressDuration = 0f;
        currentPlaceableExplosiv.SetActive(false);
    }

    private void UpdateExplosiveIndicator()
    {
        Vector2 currentMousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        currentPlaceableExplosiv.transform.position = currentMousePosition;
        float scalingFactor = Mathf.Sin(buttonPressDuration * scalingFactorMultiplier - Mathf.PI / 2) * maximumExplosivScale + maximumExplosivScale + 1;
        currentPlaceableExplosiv.transform.localScale = new Vector2(scalingFactor, scalingFactor);
        currentPlaceableExplosiv.GetComponent<Bomb_slave>()._BombRadius = currentPlaceableExplosiv.transform.GetChild(0).transform.lossyScale.x * _ExplosivRadiusTweaker;
        currentPlaceableExplosiv.GetComponent<Bomb_slave>()._ExplosionRadius = currentPlaceableExplosiv.transform.GetChild(0).transform.lossyScale.x * _TriggerRadiusTweaker; //scalingFactor * 2
        buttonPressDuration += Time.deltaTime;
    }
}
