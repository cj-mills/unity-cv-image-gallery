using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if EXAMPLE_PACKAGE_INSTALLED
using MediaDisplay;

namespace CVGallery
{
    // This class handles the functionality of an image gallery,
    // allowing the user to select an image and display it on the specified screen.
    public class ImageGallery : MonoBehaviour
    {
        [Header("Scene")]
        [Tooltip("The screen GameObject where the selected image will be displayed")]
        public GameObject screenObject;
        [Tooltip("The camera GameObject used to display the selected image")]
        public GameObject cameraObject;
        [Tooltip("The content panel GameObject where the image gallery is located")]
        public GameObject contentPanel;
        [Tooltip("The image prefab used to create each image in the gallery")]
        public GameObject imagePrefab;
        public List<Sprite> imageSprites;
        public float spacing = 5f;
        public float specifiedWidth = 100f;

        private void Start()
        {
            SetupContentPanel();
            PopulateImageGallery();
            AdjustContentHeight();
            AssignButtonClickEvents();
        }

        /// <summary>
        /// Set up the content panel with a VerticalLayoutGroup component.
        /// </summary>
        private void SetupContentPanel()
        {
            VerticalLayoutGroup verticalLayoutGroup = contentPanel.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.spacing = spacing;
            verticalLayoutGroup.childAlignment = TextAnchor.UpperCenter;
            verticalLayoutGroup.childControlHeight = false;
            verticalLayoutGroup.childControlWidth = false;
            verticalLayoutGroup.childForceExpandHeight = false;
            verticalLayoutGroup.childForceExpandWidth = false;
        }

        /// <summary>
        /// Populate the image gallery with the sprites provided in imageSprites.
        /// </summary>
        private void PopulateImageGallery()
        {
            foreach (Sprite sprite in imageSprites)
            {
                GameObject newImageObject = Instantiate(imagePrefab, contentPanel.transform);
                Image newImage = newImageObject.GetComponent<Image>();
                newImageObject.SetActive(true);
                newImage.sprite = sprite;
                newImage.preserveAspect = true;
                newImageObject.name = sprite.name;

                RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
                float aspectRatio = sprite.rect.height / sprite.rect.width;
                rectTransform.sizeDelta = new Vector2(specifiedWidth, specifiedWidth * aspectRatio);
            }
        }

        /// <summary>
        /// Adjust the content panel height based on the total height of the images and spacing.
        /// </summary>
        private void AdjustContentHeight()
        {
            RectTransform contentPanelRectTransform = contentPanel.GetComponent<RectTransform>();
            float totalHeight = 0f;

            for (int i = 0; i < contentPanelRectTransform.childCount; i++)
            {
                RectTransform childRect = contentPanelRectTransform.GetChild(i).GetComponent<RectTransform>();
                totalHeight += childRect.sizeDelta.y;
            }

            totalHeight += spacing * (contentPanelRectTransform.childCount - 1);
            contentPanelRectTransform.sizeDelta = new Vector2(contentPanelRectTransform.sizeDelta.x, totalHeight);
        }

        /// <summary>
        /// Assigns click events to the images in the gallery to update the screen texture when clicked.
        /// </summary>
        private void AssignButtonClickEvents()
        {
            Image[] images = transform.GetComponentsInChildren<Image>();

            foreach (Image image in images)
            {
                Button button = image.GetComponent<Button>();
                if (button == null)
                {
                    button = image.gameObject.AddComponent<Button>();
                }

                button.onClick.AddListener(() => MediaDisplayManager.UpdateScreenTexture(screenObject, image.mainTexture, cameraObject, false));
            }
        }
    }
}
#else
Debug.LogWarning("Dependency not installed.");
#endif