using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameComposteira : MonoBehaviour
{
    public bool activate;
    public int notasCount, resetNotas;
    public List<GameObject> notesToDestroy;
    public List<GameObject> camadasList;
    public GameObject prefab, camdas, camadaTerra, camadaGalinha;
    public GameObject nota;
    public Transform notasSpawner;
    public MinigamesValuesContainers composteiraData;
    public SetImage mySetImage;

    int setCamada;
    int spawncamada;

    [SerializeField]
    private string getId;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnNoata", 1.0f, 5.0f);
        //print("Acerte [" + notasCount + "] notas para concluir a tarefa");
        mySetImage = FindObjectOfType<SetImage>();
        resetNotas = notasCount;
        float i = composteiraData.GetData(getId);
        if (i == 1)
        {
            spawncamada = notasCount;
            do
            {
                SpawnCamada();
            } while (spawncamada > 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && activate)
        {
            Destroy(nota);
            notasCount -= 1;
            print("faltam [" + notasCount + "] para concluir a tarefa");
        }

        if (Input.GetMouseButtonDown(0) && activate)
        {
            Destroy(nota);
            CheckCamada();
            notasCount -= 1;
            print("faltam [" + notasCount + "] para concluir a tarefa");
        }

        if (notasCount <= 0)
        {
            notasCount = 0;
            CancelInvoke("SpawnNoata");
            foreach (GameObject i in notesToDestroy)
            {
                Destroy(i);
            }
            Encerrar();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "nota")
        {
            print(other.gameObject.name);
            nota = other.gameObject;
            activate = true;
        }
        else
        {
            activate = false;
        }

    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "nota")
        {
            activate = false;
            Destroy(nota, 2);
        }

    }

    public void SpawnNoata()
    {
        GameObject go = Instantiate(prefab, notasSpawner.position, notasSpawner.rotation, notasSpawner.transform);
        //go.transform.SetParent(notasSpawner.transform, true);
        go.name = "nota";
        notesToDestroy.Add(go);
    }

    public void CheckCamada()
    {
        float i = composteiraData.GetData(getId);
        if (i == 0)
        {
            if (setCamada == 0)
            {
                GameObject go = Instantiate(camadaTerra, camdas.transform.position, camdas.gameObject.transform.rotation, camdas.transform);
                camadasList.Add(go);
                go.name = camadaTerra.name;
                setCamada = 1;
            }
            else if (setCamada == 1)
            {
                GameObject go = Instantiate(camadaGalinha, camdas.transform.position, camdas.gameObject.transform.rotation, camdas.transform);
                go.name = camadaGalinha.name;
                camadasList.Add(go);
                setCamada = 0;
            }
        }
        else if (i == 1)
        {
            Destroy(camadasList[camadasList.Count - 1]);
            camadasList.RemoveAt(camadasList.Count - 1);
        }

    }

    public void SpawnCamada()
    {


        if (setCamada == 0)
        {
            GameObject go = Instantiate(camadaTerra, camdas.transform.position, transform.rotation * Quaternion.Euler(0,0,0), camdas.transform);
            camadasList.Add(go);
            go.name = camadaTerra.name;
            setCamada = 1;
        }
        else if (setCamada == 1)
        {
            GameObject go = Instantiate(camadaGalinha, camdas.transform.position, transform.rotation * Quaternion.Euler(0,0,0), camdas.transform);
            go.name = camadaGalinha.name;
            camadasList.Add(go);
            setCamada = 0;
        }

        spawncamada--;

    }


    public void Iniciar(string id)
    {

        SetImage setImage = FindObjectOfType<SetImage>();
        if (setImage.regionHealth != setImage.regioMaxHealth)
        {

            getId = id;

            this.gameObject.transform.parent.gameObject.SetActive(true);
            InvokeRepeating("SpawnNoata", 1.0f, 2.5f);
            print("Acerte [" + notasCount + "] notas para concluir a tarefa");
        }
        else
        {
            print("tudo certo por aqui");
        }


    }

    public void Encerrar()
    {

        float i = composteiraData.GetData(getId);
        if (i == 0)
        {
            AddHealth();
            i = 1;
            composteiraData.SetData(getId, i);
        }
        else if (i == 1)
        {
            i = 0;
            composteiraData.SetData(getId, i);
        }
        mySetImage.SetImageColor();
        //notasCount = resetNotas;
        RegionDescriptionContainer regionDescriptionData;
        regionDescriptionData = Resources.Load<RegionDescriptionContainer>("RegionDescriptionData");
        EnergyManager.RemovEnergy("value", regionDescriptionData.GetEstamina(mySetImage.region.ToString()));
        TimeManager.CalculateDestination(regionDescriptionData.GetTimeToComplete(mySetImage.region.ToString()));
        //this.gameObject.transform.parent.gameObject.SetActive(false);
        Close();
    }

    public void Close()
    {
        CancelInvoke("SpawnNoata");
        foreach (GameObject i in notesToDestroy)
        {
            Destroy(i);
        }

        float z = composteiraData.GetData(getId);
        if (z == 0)
        {
            foreach (GameObject i in camadasList)
            {
                Destroy(i);
            }
        }
        notasCount = resetNotas;
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void AddHealth()
    {
        RegionProperties[] regions = FindObjectsOfType<RegionProperties>();
        for (int i = 0; i < regions.Length; i++)
        {
            if (regions[i].region == mySetImage.region)
            {
                regions[i].IncreaseHealth();
            }
        }
    }

}
