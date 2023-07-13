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
    public int _ammoCount;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private float _fireRate = 2.0f;

    public int kills;

    private void Start()
    {
        kills = 0;
        _uiManager = FindObjectOfType<UIManager>();

        _score = 0;
        _ammoCount = 25;
        Cursor.lockState = CursorLockMode.Locked;
        _uiManager.UpdateAmmoAmount();
    }

    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(Time.time > _canFire && _ammoCount > 0)
            {
                _canFire = _fireRate + Time.time;
                AudioManager.Instance.FireWeapon();

                Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hitInfo;

                if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 6 | 1 << 7 | 1<< 8))
                {
                    AI_Movement aiMovement = hitInfo.collider.GetComponent<AI_Movement>();

                    if (aiMovement != null)
                    {
                        AudioManager.Instance.AIDeath();
                        aiMovement._currentState = AI_Movement.AIState.Death;
                        _uiManager.UpdateScore(aiMovement.killPoint);
                        kills++;
                        if (kills < 16)
                        {
                            UIManager.Instance.UpdateKills();
                        }
                        else
                        {
                            GameManager.Instance.youWin = true;
                        }
                    }
                    else if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Barrel"))
                    {
                        ExplodingBarrel barrel = hitInfo.collider.GetComponent<ExplodingBarrel>();
                        barrel.Shot();
                    }
                    else if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                    {
                        AudioManager.Instance.HitBarrier();
                        Barrier barrierHit = hitInfo.collider.GetComponent<Barrier>();
                        barrierHit.Damage();
                    }

                }

                _ammoCount--;
                _uiManager.UpdateAmmoAmount();
            }
            else
            {
                return;
            }
        }
    }
}
