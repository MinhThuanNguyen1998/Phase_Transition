using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TestMoleculesManager : MonoBehaviour
{
    [SerializeField] private List<Transform> m_MoleculesList;
    
    
    [SerializeField] private GameObject m_MoleculePrefab;
    [SerializeField] private Transform m_MoleCuleSpawnPosition;

    private int m_FixedRows = 5;
    private float m_Spacing = 0.3f;

    private void Start()
    {
        ArrangeAs2DSolid();
    }

    private void Update()
    {
        if (Input.GetKeyDown("a")) SpawnMolecule();
    }
    private void ArrangeAs2DSolid()
    {
        int count = m_MoleculesList.Count;
        if (count == 0) return;
        int columns = Mathf.CeilToInt((float)count / m_FixedRows);
        Vector3 origin = transform.position;
        for (int i = 0; i < count; i++)
        {
            int x = i / m_FixedRows;  
            int y = i % m_FixedRows;
            Vector3 pos = new Vector3(x * m_Spacing,y * m_Spacing,0f);
            m_MoleculesList[i].position = origin + pos;
        }
    }
    public void SpawnMolecule()
    {
        GameObject molecule = Instantiate(m_MoleculePrefab);
        molecule.transform.position = m_MoleCuleSpawnPosition.position;
        molecule.transform.parent = transform;
        AddMoleculeIncremental(molecule.transform);

    }
    public void AddMoleculeIncremental(Transform newMolecule)
    {
        if (newMolecule == null) return;
        m_MoleculesList.Add(newMolecule);
        newMolecule.SetParent(transform);
        ArrangeAs2DSolid();
    }
}
