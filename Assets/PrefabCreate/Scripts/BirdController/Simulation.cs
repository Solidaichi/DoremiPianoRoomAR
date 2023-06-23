using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField]
    int boidCount;

    [SerializeField]
    GameObject boidPrefab;

    [SerializeField]
    Param param;

    private GameObject dominoObj;

    List<Boid> boids_ = new List<Boid>();
    public ReadOnlyCollection<Boid> boids
    {
        get { return boids_.AsReadOnly(); }
    }

    private void Start()
    {
        dominoObj = GameObject.FindGameObjectWithTag("domino");
        //Debug.Log("domino : " + dominoObj);
    }


    void AddBoid()
    {
        var go = Instantiate(boidPrefab, dominoObj.transform.position, Random.rotation);
        Debug.Log("go : " + go.transform.position);
        go.transform.SetParent(transform);
        var boid = go.GetComponent<Boid>();

        boid.simulation = this;
        boid.param = param;
        boids_.Add(boid);
    }

    void RemoveBoid()
    {
        if (boids_.Count == 0) return;

        var lastIndex = boids_.Count - 1;
        var boid = boids_[lastIndex];
        Destroy(boid.gameObject);
        boids_.RemoveAt(lastIndex);
    }

    void Update()
    {
        while (boids_.Count < boidCount)
        {
            AddBoid();
        }
        while (boids_.Count > boidCount)
        {
            RemoveBoid();
            Debug.Log("Remove");
        }


    }

    void OnDrawGizmos()
    {
        if (!param) return;
        Gizmos.color = Color.green;
        // dominoObj.transform.position
        Gizmos.DrawWireCube(dominoObj.transform.position, Vector3.one * param.wallScale);
    }
}