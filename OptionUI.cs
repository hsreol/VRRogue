using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionUI : MonoBehaviour
{
    public GameObject cam;
    public Vector3 addPos_Stat;
    public Vector3 addPos_HP;

    public Button Video;

    //HP관련
    public GameObject HP_Bar;
    public GameObject HP_Control;
    public Button HP_Button;
    
    public Slider HP_X;
    public TextMeshProUGUI hp_x_value;
    public Slider HP_Y;
    public TextMeshProUGUI hp_y_value;
    public Slider HP_Z;
    public TextMeshProUGUI hp_z_value;

    //Status관련
    public GameObject Status;
    public GameObject Status_Control;
    public Button Status_Button;
    
    public Slider Status_X;
    public TextMeshProUGUI status_x_value;
    public Slider Status_Y;
    public TextMeshProUGUI status_y_value;
    public Slider Status_Z;
    public TextMeshProUGUI status_z_value;

    //오디오
    public Button Audio;
    
    public Slider BGM;
    public TextMeshProUGUI BGM_Value;

    public Slider SFX;
    public TextMeshProUGUI SFX_Value;

    public Button Back;
    public Button GameQuit;

    public AudioMixer AM;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Video.gameObject.SetActive(true);
        Audio.gameObject.SetActive(true);
        GameQuit.gameObject.SetActive(true);

        BGM.gameObject.SetActive(false);
        SFX.gameObject.SetActive(false);

        Back.gameObject.SetActive(false);

        HP_Control.gameObject.SetActive(false);
        Status_Control.gameObject.SetActive(false);

        HP_Button.gameObject.SetActive(false);
        Status_Button.gameObject.SetActive(false);
    }

    public void Click_Audio()
    {
        Video.gameObject.SetActive(false);
        Audio.gameObject.SetActive(false);
        GameQuit.gameObject.SetActive(false);

        BGM.gameObject.SetActive(true);
        SFX.gameObject.SetActive(true);

        Back.gameObject.SetActive(true);

        HP_Control.gameObject.SetActive(false);
        Status_Control.gameObject.SetActive(false);

        HP_Button.gameObject.SetActive(false);
        Status_Button.gameObject.SetActive(false);
    }

    public void Click_Video()
    {
        Video.gameObject.SetActive(false);
        Audio.gameObject.SetActive(false);
        GameQuit.gameObject.SetActive(false);

        BGM.gameObject.SetActive(false);
        SFX.gameObject.SetActive(false);

        Back.gameObject.SetActive(true);

        HP_Control.gameObject.SetActive(false);
        Status_Control.gameObject.SetActive(false);

        HP_Button.gameObject.SetActive(true);
        Status_Button.gameObject.SetActive(true);
    }

    public void Click_Back()
    {
        Video.gameObject.SetActive(true);
        Audio.gameObject.SetActive(true);
        GameQuit.gameObject.SetActive(true);

        BGM.gameObject.SetActive(false);
        SFX.gameObject.SetActive(false);

        Back.gameObject.SetActive(false);

        HP_Control.gameObject.SetActive(false);
        Status_Control.gameObject.SetActive(false);

        HP_Button.gameObject.SetActive(false);
        Status_Button.gameObject.SetActive(false);
    }
    public void Click_HP()
    {
        Video.gameObject.SetActive(false);
        Audio.gameObject.SetActive(false);
        GameQuit.gameObject.SetActive(false);

        BGM.gameObject.SetActive(false);
        SFX.gameObject.SetActive(false);

        Back.gameObject.SetActive(true);

        HP_Control.gameObject.SetActive(true);
        Status_Control.gameObject.SetActive(false);

        HP_Button.gameObject.SetActive(false);
        Status_Button.gameObject.SetActive(false);
    }
    public void Click_Status()
    {
        Video.gameObject.SetActive(false);
        Audio.gameObject.SetActive(false);
        GameQuit.gameObject.SetActive(false);

        BGM.gameObject.SetActive(false);
        SFX.gameObject.SetActive(false);

        Back.gameObject.SetActive(true);

        HP_Control.gameObject.SetActive(false);
        Status_Control.gameObject.SetActive(true);

        HP_Button.gameObject.SetActive(false);
        Status_Button.gameObject.SetActive(false);
    }

    public void HPUI()
    {
        int x = int.Parse(HP_X.value.ToString());
        int y = int.Parse(HP_Y.value.ToString());
        int z = int.Parse(HP_Z.value.ToString());

        hp_x_value.text = x.ToString();
        hp_y_value.text = y.ToString();
        hp_z_value.text = z.ToString();

        addPos_HP = new Vector3(x, y, z);
    }

    public void StatusUI()
    {
        int x = int.Parse(Status_X.value.ToString());
        int y = int.Parse(Status_Y.value.ToString());
        int z = int.Parse(Status_Z.value.ToString());

        status_x_value.text = x.ToString();
        status_y_value.text = y.ToString();
        status_z_value.text = z.ToString();

        addPos_Stat = new Vector3(x/50, y/50, z/50);
    }

    public void SFXControl()
    {
        float sound = SFX.value;
        SFX_Value.text = SFX.value.ToString();
        
        if (sound == -40f) AM.SetFloat("SFX", -80);
        else AM.SetFloat("SFX", sound);
    }

    public void BGMControl()
    {
        float sound = BGM.value;
        BGM_Value.text = BGM.value.ToString();

        if (sound == -40f) AM.SetFloat("BGM", -80);
        else AM.SetFloat("BGM", sound);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
