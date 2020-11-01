using UnityEngine;

namespace uWintab
{

public class Pentate : MonoBehaviour 
{
    [SerializeField]
    Vector2 localScale = new Vector2(1.92f, 1.08f);

    [SerializeField]
    float hoverHeight = -0.03f;

    Tablet tablet_;
    float z_ = 0f;

    void Start()
    {
        tablet_ = FindObjectOfType<Tablet>();
    }

    void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }

    void UpdatePosition()
    {
        var x = (tablet_.x - 0.5f) * localScale.x;
        var y = (tablet_.y - 0.5f) * localScale.y;
        z_ = (tablet_.proximity ? -(Mathf.Sqrt(1 - Mathf.Pow((tablet_.x - 0.5f)*2,2))* localScale.x/2) : z_);
        //z_ = Mathf.Lerp(z_, (tablet_.proximity ? 0f : hoverHeight), 0.0f);
        transform.localPosition = new Vector3(x, y, z_);
    }

    void UpdateRotation()
    {
        var yaw = tablet_.azimuth * 360f;
        var pitch = (0.5f - tablet_.altitude) * 180f;
        var rot = Quaternion.Euler(pitch, yaw, 0f);
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, rot * Vector3.up);
    }
}

}