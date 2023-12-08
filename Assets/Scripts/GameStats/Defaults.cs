using UnityEngine;

namespace GameStats
{
    /// <summary>
    /// Set default values
    /// </summary>
    public class Defaults : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}