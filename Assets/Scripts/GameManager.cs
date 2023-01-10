using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerGroup;
    public GameObject player;
    public GameObject playerSprite;

    public GameObject mainMenu;
    public GameObject customScreen;
    public GameObject customScreenButton;
    GameObject articleScreen;
    public Transform camOgPosi;
    public Transform camCustomPosi;

    public Transform playerOgPosi;
    public Transform playerCustomPosi;

    public float camOgOrtosize;
    public float camCustomOrtosize;

    public bool pause;

    public void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Game")
        {
            //SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
            playerGroup = GameObject.FindGameObjectWithTag("PlayerGroup");
            PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
            player = playerController.gameObject;
            playerSprite = GameObject.FindGameObjectWithTag("Player");
        }

        articleScreen = Resources.Load<GameObject>("TecnicalInformationScreen");
    }

    public void MainMenu()
    {
        if (pause) { Resume(); } else { Pause(); }
    }

    public void Pause()
    {
        mainMenu.SetActive(true);
        EnablePlayerMovementAndCameraControler(false);
        pause = true;
    }

    public void Resume()
    {
        mainMenu.SetActive(false);
        EnablePlayerMovementAndCameraControler(true);
        pause = false;
    }

    public void Quit()
    {
        MainMapSave mainMapSave = FindObjectOfType<MainMapSave>();
        mainMapSave.Save();
        Application.Quit();
    }

    public void DeleteFiles()
    {

        MainMapSave.DeleteSave();
    }


    public void PlayerCustomScreen(bool enable)
    {

        if (enable)
        {

            /*//guardando posição origial e rotação original da camera
            Camera.main.GetComponent<CameraBehavior>().enabled = false;
            camOgPosi.position = Camera.main.transform.position;
            camOgPosi.rotation = Camera.main.transform.rotation;
            camOgOrtosize = Camera.main.orthographicSize;

            //guardando posição origial e rotação original do player
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<NavMeshAgent>().enabled = false;
            playerOgPosi.position = playerSprite.transform.position;
            playerOgPosi.rotation = playerSprite.transform.rotation;

            //setando a nova posição/rotação da camera
            Camera.main.transform.position = camCustomPosi.position;
            Camera.main.transform.rotation = camCustomPosi.rotation;
            Camera.main.orthographicSize = 8;

            //setando a nova posição/rotação do player
            playerSprite.transform.position = playerCustomPosi.position;
            playerSprite.transform.rotation = playerCustomPosi.rotation;

            
            //customScreenButton.SetActive(false);*/
            customScreen.SetActive(true);
            //customScreenButton.SetActive(false);


        }
        else
        {
            /*Camera.main.transform.position = camOgPosi.position;
            Camera.main.transform.rotation = camOgPosi.rotation;
            Camera.main.orthographicSize = camOgOrtosize;
            Camera.main.GetComponent<CameraBehavior>().enabled = true;

            playerSprite.transform.position = playerOgPosi.position;
            playerSprite.transform.rotation = playerOgPosi.rotation;
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<NavMeshAgent>().enabled = true;

            customScreen.SetActive(false);
            //customScreenButton.SetActive(true);*/
            customScreen.SetActive(false);
            //customScreenButton.SetActive(true);


        }

    }

    public void ArticleScreen()
    {
        GameObject go = GameObject.Instantiate(articleScreen, this.gameObject.transform.parent.position, transform.rotation * Quaternion.Euler(0, 0, 0), this.gameObject.transform.parent.transform);
        go.name = articleScreen.name;
    }

    public void EnablePlayer(bool enable)
    {
        playerGroup.SetActive(enable);
    }

    public void EnablePlayerMovement(bool enable)
    {
        player.GetComponent<PlayerController>().enabled = enable;
    }

    public void EnableCameraController(bool enable)
    {
        Camera.main.GetComponent<CameraBehavior>().enabled = enable;
    }

    public void EnablePlayerMovementAndCameraControler(bool enable)
    {

        player.GetComponent<PlayerController>().enabled = enable;
        Camera.main.GetComponent<CameraBehavior>().enabled = enable;

    }

    public void EnableCustomizationButton(bool enable)
    {
        //customScreenButton.SetActive(enable);
    }

   

}
