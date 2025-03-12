using System;
using UnityEngine;

public class DisplayStars : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public String whichStars = "level1Stars";
    public GameObject starHint = null;

    void Update()
    {
        int starCount = PlayerPrefs.GetInt(whichStars);


        if(starCount >= 1) star1.SetActive(true);
        else star1.SetActive(false);
        if(starCount >= 2) star2.SetActive(true);
        else star2.SetActive(false);
        if(starCount == 3) star3.SetActive(true);
        else star3.SetActive(false);

        if (starHint != null && starCount != 3) starHint.SetActive(true);
    }
}
