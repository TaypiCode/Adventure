using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCloseScript : MonoBehaviour
{
    [SerializeField] private bool _needSave;
    private SaveGame _save;
    private void Start()
    {
        _save = FindObjectOfType<SaveGame>();
    }
    public void OnClose()
    {
        if (_needSave)
        {
            _save.SaveProgress();
        }
    }
}
