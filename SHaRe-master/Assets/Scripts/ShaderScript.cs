using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderScript : MonoBehaviour {
    // Kaffetassenshader
    [SerializeField]
    private float _filHeight;
    [SerializeField]
    private float _milchAnteil;
    [SerializeField]
    private float _kaffeAnteil;
    [SerializeField]
    private Color _myColor = new Color(124, 88, 82);
    [SerializeField]
    private Color _brown = new Color(124, 88, 82);

    public Material ShaderMaterial;


    private bool _positionCounter = true;

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        Graphics.Blit(src, dst, ShaderMaterial);
    }
    public void setFillHeight(float height)
    {
        _filHeight = height;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G))
            _positionCounter = !_positionCounter;
        if (Input.GetKey(KeyCode.Return) && _positionCounter && _filHeight < 0.95f) {
            _kaffeAnteil += Time.deltaTime * 0.25f;
            _filHeight = _kaffeAnteil + _milchAnteil;
            _myColor = Color.Lerp(Color.white, _brown, _kaffeAnteil / _filHeight * 1.5f);
            Shader.SetGlobalColor("_MyColor", _myColor);
            Shader.SetGlobalFloat("_FillHeight", _filHeight);
        }

        if (Input.GetKey(KeyCode.Return) && !_positionCounter && _filHeight < 0.95f) {
            _milchAnteil += Time.deltaTime * 0.25f;
            _filHeight = _kaffeAnteil + _milchAnteil;
            _myColor = Color.Lerp(Color.white, _brown, _kaffeAnteil / _filHeight * 1.5f);
            Shader.SetGlobalColor("_MyColor", _myColor);
            Shader.SetGlobalFloat("_FillHeight", _filHeight);
        }

        if (Input.GetKey(KeyCode.R)) {
            _filHeight = _kaffeAnteil = _milchAnteil = 0;
            Shader.SetGlobalFloat("_FillHeight", _filHeight);
        }
    }
}
