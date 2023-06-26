using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private AI_Movement _aiMovement;
    private UIManager _uiManager;
    [SerializeField]
    public int _score;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _score = 0;
    }

    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if(Physics.Raycast(rayOrigin, out hitInfo))
            {
                AI_Movement aiMovement = hitInfo.collider.GetComponent<AI_Movement>();
                if (aiMovement != null)
                {
                    aiMovement._currentState = AI_Movement.AIState.Death;
                    _uiManager.UpdateScore(aiMovement.killPoint);
                }
            }
        }
    }
}
