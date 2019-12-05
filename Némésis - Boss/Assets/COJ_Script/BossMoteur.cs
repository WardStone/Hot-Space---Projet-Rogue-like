using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoteur : MonoBehaviour
{
    List<int> patternList = new List<int>();
    int patternSaved = -1;
    int patternRef;
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            patternList.Add(i);
        }
        patternSaved = -1;
        BossPatternSelection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BossPatternSelection()
    {
        int iCurrentPattern = Random.Range(0, patternList.Count);
        Debug.Log("PatternList =" + patternList.Count);
        if (iCurrentPattern != -1)
        {
            patternRef = patternList[iCurrentPattern];
            Debug.Log("PatternRef =" + patternRef);
            if (patternSaved != -1)
            {
                patternList.Add(patternSaved);
                Debug.Log("patternRefSaved" + patternSaved + "has been added");
            }
        }
    }

    void RefreshPattern()
    {
        patternSaved = patternRef;
        patternList.Remove(patternRef);
        Debug.Log("PatternRef" + patternRef + "has been removed");
        BossPatternSelection();
    }

    IEnumerator MDRJESUISKINDEDEDEDJeSLAMTAMERE()
    {
        // Je me preparé a slam ta mère
        yield return new WaitForSeconds(2f);
        // Je slam ta grosse mère
        //puis je suis coincé pck je suis gros
        yield return new WaitForSeconds(2f);
        //c bon ca va mieux je vais faire autre chose d ema vie
        RefreshPattern();
    }

    IEnumerator JURINESURTESMORT()
    {
        float timerpipi = 2f;
        //Accumuler d ela pisse
        yield return new WaitForSeconds(2f);
        //Je te pisse dessu a distance
        // WITH A TWIST mon pipi te suis
        while (timerpipi > 0)
        {
            yield return new WaitForSeconds(0.1f);
            timerpipi -= Time.deltaTime;
            yield return null;
        }
        //Ok je vais faire autre chose;
        RefreshPattern();
    }

}
