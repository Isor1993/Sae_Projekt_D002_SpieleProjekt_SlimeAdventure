using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionSettings : MonoBehaviour
{
    [SerializeField] private GameObject _soundSlider;
    private AudioListener _audioListener;

    private void Awake()
    {
        _audioListener=GameBootstrapper.Instance.GetComponent<AudioListener>();
    }

    private void ChangeVolume()
    {
      
        
    }

}
