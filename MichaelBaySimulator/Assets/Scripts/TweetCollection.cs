using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TweetCollection : MonoBehaviour {

    public GameObject tweetCollectionPanel;
    public GameObject tweetTemplate;
    public Sprite[] tweetSpriteTemplates;
    public int maxTweetPool;
    public int watcherSatisfication;

    public static TweetCollection instance;

    private List<GameObject> tweets;
    private List<GameObject> activeTweets;
    public Vector3 tweetOriginPosition;


    private void Awake()
    {
        if (instance)
            return;
        instance = this;
    }

    // Use this for initialization
    void Start () {
        tweets = new List<GameObject>();
        activeTweets = new List<GameObject>();
		for(int i = 0; i < maxTweetPool; i++)
        {
            GameObject tweet = InstantiateTweet(i);
            tweets.Add(tweet);
        }
        tweetOriginPosition = tweets[0].transform.position;
        StartCoroutine(ShowTweet());
	}

    private IEnumerator ShowTweet()
    {
        while (true)
        {
            // wait some time
            yield return new WaitForSeconds(Random.Range(20, 60) / watcherSatisfication);

            GameObject showTweet = tweets.Find(tweet => tweet.activeSelf == false);
            if(showTweet == null)
            {
                showTweet = InstantiateTweet(tweets.Count);
                tweets.Add(showTweet);
            }
            showTweet.transform.position = tweetOriginPosition;
            for(int i = 0; i < activeTweets.Count; i++)
            {
                activeTweets[i].transform.Translate(Vector2.up * (activeTweets[i].GetComponent<RectTransform>().rect.height + 1.5f));
            }
            activeTweets.Add(showTweet);

            showTweet.SetActive(true);
        } 
    }

    private GameObject InstantiateTweet(int id)
    {
        GameObject tweet = Instantiate(tweetTemplate, tweetCollectionPanel.transform) as GameObject;
        tweet.name = "Tweet " + id;
        Sprite sprite = tweetSpriteTemplates[Random.Range(0, tweetSpriteTemplates.Length - 1)];
        tweet.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        tweet.SetActive(false);
        return tweet;
    }

    public void SetTweetInactive(GameObject tweet)
    {
        activeTweets.Remove(tweet);
        tweet.SetActive(false);
        Color resetAlpha = tweet.GetComponent<UnityEngine.UI.Image>().color;
        resetAlpha.a = 1f;
        tweet.GetComponent<UnityEngine.UI.Image>().color = resetAlpha;
    }
}
