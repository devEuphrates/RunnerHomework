using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] SaveChannelSO _saveCahannel;
    [SerializeField] IntSO _level;

    [Header("Events"), Space]
    [SerializeField] TriggerEventSO _loadNextLevel;
    [SerializeField] TriggerEventSO _restartLevel;
    [SerializeField] TriggerEventSO _levelSet;

    int _scene
    {
        get
        {
            int levelCount = SceneManager.sceneCountInBuildSettings - 1;
            int convertedIndex = _level.Value - 1;

            return convertedIndex >= levelCount ? convertedIndex % levelCount + 1 : convertedIndex + 1;
        }
    }

    void OnEnable()
    {
        _loadNextLevel.AddListener(LoadNextLevel);
        _restartLevel.AddListener(RestartLevel);
    }

    void OnDisable()
    {
        _loadNextLevel.RemoveListener(LoadNextLevel);
        _restartLevel.RemoveListener(RestartLevel);
    }

    void Start()
    {
        _saveCahannel.Load();
        LoadLevel();
    }

    public void LoadLevel() => SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive).completed += OnLoadFinish;

    void OnLoadFinish(AsyncOperation op)
    {
        Scene curScene = SceneManager.GetSceneByBuildIndex(_scene);
        SceneManager.SetActiveScene(curScene);
        _levelSet?.Invoke();
    }

    public void LoadNextLevel() => SceneManager.UnloadSceneAsync(_scene).completed += OnNextLevelUnloadFinish;

    void OnNextLevelUnloadFinish(AsyncOperation op)
    {
        _level.Value += 1;
        _saveCahannel.Save();
        LoadLevel();
    }

    void RestartLevel() => SceneManager.UnloadSceneAsync(_scene).completed += (AsyncOperation op) => LoadLevel();
}
