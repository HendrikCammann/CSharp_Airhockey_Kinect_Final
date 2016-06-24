using UnityEngine;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;


public class OriginSetup : MonoBehaviour {

    public GameObject DepthSrcManager;
    private MultiSourceManager depthManager;
    private ushort[] distances;
    private Texture2D image;
    GameObject objToSpawn;
    public GameObject Prefab;
    bool setupFinished = false;



    // Use this for initialization
    void Start () {
        if(DepthSrcManager == null)
        {
            Debug.Log("Assign Game Object with Depth Source Manager");
        }
        else
        {
            depthManager = DepthSrcManager.GetComponent<MultiSourceManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (depthManager == null)
        {
            return;
        }

        if (!setupFinished)
        {
            distances = depthManager.GetDepthData();
            ushort[] distancesLow = distances;
            ushort[] distancesMid = distances;
            ushort[] distancesHigh = distances;

            ushort low = 2000;
            ushort mid = 1500;
            ushort high = 1000;

            int width = 512;
            int height = 424;
            int minValue = 10000;
            int xCoord = 0;
            int yCoord = 0;
            int index = 0;
            
            for(int i = 0; i < distances.Length; i++)
            {
                if(distances[i] >= high && distances[i] < mid)
                {
                    distancesHigh[i] = distances[i];
                }
            }

            for (int x = 50; x < width - 50; x++)
                for (int y = 50; y < height - 50; y++)
                {

                    if ((minValue > distances[y * width + x]) && (distances[y * width + x] != 0))
                    {
                        minValue = distances[y * width + x];
                        index = y * width + x;
                        xCoord = x;
                        yCoord = y;
                    }
                }

            if(xCoord != 0 && yCoord != 0)
            {
                xCoord = xCoord - 256;
                yCoord = (yCoord - 212) * 2;

                Vector3 Pos = new Vector3(xCoord, 0, yCoord);

                Debug.Log("(x,y,z) = " + "(" + Pos.x + "," + Pos.y + "," + Pos.z + ")");

                Instantiate(Prefab, Pos, Quaternion.identity);
                setupFinished = true;
                Destroy(GetComponent<OriginSetup>());
            }

        } 





    }

}