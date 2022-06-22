using INFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frame.Test
{
	public class Test : MonoBehaviour
	{
        public AudioSource AudioSource;
        private void Start()
        {
            AudioMgr.Instance.PlaySound("AiOuLiYa_001_L_SE_Atk101_sf");
            //GameObject.Instantiate(ResMgr.Instance.Load<GameObject>("Cube"));
            //AudioClip clip = ResMgr.Instance.Load<AudioClip>("AiOuLiYa_001_L_SE_Atk101_sf.mp3");
            //ResMgr.Instance.LoadAsync<GameObject>("Cube", obj => obj.gameObject.transform.localScale = new Vector3(1.5f, 1, 1));
            //ResMgr.Instance.LoadAsync<AudioClip>("AiOuLiYa_001_L_SE_Atk101_sf", clip => AudioSource.clip = clip);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                AudioMgr.Instance.PlayMusic("AiOuLiYa_001_L_SE_Atk101_sf");
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                AudioMgr.Instance.StopMusic();
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                AudioMgr.Instance.PasueMusic();
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                AudioMgr.Instance.UnPauseMusic();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                AudioMgr.Instance.MusicOff();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                AudioMgr.Instance.MusicOn();
            }
        }
    }
}

