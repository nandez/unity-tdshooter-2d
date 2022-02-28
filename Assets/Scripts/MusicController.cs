using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController _instance;

    public static MusicController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MusicController>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(gameObject);
        }
    }

    private void Start()
    {
        var _audioSource = GetComponent<AudioSource>();

        if (_audioSource.isPlaying)
            return;

        _audioSource.Play();
    }
}