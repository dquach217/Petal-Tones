using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    public GameObject dandelionPrefab;
    public int populationSize = 30;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int trialTime = 40;
    int generation = 1;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioClip AudioClip1;
    public Object[] parentAudio;
    /*GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
    }    
    */
    // Use this for initialization
    void Start () {
        for(int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-1.5f,1.5f),Random.Range(0f,1.5f), Random.Range(-1f, 1f));
            GameObject go = Instantiate(dandelionPrefab, pos, Quaternion.identity);
            //color randomize 
            /* go.GetComponent<DNA>().r = Random.Range(0.0f,1.0f);
             go.GetComponent<DNA>().g = Random.Range(0.0f,1.0f);
             go.GetComponent<DNA>().b = Random.Range(0.0f,1.0f);
             */
            //sound randomize
            go.GetComponent<DNA>().cachedPitch = Random.Range(0.5f, 1.5f);
            population.Add(go);

            parentAudio = Resources.LoadAll("Sounds", typeof(AudioClip));
            int index = Random.Range(0, parentAudio.Length);
            go.GetComponent<DNA>().AudioClip1 = (AudioClip)parentAudio[index];
        }        
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9,9),Random.Range(-4.5f,4.5f),0);
        GameObject offspring = Instantiate(dandelionPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();
        DNA offspringDNA = offspring.GetComponent<DNA>();
        //swap parent dna
        if (Random.Range(0,10) < 5)
        {
            /*offspring.GetComponent<DNA>().r = Random.Range(0,10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA>().g = Random.Range(0,10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA>().b = Random.Range(0,10) < 5 ? dna1.b : dna2.b;*/

            offspring.GetComponent<DNA>().AudioClip1.LoadAudioData();
            Debug.Log("This is Audio Clip 1 of Parent " + offspring.GetComponent<DNA>().AudioClip1);
            AudioClip1 = offspring.GetComponent<DNA>().AudioClip1;
            audio1.clip = AudioClip1;

            if (audio1.clip != null)
            {
                Debug.Log("Audio Clip is set");
                offspringDNA.cachedPitch = Random.Range(0, 10) < 5
                ?
                   /*momPitch*/ dna1.cachedPitch

                :
                   /*dadPitch*/ dna2.cachedPitch;
                //  Debug.Log("mompitch", + dna1);

                audio1.pitch = offspringDNA.cachedPitch;
            }
            else
            {
                Debug.Log("Nothing happens");
                return offspring;
            }
            audio2.clip = offspring.GetComponent<DNA>().Audio2;
            audio2.pitch = Random.Range(0.0f, 1.0f);

            audio3.clip = offspring.GetComponent<DNA>().Audio3;
            audio3.pitch = Random.Range(0.0f, 1.0f);
        }
        else
        {

        
        }
        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        //get rid of unfit individuals
        List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeToDie).ToList();
        foreach (var go in sortedList)
        {
            Debug.Log("Sorted List " + go.name);
        }
        population.Clear();
        //breed upper half of sorted list
        for (int i = (int) (sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }
        
        //destroy all parents and previous population
        for(int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }
    
    // Update is called once per frame
    void Update () {
        elapsed += Time.deltaTime;
        Debug.Log("Trial Time " + elapsed);
        if(elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }

    }
}
