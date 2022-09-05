using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PannalInteraction : MonoBehaviour
{
    [System.Serializable]
    struct Textes 
    {
        [SerializeField] public string txt;
        [SerializeField, Range(0.1f,10.0f)] public float delay;

        public Textes(string a = "", float b = 1f)
        {
            this.txt = a;
            this.delay = b;
        }
    }

    [SerializeField] List<Textes> txts;
    [SerializeField] Collider2D interactionCollider;
    [SerializeField, Range(0.1f,10f)] float _defaultTextDelay;
    private TextMeshPro txtComponent;
    private Coroutine _corout;

    private void Reset()
    {
        _defaultTextDelay = 1f;
    }

    void Start()
    {
        txtComponent = GetComponentInChildren<TextMeshPro>();
    }

    public void PlayerIn()
    {
        if (_corout != null) return;

        if (txts.Count > 0)
        {
            txtComponent.enabled = true;
            _corout = StartCoroutine(ReadTxts());
        }
    }

    public void PlayerOut()
    {
        StopCoroutine(_corout);
        txtComponent.enabled = false;
        _corout = null;
    }

    IEnumerator ReadTxts()
    {
        for (int i = 0; i < txts.Count; i++)
        {
            if (txts[i].txt != null)
            {
                txtComponent.text = txts[i].txt;

                yield return new WaitForSeconds(txts[i].delay > 0 ? txts[i].delay : _defaultTextDelay);
            }
        }

        PlayerOut();
    }
}
