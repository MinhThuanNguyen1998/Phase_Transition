
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> listObjectOnTop = new();
    [SerializeField] private List<GameObject> listObjectOnLeft = new();
    [SerializeField] private List<GameObject> listObjectOnDown = new();
    [SerializeField] private List<GameObject> listObjectOnRight = new();

    [Header("Focus & Arrow")]
    [SerializeField] private Image m_focusedImage;       // Khung highlight
    [SerializeField] private ArrowTutorialUI m_arrow;    // Mũi tên hướng dẫn
    [SerializeField] private Canvas m_mainCanvas;        // Canvas (Screen Space - Overlay)
    [SerializeField] private GameObject m_infoObjectPanel;

    [Header("Setting")]
    [SerializeField] private float m_arrowOffset = 100f; // Khoảng cách giữa arrow và target
    [SerializeField] private float m_focusPadding = 1.3f;// Phóng to thêm khung focus

    private Camera mainCamera;
    private List<GameObject> allTargets = new();
    private int currentStep = 0;

    private void Awake()
    {
        mainCamera = Camera.main;
        SortAllLists();
        MergeListsInOrder();
    }
    private void OnEnable() => Reset();

    // ==============================
    // === RESET TUTORIAL
    // ==============================
    private void Reset()
    {
        currentStep = -1;
        ShowNextStep();
    }
    // ==============================
    // === SẮP XẾP DANH SÁCH THEO HƯỚNG
    // ==============================
    private void SortAllLists()
    {
        listObjectOnLeft = TutorialUIHelper.SortByScreenPosition(listObjectOnLeft, mainCamera, true, true);   // Trên → Dưới
        listObjectOnRight = TutorialUIHelper.SortByScreenPosition(listObjectOnRight, mainCamera, true, false); // Dưới → Trên
        listObjectOnTop = TutorialUIHelper.SortByScreenPosition(listObjectOnTop, mainCamera, false, true);    // Phải → Trái
    }
    private void MergeListsInOrder()
    {
        allTargets.Clear();
        allTargets.AddRange(listObjectOnTop);
        allTargets.AddRange(listObjectOnLeft);
        allTargets.AddRange(listObjectOnRight);
        allTargets.AddRange(listObjectOnDown);
    }
    public int ShowNextStep()
    {
        if (currentStep >= allTargets.Count - 1) return EndTutorial();
        return ShowStep(currentStep + 1);
    }
    public int ShowPreViousStep()
    {
        return ShowStep(currentStep - 1);
    }
    public int ShowStep(int index)
    {
        if (allTargets == null || allTargets.Count == 0) return -1;
        // Giới hạn chỉ số
        index = Mathf.Clamp(index, 0, allTargets.Count - 1);
        currentStep = index;
        var target = allTargets[currentStep];
        if (target == null) return HandleNullTarget();
        FocusHelper.SetupArrow(m_arrow, target, mainCamera, m_mainCanvas, m_arrowOffset, listObjectOnTop, listObjectOnDown, listObjectOnLeft, listObjectOnRight, this);
        FocusHelper.UpdateImage(m_focusedImage, target, mainCamera, m_mainCanvas, m_focusPadding);
        FocusHelper.PositionInfoPanel(m_infoObjectPanel, m_arrow, m_mainCanvas);
        //Debug.Log($"Step {currentStep}: {target.name}");
        return currentStep;
    }
    private int EndTutorial()
    {
        m_arrow.gameObject.SetActive(false);
        m_focusedImage.gameObject.SetActive(false);
        return -1;
    }
    private int HandleNullTarget()
    {
        Debug.Log($"UI reset vì object thứ {currentStep} là null, tutorial đã kết thúc");
        Reset();
        return -1;
    }
}
