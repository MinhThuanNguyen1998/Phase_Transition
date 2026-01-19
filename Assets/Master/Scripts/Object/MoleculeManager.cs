using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
public class MoleculeManager : Singleton<MoleculeManager>
{
    [Header("Grid")]
    [SerializeField] private int m_FixedRows = 4;
    [SerializeField] private float m_Spacing = 0.3f;
    [SerializeField] private List<MoleculeMotion> m_MoleculesList;
    [SerializeField] private GameObject m_MoleculePrefab;
    [SerializeField] private Transform m_SpawnPosition;
    [Header("Bound")]
    [SerializeField] private BoxCollider m_Bound;
    [Header("Pressure")]
    [SerializeField] private PressureController m_PressureController;
    [SerializeField] private List<PressureThreshold> m_PressureThresholdList;
    [SerializeField] private StateChangeController m_StateChangeController;
    [Header("AirPumpMoving")]
    [SerializeField] private AirPumpMoving m_AirPumpMoving;
    [Header("PointPhaseController")]
    [SerializeField] private PointPhaseController m_PointPhaseController;
    public BoxCollider Bound => m_Bound;
    private float m_OffsetBoundX = 0.6f;
    private float m_MoleculeAmount = 48f;
    private float m_MoveDuration = 0.6f;
    private Ease m_Ease = Ease.OutQuad;
    private void Start() => ArrangeAs2DSolid(); 
    public void SpawnMolecule() 
    {
        AudioMainManager.Instance.PlayOnShot(SoundType.Pumping);
        bool isOver = IsMoleculeOverLimit();
        m_StateChangeController.WarnIfMoleculesFull(isOver);
        if (isOver) 
        {
            m_AirPumpMoving.StopAutoPump();
            return;
        }
        CheckMoleculeAmountToIncreasePressure();
        m_PointPhaseController.Move(Config.POINT_SPEED_ON_MOLECULE_CREATION);
        GameObject molecule = Instantiate(m_MoleculePrefab, m_SpawnPosition.position, m_MoleculePrefab.transform.rotation, transform);
        MoleculeMotion motion = molecule.GetComponent<MoleculeMotion>();
        m_MoleculesList.Add(motion);
        MoveMoleculeGrid();
        ArrangeAs2DSolid();
    }
    private void MoveMoleculeGrid()
    {
        Bounds b = m_Bound.bounds;
        float targetX = transform.position.x - 0.04f;
        targetX = Mathf.Clamp(targetX, b.min.x + m_OffsetBoundX, b.max.x);
        transform.DOMoveX(targetX, 0.3f).SetEase(Ease.InOutSine);
    }
    private void ArrangeAs2DSolid()
    {
        if (m_MoleculesList.Count == 0) return;
        Vector3 origin = transform.position;
        for (int i = 0; i < m_MoleculesList.Count; i++)
        {
            int x = i / m_FixedRows;
            int y = i % m_FixedRows;
            Vector3 gridPos = origin + new Vector3(x * m_Spacing, y * m_Spacing, 0f);
            MoveNewMoleculeToGrid(m_MoleculesList[i], gridPos);
        }
    }
    private void MoveNewMoleculeToGrid(MoleculeMotion molecule, Vector3 targetPos)
    {
        Transform newMolecule = molecule.transform;
        newMolecule.DOMove(targetPos, m_MoveDuration)
        .SetEase(m_Ease)
        .OnComplete(() =>
        {
            molecule.SetOrigin(targetPos);
            molecule.Initialize(targetPos);
        });
    }
    private void CheckMoleculeAmountToIncreasePressure()
    {
        int count = m_MoleculesList.Count;
        foreach (var threshold in m_PressureThresholdList)
        {
            if (count >= threshold.moleculeCount && !threshold.triggered)
            {
                threshold.triggered = true;
                m_PressureController.IncreasePressureByStep(threshold.pressureStep);
                m_StateChangeController.ChangeState();
            }
        }
    }
    public bool IsMoleculeOverLimit()
    {
        bool isOver = m_MoleculesList.Count >= m_MoleculeAmount;
        return isOver;
    }
}

