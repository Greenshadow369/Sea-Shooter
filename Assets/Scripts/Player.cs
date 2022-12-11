using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    public float moveSpeed;
    //[SerializeField] float currentMoveSpeed;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 delta;
    Shooter shooter;

    private void Awake() {
        shooter = GetComponent<Shooter>();
    }

    private void Start() {
        InitBounds();
        
    }

    void Update()
    {
        Movement();
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        
    }

    private void Movement()
    {
        delta = rawInput * moveSpeed * Time.deltaTime * StatModifier.instance.GetPlayerSpeedModifier();
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    private void OnFire(InputValue inputValue)
    {
        if(shooter != null)
        {
            shooter.isFiring = inputValue.isPressed;
        }
    }

    /*public void SpawnPlayer()
    {
        Debug.Log("spawn");
        Transform v = spawnPoint.transform;
        GameObject player = Instantiate(playerPrefab, v.position, v.rotation);
        //levelManager.onGameLoad -= SpawnPlayer;

    }*/

    /*public void ModifyMoveSpeed(float modifier)
    {
        float temp = moveSpeed;
        moveSpeed = (temp * modifier / 100f) + temp;
        
    }*/
}
