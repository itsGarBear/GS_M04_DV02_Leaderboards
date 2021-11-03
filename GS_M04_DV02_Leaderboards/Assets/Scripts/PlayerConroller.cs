using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerConroller : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    private float startTime;
    private float timeTaken;

    private int collectablesPicked;
    public int maxCollectables = 10;

    private bool isPlaying;

    public GameObject playButton;
    public TextMeshProUGUI currTimeText;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isPlaying)
            return;

        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;

        rb.velocity = new Vector3(x, rb.velocity.y, z);

        currTimeText.text = (Time.time - startTime).ToString("F2");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            collectablesPicked++;
            Destroy(other.gameObject);

            if (collectablesPicked == maxCollectables)
                End();
        }
    }

    public void Begin()
    {
        playButton.SetActive(false);
        startTime = Time.time;
        isPlaying = true;
    }

    void End()
    {
        playButton.SetActive(true);
        timeTaken = Time.time - startTime;
        isPlaying = false;

        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
    }

}
