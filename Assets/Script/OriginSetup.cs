using UnityEngine;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine.UI;

public class OriginSetup : MonoBehaviour {

    public GameObject DepthSrcManager;
    public Text alert;
    private MultiSourceManager depthManager;
    private ushort[] distances;
    GameObject objToSpawn;
    public GameObject Prefab;
    public static bool setupFinished = false;
    bool foundHighestObstacle = false;
    bool foundMidObstacle = false;
    bool foundLowObstacle = false;

    int width = 512;
    int height = 424;
    float minValue = 10000;

    float lastMinValue = 10000;
    int findObstacleCounter = 0;
    float distanceToHighestObstacle;
    int index, xCoord, yCoord = 0;

    private int sizeDifferenceBetweenLargestAndMidObstacle = 50;
    private int sizeDiiferenceBetweenLargestAndLowObstacle = 150;

    float distance = 0;
    Vector3 PosLarge;
    Vector3 PosMid;
    Vector3 PosLow;



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
        alert.text = ("Spielfeld freihalten");

    }

    // Update is called once per frame
    void Update()
    {
        if (depthManager == null)
        {
            return;
        }

        if(!setupFinished)
        {
            distances = depthManager.GetDepthData();
            int height = 424;
            int width = 512;
            int center = (height * width) / 2;

            if (!foundHighestObstacle)
            {
                for (int x = 50; x < width - 50; x++)
                    for (int y = 50; y < height - 50; y++)
                    {
                        if ((minValue > distances[y * width + x]) && (distances[y * width + x] != 0))
                        {
                            minValue = distances[y * width + x];

                            index = y * width + x;
                            xCoord = x;
                            yCoord = y;
                            xCoord = xCoord - 256;
                            yCoord = (yCoord - 212) * 2;
                        }
                    }

                Debug.Log(minValue);

                if (!foundHighestObstacle && minValue != 10000)
                {
                    float contrast = lastMinValue - minValue;
                    lastMinValue = minValue;

                    if (contrast >= -20 && contrast <= 20)
                    {
                        findObstacleCounter++;
                        if (findObstacleCounter >= 20)
                        {
                            distanceToHighestObstacle = minValue;
                            Vector3 PosHigh = new Vector3(xCoord, 0, yCoord);
                            Instantiate(Prefab, PosHigh, Quaternion.identity);
                            PosLarge = PosHigh;
                            foundHighestObstacle = true;
                            findObstacleCounter = 0;
                            minValue = 10000;
                            lastMinValue = 9000;
                            index = 0;
                            xCoord = 0;
                            yCoord = 0;
                        }
                    }
                }

                //Debug.Log(distanceToHighestObstacle);
            }


            if(foundHighestObstacle && !foundMidObstacle)
            {
                for(int i = 0; i < distances.Length; i++)
                {
                    if(distances[i] < distanceToHighestObstacle + sizeDifferenceBetweenLargestAndMidObstacle)
                    {
                        distances[i] = 10000;
                    }
                }

                for (int x = 25; x < width - 25; x++)
                    for (int y = 25; y < height - 25; y++)
                    {
                        if ((minValue > distances[y * width + x]) && (distances[y * width + x] != 0))
                        {
                            minValue = distances[y * width + x];

                            index = y * width + x;
                            xCoord = x;
                            yCoord = y;
                            xCoord = xCoord - 256;
                            yCoord = (yCoord - 212) * 2;
                        }
                    }

                if (!foundMidObstacle && minValue != 10000)
                {
                    float contrast = lastMinValue - minValue;
                    lastMinValue = minValue;

                    if (contrast >= -20 && contrast <= 20)
                    {
                        findObstacleCounter++;
                        if (findObstacleCounter >= 20)
                        {
                            Vector3 PosHigh = new Vector3(xCoord, 0, yCoord);
                            PosMid = PosHigh;
                            distance = Vector3.Distance(PosLarge, PosMid);
                            Debug.Log(distance);

                            if(distance > 50)
                            {
                                Instantiate(Prefab, PosHigh, Quaternion.identity);
                                foundMidObstacle = true;
                                findObstacleCounter = 0;
                                minValue = 10000;
                                lastMinValue = 9000;
                                index = 0;
                                xCoord = 0;
                            } else
                            {
                                findObstacleCounter = 0;
                            }
                        }
                    }
                }
            }

            /*
            if (foundMidObstacle && !foundLowObstacle)
            {
                for (int i = 0; i < distances.Length; i++)
                {
                    if (distances[i] < distanceToHighestObstacle + sizeDiiferenceBetweenLargestAndLowObstacle)
                    {
                        distances[i] = 10000;
                    }
                }

                for (int x = 25; x < width - 25; x++)
                    for (int y = 25; y < height - 25; y++)
                    {
                        if ((minValue > distances[y * width + x]) && (distances[y * width + x] != 0))
                        {
                            minValue = distances[y * width + x];

                            index = y * width + x;
                            xCoord = x;
                            yCoord = y;
                            xCoord = xCoord - 256;
                            yCoord = (yCoord - 212) * 2;
                        }
                    }

                if (!foundLowObstacle && minValue != 10000)
                {
                    float contrast = lastMinValue - minValue;
                    lastMinValue = minValue;

                    if (contrast >= -20 && contrast <= 20)
                    {
                        findObstacleCounter++;
                        if (findObstacleCounter >= 20)
                        {
                            Vector3 PosHigh = new Vector3(xCoord, 0, yCoord);
                            Instantiate(Prefab, PosHigh, Quaternion.identity);
                            Debug.Log("(x,y,z) = " + "(" + PosHigh.x + "," + PosHigh.y + "," + PosHigh.z + ") - Item3");
                            PosLow = PosHigh;
                            foundLowObstacle = true;
                            findObstacleCounter = 0;
                            minValue = 10000;
                            lastMinValue = 9000;
                            index = 0;
                            xCoord = 0;
                            yCoord = 0;
                        }
                    }
                }
            }
            */
            if(foundHighestObstacle && foundMidObstacle)
            {
                alert.text = ("Spielhand ins Feld halten");
                setupFinished = true;
                Debug.Log(setupFinished + " originsetup");
                Destroy(GetComponent<OriginSetup>());
            }
        }
    }
}