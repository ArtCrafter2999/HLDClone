using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedAudioSource : MonoBehaviour
{
    public void Play(AudioClip clip)
    {
        var obj = new GameObject
        {
            transform =
            {
                position = transform.position
            }
        };
        obj.AddComponent<AudioSource>().PlayOneShot(clip);
    }
}
