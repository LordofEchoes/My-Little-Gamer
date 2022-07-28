using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeController : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MasterPref = "MasterPref";
    private static readonly string BGMPref = "BGMPref";
    private static readonly string SFXPref = "SFXPref";
    private static readonly string MasterMutePref = "MasterMutePref";
    private static readonly string BGMMutePref = "BGMMutePref";
    private static readonly string SFXMutePref = "SFXMutePref";
    private int firstPlayInt;
    public static float MasterVolume {get; private set;}
    public static float BGMVolume {get; private set;}
    public static float SFXVolume {get; private set;}

    public bool MasterMute, BGMMute, SFXMute;

    [SerializeField] private TextMeshProUGUI MasterSliderText;
    [SerializeField] private TextMeshProUGUI BGMSliderText;
    [SerializeField] private TextMeshProUGUI SFXSliderText;

    [SerializeField] private Slider MasterSliderBar;
    [SerializeField] private Slider BGMSliderBar;
    [SerializeField] private Slider SFXSliderBar;

    [SerializeField] private Toggle MasterCheckBox;
    [SerializeField] private Toggle BGMCheckBox;
    [SerializeField] private Toggle SFXCheckBox;

    //Clamping function for bounding floats
    public static float Clamp( float value, float min, float max )
{
    return (value < min) ? min : (value > max) ? max : value;
}

    // Start is called before the first frame update
    void Start()
    {
        //is Player's first time playing?
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0){
            //Initialize Volume Values
            MasterVolume = 1.00f;
            BGMVolume = 1.00f;
            SFXVolume = 1.00f;

            //mute checkboxes
            MasterMute = false;
            BGMMute = false;
            SFXMute = false;

            //Update Slider bar Values
            MasterSliderBar.value = MasterVolume;
            BGMSliderBar.value = BGMVolume;
            SFXSliderBar.value = SFXVolume;

            //Update Text box Values
            MasterSliderText.text = ((int)(MasterVolume)).ToString();
            BGMSliderText.text = ((int)(BGMVolume)).ToString();
            SFXSliderText.text = ((int)(SFXVolume)).ToString();

            MasterCheckBox.isOn = MasterMute;
            BGMCheckBox.isOn = BGMMute;
            SFXCheckBox.isOn = SFXMute;

            //Push updates to PlayerPrefs
            PlayerPrefs.SetFloat(MasterPref, MasterVolume);
            PlayerPrefs.SetFloat(BGMPref, BGMVolume);
            PlayerPrefs.SetFloat(SFXPref, SFXVolume);

            PlayerPrefs.SetInt(FirstPlay, -1);

            //Sanity push of checkboxes
            PlayerPrefs.SetInt(MasterMutePref, (MasterMute) ? 1 : 0 );
            PlayerPrefs.SetInt(BGMMutePref, (BGMMute) ? 1 : 0 );
            PlayerPrefs.SetInt(SFXMutePref, (SFXMute) ? 1 : 0 );

            //boolean access with 
            //bool = (PlayerPrefs.GetInt("NameofBool") != 0);
        }
        else {
            //Pull Player Preferences
            MasterVolume = PlayerPrefs.GetFloat(MasterPref);
            BGMVolume = PlayerPrefs.GetFloat(BGMPref);
            SFXVolume = PlayerPrefs.GetFloat(SFXPref);

            MasterMute = (PlayerPrefs.GetInt(MasterMutePref)!= 0);
            BGMMute = (PlayerPrefs.GetInt(BGMMutePref)!= 0);
            SFXMute = (PlayerPrefs.GetInt(SFXMutePref)!= 0);

            //Update Slider bar Values
            MasterSliderBar.value = MasterVolume;
            BGMSliderBar.value = BGMVolume;
            SFXSliderBar.value = SFXVolume;

            //Update Text box Values
            MasterSliderText.text = ((int)(MasterVolume)).ToString();
            BGMSliderText.text = ((int)(BGMVolume)).ToString();
            SFXSliderText.text = ((int)(SFXVolume)).ToString();
        }
    }

    //Slider Update Functions
    public void OnMasterSliderValuechange(float value)
    {
        MasterVolume = value;
        MasterSliderText.text = ((int)(value*100)).ToString();
        //PlayerPrefs.SetFloat(MasterPref, MasterVolume);
    }
    public void OnBGMSliderValuechange(float value)
    {
        BGMVolume = value;
        BGMSliderText.text = ((int)(value*100)).ToString();
        //PlayerPrefs.SetFloat(BGMPref, BGMVolume);
    }

    public void OnSFXSliderValuechange(float value)
    {
        SFXVolume = value;
        SFXSliderText.text = ((int)(value*100)).ToString();
        //PlayerPrefs.SetFloat(SFXPref, SFXVolume);
    }

    //Text update functions
    public void OnMasterTextValuechange(int value)
    {
        MasterSliderBar.value = ((float)value);
        MasterSliderText.text = ((int)(value*100)).ToString();
    }

    public void OnBGMTextValuechange(int value)
    {
        BGMSliderBar.value = ((float)value);
        BGMSliderText.text = ((int)(value*100)).ToString();
    }
    public void OnSFXTextValuechange(int value)
    {
        

        SFXSliderBar.value = ((float)value);
        SFXSliderText.text = ((int)(value*100)).ToString();
    }

    //CheckBox Mute Functions
    public void OnMasterCheckBoxchange(bool value)
    {
        MasterCheckBox.isOn = value;
        //PlayerPrefs.SetInt(MasterMutePref, (value) ? 1 : 0 );
    }

    public void OnBGMCheckBoxchange(bool value)
    {
        BGMCheckBox.isOn = value;
        //PlayerPrefs.SetInt(BGMMutePref, (value) ? 1 : 0 );
    }


    public void OnSFXCheckBoxchange(bool value)
    {
        SFXCheckBox.isOn = value;
        //PlayerPrefs.SetInt(SFXMutePref, (value) ? 1 : 0 );
    }

    //Save all Function
    public void SavePrefs()
    {
        PlayerPrefs.SetFloat(MasterPref, MasterVolume);
        PlayerPrefs.SetFloat(BGMPref, BGMVolume);
        PlayerPrefs.SetFloat(SFXPref, SFXVolume);

        PlayerPrefs.SetInt(MasterMutePref, (MasterMute) ? 1 : 0 );
        PlayerPrefs.SetInt(BGMMutePref, (BGMMute) ? 1 : 0 );
        PlayerPrefs.SetInt(SFXMutePref, (SFXMute) ? 1 : 0 );
    }

    // Update is called once per frame
}
