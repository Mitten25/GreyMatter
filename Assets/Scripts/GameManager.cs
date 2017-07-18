using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Transform spawnPoint;

    public GameObject Player;

    [HideInInspector]
    public GameObject PlayerPrefab;

    private void Awake()
    {
        //initialize singleton behavior
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        Player = Instantiate(PlayerPrefab, spawnPoint.position, Quaternion.identity) as GameObject;

        SetCameraTargets();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Player == null)
        {
            Player = Instantiate(PlayerPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
            SetCameraTargets();
        }
	}

    private void SetCameraTargets()
    {
        CameraControlScript.instance.m_Targets = new Transform[] { Player.transform };
    }
}
