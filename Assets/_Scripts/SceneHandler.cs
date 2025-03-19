using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : SingletonMonoBehavior<SceneHandler>
{
    [Header("Scene Data")]
    [SerializeField] private List<string> levels;
    [SerializeField] private string menuScene;
    [Header("Transition Animation Data")]
    [SerializeField] private Ease animationType;
    [SerializeField] private float animationDuration;
    [SerializeField] private RectTransform transitionCanvas;

    private int nextLevelIndex;
    private float initXPosition;

    protected override void Awake()
    {
        base.Awake();
        initXPosition = transitionCanvas.transform.localPosition.x;
        SceneManager.LoadScene(menuScene);
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode _)
    {
        // transitionCanvas.DOLocalMoveX(initXPosition, animationDuration).SetEase(animationType);
    }

    public void LoadNextScene()
    {
        if(nextLevelIndex >= levels.Count)
        {
            LoadMenuScene();
        }
        else
        {
            var distanceToCoverScreen = transitionCanvas.rect.width * 1.5f;
            transitionCanvas.DOLocalMoveX(initXPosition + distanceToCoverScreen, animationDuration).SetEase(animationType);
            StartCoroutine(LoadSceneAfterTransition(levels[nextLevelIndex]));
            nextLevelIndex++;
        }
    }

    public void LoadMenuScene()
    {
        transitionCanvas.DOLocalMoveX(initXPosition + transitionCanvas.rect.width, animationDuration).SetEase(animationType);
        StartCoroutine(LoadSceneAfterTransition(menuScene));
        nextLevelIndex = 0;
    }

    private IEnumerator ResetTransitionCanvas() 
    {
        yield return new WaitForSeconds(animationDuration);
        var localPosition = transitionCanvas.localPosition;
        transitionCanvas.localPosition = new Vector3(initXPosition, localPosition.y, localPosition.z);
    }

    private IEnumerator LoadSceneAfterTransition(string scene)
    {
        yield return new WaitForSeconds(animationDuration);
        SceneManager.LoadScene(scene);

        var distanceToCoverScreen = transitionCanvas.rect.width * 1.5f;
        var distanceToLeaveScreen = distanceToCoverScreen * 2.0f;
        transitionCanvas.DOLocalMoveX(initXPosition + distanceToLeaveScreen, animationDuration).SetEase(animationType);
        StartCoroutine(ResetTransitionCanvas());
    }
}
