﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private RectTransform _ScrollVlew;

    public void SceneLoad(int scenceIndex)
    {
        StartCoroutine(ESceneLoade(scenceIndex, 2.5f));
    }
    public void MainSceneLoade()
    {

    }
    private IEnumerator ESceneLoade(int scenceIndex, float time)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;
            _ScrollVlew.localPosition = Vector2.Lerp(_ScrollVlew.localPosition, new Vector2(0, 1000), ratio);

            yield return null;
        }
        SceneManager.LoadScene(scenceIndex);
    }
}
