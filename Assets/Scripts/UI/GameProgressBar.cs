using PathCreation.Examples;
using UnityEngine;
using UnityEngine.UI;
public class GameProgressBar : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    private PathFollower pathFollower;
    private void Awake()
    {
        progressBar.value = 0;
        pathFollower = FindObjectOfType<PathFollower>();
    }
    void Update()
    {
        progressBar.value = pathFollower.pathProgress;   
    }
}
