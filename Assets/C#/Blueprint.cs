using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public string itemName;

    public string Rep1;
    public string Rep2;

    public int Req1amount;
    public int Req2amount;

    public int numOfRequirements;

    public Blueprint(string name, int repNUM, string R1, int R1num, string R2, int R2num)
    {
        itemName = name;

        numOfRequirements = repNUM;

        Rep1 = R1;
        Rep2 = R2;

        Req1amount = R1num;
        Req2amount = R2num;

    }
}
