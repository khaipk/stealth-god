using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshBuild : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
        
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Init()
    {
        BakeNavMesh();
    }

    private void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
