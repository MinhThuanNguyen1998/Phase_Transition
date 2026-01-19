using System;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    [SerializeField] DragOjectConfig dragOjectConfig;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private Rigidbody rb;
    private Collider col;

    public bool IsDragging => isDragging;
    public event Action OnBeingDragged;
   
    // Tốc độ kéo mượt
    private float smoothSpeed;
    private bool isInteractable = true;

    public static bool GlobalDisableDrag { get; set; } = false;

    public void ChangeDragState(bool isDraggable)
    {
        if(isDraggable == false && isDragging)
        {
            isDragging = false;
            ResetSate();
        }
        isInteractable = isDraggable;
    }

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        smoothSpeed = dragOjectConfig.smoothSpeed;
        col = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        if(!isInteractable || GlobalDisableDrag) return; 
        isDragging = true;
        col.enabled = false;
        OnBeingDragged?.Invoke();
        //AudioManager.Instance.PlaySFX(dragOjectConfig.dragAudioClip);
        // Nếu có Rigidbody thì tắt lực vật lý để điều khiển bằng tay
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Vector3 mouseWorldPos = GetMouseWorldPos();
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseUp()
    {
        if (!isDragging) return;
        //if (!isDragging || !isInteractable || GlobalDisableDrag) return;
        isDragging = false;

        // Bật lại Rigidbody để vật rơi/va chạm
        ResetSate();
        
    }
    private void ResetSate()
    {
        col.enabled = true;
    }

    void Update()
    {
        if(isInteractable == false || GlobalDisableDrag) return;
        if (isDragging)
        {
            Vector3 mouseWorldPos = GetMouseWorldPos();
            Vector3 targetPos = mouseWorldPos + offset;

            if (rb != null)
            {
                // Di chuyển mượt bằng Rigidbody
                rb.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed));
            }
            else
            {
                // Nếu không có Rigidbody thì dùng Lerp trực tiếp
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;

        // Lấy khoảng cách offset từ config
        float offsetX = dragOjectConfig.screenEdgeOffset.x;
        float offsetY = dragOjectConfig.screenEdgeOffset.y;

        // Giới hạn vị trí chuột trong vùng màn hình trừ offset
        mousePos.x = Mathf.Clamp(mousePos.x, offsetX, Screen.width - offsetX);
        mousePos.y = Mathf.Clamp(mousePos.y, offsetY, Screen.height - offsetY);

        // Chuyển sang toạ độ thế giới
        return mainCamera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, mainCamera.WorldToScreenPoint(transform.position).z)
        );
    }
}
