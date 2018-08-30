using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;      // for sorting list

public class PopulationManager : MonoBehaviour
{

    public GameObject personPrefab;
    public int populationSize = 10;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0f;

    private int trialTime = 10;     // time limit for the round 
    private int generation = 1;

    public Text generationText;
    public Text trialTimeText;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);   // replace these magic numbers later!
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);

            // create a random val for the size
            go.GetComponent<DNA>().scaleX = Random.Range(0.1f, 0.3f);
            go.GetComponent<DNA>().scaleY = Random.Range(0.1f, 0.3f);

            population.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        UpdateUI();

    }

    private void UpdateTimer()
    {

        elapsed += Time.deltaTime;       //ok to use fixedDeltaTime
        if (elapsed > trialTime)         // create the next generation, reset the timer
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

    private void UpdateUI()
    {
        generationText.text = "Generation: " + generation.ToString();
        trialTimeText.text = "Trial Time: " + ((int)elapsed).ToString();
    }

    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();

        // get rid of unfit individuals

        // for camoflauge, one clicked later to be favored
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

        //// for selecting your favorite color first - what you click first to be favored
        //List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeToDie).ToList();



        population.Clear();

        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            // creates two children at a time
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }


        // destroy all parents and previous population
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }

        generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);   // it's the random location
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        // swap parent dna
        if (Random.Range(0, 1000) > 5) // for most of the time, mix the traits
        {
            offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;

            // sort by size
            offspring.GetComponent<DNA>().scaleX = Random.Range(0, 10) < 5 ? dna1.scaleX : dna2.scaleX;
            offspring.GetComponent<DNA>().scaleY = Random.Range(0, 10) < 5 ? dna1.scaleY : dna2.scaleY;
        }
        else  // add a mutation - selects a random color
        {
            offspring.GetComponent<DNA>().r = Random.Range(0f, 10f);
            offspring.GetComponent<DNA>().g = Random.Range(0f, 10f);
            offspring.GetComponent<DNA>().b = Random.Range(0f, 10f);

            offspring.GetComponent<DNA>().scaleX = Random.Range(0, 10);
            offspring.GetComponent<DNA>().scaleY = Random.Range(0, 10);
        }
        return offspring;
    }

}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Linq;      // for sorting list

//public class PopulationManager : MonoBehaviour {

//    public GameObject personPrefab;
//    public int populationSize = 10;
//    List<GameObject> population= new List<GameObject>();
//    public static float elapsed = 0f;

//    private int trialTime = 15;     // time limit for the round 
//    private int generation = 1;

//    public Text generationText;
//    public Text trialTimeText;

//	// Use this for initialization
//	void Start () {
//        for (int i = 0; i < populationSize; i++)
//        {
//            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);   // replace these magic numbers later!
//            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
//            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
//            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
//            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);

//            population.Add(go);

//        }
//	}

//    // Update is called once per frame
//    void Update()
//    {
//        UpdateTimer();
//        UpdateUI();

//    }

//    private void UpdateTimer()
//    {

//        elapsed += Time.deltaTime;       //ok to use fixedDeltaTime
//        if (elapsed > trialTime)         // create the next generation, reset the timer
//        {
//            BreedNewPopulation();
//            elapsed = 0;
//        }
//    }

//    private void UpdateUI()
//    {
//        generationText.text = "Generation: " + generation.ToString();
//        trialTimeText.text = "Trial Time: " + ((int)elapsed).ToString();
//    }
	
//    void BreedNewPopulation()
//    {
//        List<GameObject> newPopulation = new List<GameObject>();

//        // get rid of unfit individuals

//        // for camoflauge, one clicked later to be favored
//        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

//        //// for selecting your favorite color first - what you click first to be favored
//        //List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeToDie).ToList();

//        population.Clear();

//        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
//        {
//            // creates two children at a time
//            population.Add(Breed(sortedList[i], sortedList[i + 1]));
//            population.Add(Breed(sortedList[i + 1], sortedList[i]));
//        }


//        // destroy all parents and previous population
//        for (int i = 0; i < sortedList.Count; i++)
//        {
//            Destroy(sortedList[i]);
//        }

//        generation++;
//    }

//    private GameObject Breed(GameObject parent1, GameObject parent2)
//    {
//        Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);   // it's the random location
//        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
//        DNA dna1 = parent1.GetComponent<DNA>();
//        DNA dna2 = parent2.GetComponent<DNA>();

//        // swap parent dna
//        if (Random.Range(0, 1000) > 5) // for most of the time, mix the traits
//        {
//            offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
//            offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
//            offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
//        }
//        else  // add a mutation - selects a random color
//        {
//            offspring.GetComponent<DNA>().r = Random.Range(0f, 10f);
//            offspring.GetComponent<DNA>().g = Random.Range(0f, 10f);
//            offspring.GetComponent<DNA>().b = Random.Range(0f, 10f);
//        }
//        return offspring;
//    }

//}
