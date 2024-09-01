using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VisaoJogador : MonoBehaviour
{
    public float zoom;
    public float sensibilidadeZoom;
    public float zoomMaximo, zoomMinimo;

    private Vector3 posicaoAtual;
    private Vector3 diferenca;
    private Vector3 ResetCamera;

    private bool drag = false;

    private void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            Camera.main.orthographicSize -= zoom * Time.deltaTime * sensibilidadeZoom;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            Camera.main.orthographicSize += zoom * Time.deltaTime * sensibilidadeZoom;
        }

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, zoomMinimo, zoomMaximo);
    }

    private void LateUpdate()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                diferenca = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (drag == false)
                {
                    drag = true;
                    posicaoAtual = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                drag = false;
            }

            if (drag)
            {
                Camera.main.transform.position = posicaoAtual - diferenca;
                Camera.main.transform.position = new Vector3(
                    Mathf.Clamp(Camera.main.transform.position.x, -30, 30),
                    Mathf.Clamp(Camera.main.transform.position.y, -30, 30), transform.position.z);
            }

            if (Input.GetMouseButton(1))
            {
                Camera.main.transform.position = ResetCamera;
            }
        }
    }
}
