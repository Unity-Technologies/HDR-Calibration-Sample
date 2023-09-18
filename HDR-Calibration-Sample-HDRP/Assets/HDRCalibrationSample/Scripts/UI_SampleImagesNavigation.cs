using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace HDRCalibrationSample
{
    public class UI_SampleImagesNavigation : MonoBehaviour
    {
        public GameObject[] sampleImages;
        public GameObject sampleImage0_Expand;
        public GameObject sampleImage1_Collapse;
        public Button[] dots;

        private RawImage[] rawImages;
        private Rect[] rawImageOriginalRects;
        private Rect rawImageDefaultRect;

        private int currentImg = 0;
        public Color dot_grey;

        void Awake()
        {
            //Get RawImages
            rawImages = new RawImage[sampleImages.Length];
            for(int i = 0; i < sampleImages.Length; i++)
            {
                rawImages[i] = sampleImages[i].GetComponent<RawImage>();
            }

            //For sample images composition
            rawImageOriginalRects = new Rect[sampleImages.Length];
            for(int i = 0; i < sampleImages.Length; i++)
            {
                if(i > 0)
                {
                    rawImageOriginalRects[i] = rawImages[i].uvRect;
                }
            }
            rawImageDefaultRect = new Rect(0,0,1,1);
        }

        void Start()
        {
            //Set default image
            UpdateSampleImage(0);

            //Add listener to all dots buttons
            for(int j = 0; j < dots.Length; j++)
            {
                int index = j;
                dots[j].onClick.AddListener(() => UpdateSampleImage(index));
            }
        }

        public void NextOrPrevImage(float direction)
        {
            currentImg += 1 * (int)Mathf.Sign(direction);

            if(currentImg >= sampleImages.Length)
            {
                currentImg = 0;
            }
            else if(currentImg < 0)
            {
                currentImg = sampleImages.Length-1;
            }

            UpdateSampleImage(currentImg);
        }

        public void UpdateSampleImage(int i)
        {
            currentImg = i;
            //change image
            for(int j = 0; j < sampleImages.Length; j++)
            {
                if(j == currentImg)
                {
                    sampleImages[j].SetActive(true);
                } 
                else
                {
                    sampleImages[j].SetActive(false);
                }
            }

            //Set dots color
            for(int j = 0; j < dots.Length; j++)
            {
                var colors = dots[j].colors;
                if(j == currentImg)
                {
                    colors.normalColor = Color.white;
                }
                else
                {
                colors.normalColor = dot_grey;
                }
                dots[j].colors = colors;
            }
        }

        public void SetRawImageOriginalRect()
        {
            for(int j = 0; j < sampleImages.Length; j++)
            {
                if(j==0)
                {
                    sampleImage0_Expand.SetActive(false);
                    sampleImage1_Collapse.SetActive(true);
                }
                else
                {
                    rawImages[j].uvRect = rawImageOriginalRects[j];
                }
            }
        }

        public void SetRawImageDefaultRect()
        {
            for(int j = 0; j < sampleImages.Length; j++)
            {
                if(j==0)
                {
                    sampleImage0_Expand.SetActive(true);
                    sampleImage1_Collapse.SetActive(false);
                }
                else
                {
                    rawImages[j].uvRect = rawImageDefaultRect;
                }
            }
        }

        //For image swipe
        private float startPos = 0f;
        private const float swipeThreshold = 100f;

        public void StartDrag()
        {
            startPos = Input.mousePosition.x;
        }

        public void EndDrag()
        {
            float delta = Input.mousePosition.x - startPos;
            if(Mathf.Abs(delta) > swipeThreshold)
            {
                NextOrPrevImage(delta);
            }
        }
    }
}