using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public GameManager manager;

    public Color[] playerColors;
    public GameObject[] cameraPool;

    //The prefabs to Create
    public GameObject gridCreatorPrefab;
    public GameObject otherPlayerPrefab;

    public float startX;
    public float startY;

    public float dX;




    // Start is called before the first frame update
    void Awake()
    {
        manager.players = new GameObject[GameData.Instance.numPlayers];
        for (int i = 0; i < GameData.Instance.numPlayers; i++)
        {
            //create new grid prefab
            GameObject go = Instantiate(gridCreatorPrefab, new Vector2(startX, startY), Quaternion.identity);

           

            //Initialize grid stuff
            GridCreator gc = go.GetComponent<GridCreator>();
            gc.playerID = i;
            GameObject player = gc._playerObject;
            player.GetComponent<AIMinerController>().playerID = i;
            manager.players[i] = player;

            gc._otherPlayers = new GameObject[GameData.Instance.numPlayers]; //TODO change name

            //Create other players
            for (int j = 0;j < GameData.Instance.numPlayers; j++)
            {
                if(j != i)
                {
                    GameObject other = Instantiate(otherPlayerPrefab, Vector3.zero, Quaternion.identity);
                    other.transform.localPosition = Vector3.zero;
                    other.GetComponent<SpriteRenderer>().color = playerColors[j];

                    other.transform.parent = gc.transform;
                    gc._otherPlayers[j] = other;
                }
                else
                {
                    gc._otherPlayers[j] = player;
                    player.GetComponent<SpriteRenderer>().color = playerColors[j];

                }

            }
            gc.SelectMine(Mine.IronMine);


            //create the "other players"
            //set stats for gridCreator, AIMinerController, axe, etc
            startX += dX;
        }
    }
}
