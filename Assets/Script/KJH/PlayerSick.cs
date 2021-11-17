using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSick : MonoBehaviour
{
    [SerializeField] UnityEngine.Rendering.Universal.UniversalAdditionalCameraData volumeCam;
    [SerializeField] float vignetteFloat = 0.4f;
    public bool _isSick { get; set; } = false;
    public float plusDark = 0.02f;

    private void Start()
    {
        vignetteFloat = 0.4f;
        volumeCam.SetRenderer(0);

    }
    void Update()
    {
        // Search Google  -------------------------------------------------------
         VolumeProfile volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(VolumeProfile));
        UnityEngine.Rendering.Universal.Vignette vignette;
        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
        vignette.intensity.Override(vignetteFloat);
        //-----------------------------------------------------------------------

        if(_isSick)
        vignetteFloat += plusDark * Time.deltaTime;

        if(vignetteFloat > 0.5f)
        {
            volumeCam.SetRenderer(1);

            if (vignetteFloat > 0.8f)
                plusDark = 0.6f;
        }
    }
}
