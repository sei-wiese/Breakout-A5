﻿using TMPro;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;

    // Add a field to assign the Particle Effect Prefab
    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem brickExplosionEffect;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float score;


    private int currentBrickCount;
    private int totalBrickCount;


    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        if (brickExplosionEffect != null)
        {Instantiate(brickExplosionEffect, position, Quaternion.identity);}

        // add camera shake here
        CameraShake.Instance.ShakeCamera(0.3f, 0.5f);

        currentBrickCount--;
        IncrementScore();
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining, score: {score}");
        if(currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        maxLives--;
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        ball.ResetBall();
    }
    private void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
