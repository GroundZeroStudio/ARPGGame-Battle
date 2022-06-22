using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace INFramework.UI
{
	public class BasePanel
	{
		public GameObject GameObject { get; set; }
		public Transform Transform { get; set; }
		public string Name { get; set; }
		protected List<Button> mButtons { get; set; } = new List<Button>();
		protected List<Toggle> mToggles { get; set; } = new List<Toggle>();

        public virtual void Awake(params object[] rParams) { }
        public virtual void OnShow(params object[] rParams) { }
        public virtual void OnUpdate() { }
        public virtual void OnDispose() { }
        public virtual void OnClose()
        {
            RemoveAllButtonListener();
            RemoveAllToggleListener();
            mButtons.Clear();
            mToggles.Clear();
        }

        public bool SetReplaceImage(string rPath, Image rImage, bool bSetNativeSize = false)
        {
            if (rImage == null)
            {
                return false;
            }

            Sprite sp = ResMgr.Instance.Load<Sprite>(rPath);
            if (sp != null)
            {
                if (rImage.sprite != null)
                    rImage.sprite = null;
                rImage.sprite = sp;
                if (bSetNativeSize)
                {
                    rImage.SetNativeSize();
                }
                return true;
            }

            return false;
        }

        public void SetReplaceImageAsync(string rPath, Image rImage, bool bSetNativeSize)
        {
            if (rImage == null)
            {
                return;
            }
            ResMgr.Instance.LoadAsync<Sprite>(rPath, (sp) =>
            {
                if (sp != null)
                {
                    if (rImage.sprite != null)
                        rImage.sprite = null;
                    rImage.sprite = sp;
                    if (bSetNativeSize)
                    {
                        rImage.SetNativeSize();
                    }
                }
            });
        }


        public void RemoveAllButtonListener()
        {
            foreach (var button in mButtons)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        public void RemoveAllToggleListener()
        {
            foreach (var toggle in mToggles)
            {
                toggle.onValueChanged.RemoveAllListeners();
            }
        }

        public void AddButtonClickListener(Button rButton, UnityAction rAction)
        {
            if (rButton != null)
            {
                if (!mButtons.Contains(rButton))
                {
                    mButtons.Add(rButton);
                }
                rButton.onClick.RemoveAllListeners();
                rButton.onClick.AddListener(rAction);
                rButton.onClick.AddListener(ButtonPlaySound);
            }
        }

        protected void ButtonPlaySound()
        {

        }

        public void AddToggleClickListener(Toggle rToggle, UnityAction<bool> rAction)
        {
            if (rToggle != null)
            {
                if (!mToggles.Contains(rToggle))
                {
                    mToggles.Add(rToggle);
                }
                rToggle.onValueChanged.RemoveAllListeners();
                rToggle.onValueChanged.AddListener(rAction);
                rToggle.onValueChanged.AddListener(TogglePlaySound);
            }
        }

        protected void TogglePlaySound(bool bIson)
        {

        }
    }
}

