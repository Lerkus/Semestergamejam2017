using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendingTweet : MonoBehaviour {

	public void OnTweetDisappeared()
    {
        TweetCollection.instance.SetTweetInactive(this.gameObject);
    }
}
