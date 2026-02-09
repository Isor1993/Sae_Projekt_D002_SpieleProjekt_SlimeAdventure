using UnityEngine;

public class Clouds : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _parallaxFaktor;
    [Range(-2f,2f)]
    [SerializeField] private float _cloudMovement;    

    public float ParallaxFaktor => _parallaxFaktor;
    public float CloudMovement => _cloudMovement;   

}
