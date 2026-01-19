using DG.Tweening;
using UnityEngine;
public class ScalingMoleCuleBoundByLid : MonoBehaviour
{
    [SerializeField] private Transform m_Lid;

    private Transform m_MoleCuleScaling;
    private float m_InitialLidY;
    private Vector3 m_InitialMoleculeScale;

    private void Start()
    {
        // Tìm object có tag MoleculeBound
        GameObject moleculeObj = GameObject.FindGameObjectWithTag("MoleculeBound");
        if (moleculeObj != null)
        {
            m_MoleCuleScaling = moleculeObj.transform;
            m_InitialMoleculeScale = m_MoleCuleScaling.localScale;
        }
        else
        {
            Debug.LogError("Cannot find GameObject with tag 'MoleculeBound'");
        }

        m_InitialLidY = m_Lid.position.y;
    }

    private void Update() => UpdateMoleCubeBoundScaleByLid();

    private void UpdateMoleCubeBoundScaleByLid()
    {
        if (m_MoleCuleScaling == null || m_Lid == null) return;

        float deltaY = m_Lid.position.y - m_InitialLidY;
        Vector3 newScale = m_InitialMoleculeScale;
        newScale.y = m_InitialMoleculeScale.y + deltaY;
        m_MoleCuleScaling.localScale = newScale;
    }
}
