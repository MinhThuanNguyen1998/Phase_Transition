using DG.Tweening;
using UnityEngine;
public class ScalingMoleCuleBoundByLid : MonoBehaviour
{
    [SerializeField] private Transform m_MoleCuleScaling;
    [SerializeField] private Transform m_Lid;
    private float m_InitialLidY;
    private Vector3 m_InitialMoleculeScale;
    private void Start()
    {
        m_InitialLidY = m_Lid.position.y;
        m_InitialMoleculeScale = m_MoleCuleScaling.localScale;
    }
    private void Update() => UpdateMoleCubeBoundScaleByLid();
   
    private void UpdateMoleCubeBoundScaleByLid()
    {
        float deltaY = m_Lid.position.y - m_InitialLidY;
        Vector3 NewScale = m_InitialMoleculeScale;
        NewScale.y = m_InitialMoleculeScale.y + deltaY;
        m_MoleCuleScaling.localScale = NewScale;
    }
}
