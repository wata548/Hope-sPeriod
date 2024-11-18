using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPhysics : MonoBehaviour
{
    Rigidbody2D playerRigidbody = null;

    Direction moveableDirection = DirectionInfo.ALL;
    Direction frictionDirection = DirectionInfo.ALL;

    float power = 5f;
    float frictionRatio = 0.5f;
    float frictionPower = 20;

    Vector2 playerVelocity = Vector2.zero;

    private void Awake() {

        if (playerRigidbody == null) {

            playerRigidbody = GetComponent<Rigidbody2D>();
        }

        SettingCollider.SetCollider(gameObject);
    }

    void Update()
    {
        Vector2 input = power * PlayerMovement.CalculateDirection(moveableDirection);


        bool isMoving = playerVelocity != Vector2.zero;

        if (isMoving && input == Vector2.zero) {

            float ratio = frictionPower * frictionRatio * Time.deltaTime;
            playerVelocity = PlayerMovement.CalculateFrictionPercent(frictionDirection, playerVelocity, ratio);
        }

        else if(input != Vector2.zero){
            playerVelocity = input;
        }

        var gravity = PlayerGravity.CalculateGravity(Direction.LEFT);

        playerRigidbody.linearVelocity = playerVelocity + gravity;
    }
}