using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if CJM_UNITY_MEDIA_DISPLAY
using CJM.MediaDisplay;

namespace CJM.CVGallery
{
    // This class handles the functionality of an image gallery,
    // allowing the user to select an image and display it on the specified screen.
    public class ImageGallery : MonoBehaviour
    {
        [Header("Scene")]
        [Tooltip("The screen GameObject where the selected image will be displayed")]
        [SerializeField] private GameObject screenObject;
        [Tooltip("The camera GameObject used to display the selected image")]
        [SerializeField] private GameObject cameraObject;
        [Tooltip("The content panel GameObject where the image gallery is located")]
        [SerializeField] private GameObject contentPanel;
        [Tooltip("The image prefab used to create each image in the gallery")]
        [SerializeField] private GameObject imagePrefab;
        [Tooltip("A list of sprites to populate the image gallery.")]
        [SerializeField] private List<Sprite> imageSprites;
        [Tooltip(" The spacing between images in the gallery.")]
        [SerializeField] private float spacing = 5f;
        [Tooltip("The specified width for each image in the gallery.")]
        [SerializeField] private float specifiedWidth = 100f;

        private void Start()
        {
            // Configures the content panel with a VerticalLayoutGroup component.
            SetupContentPanel();
            // Populates the gallery with images using the provided sprites.
            PopulateImageGallery();
            // Adjusts the content panel height by summing the vertical dimensions of all gallery images and spacing.
            AdjustContentHeight();
            // Assigns click events to the images in the gallery to update the screen texture.
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
                // Instantiates a new GameObject using the imagePrefab
                GameObject newImageObject = Instantiate(imagePrefab, contentPanel.transform);
                Image newImage = newImageObject.GetComponent<Image>();
                newImageObject.SetActive(true);
                // Assign the curent sprite
                newImage.sprite = sprite;
                // Preserves the aspect ratio
                newImage.preserveAspect = true;
                // Use  the sprite's name for easier identification
                newImageObject.name = sprite.name;

                // Adjust the image size based on the specified width
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

            // Calculate the total height of all the images in the gallery
            for (int i = 0; i < contentPanelRectTransform.childCount; i++)
            {
                RectTransform childRect = contentPanelRectTransform.GetChild(i).GetComponent<RectTransform>();
                totalHeight += childRect.sizeDelta.y;
            }

            // Add the spacing between the images to the total height
            totalHeight += spacing * (contentPanelRectTransform.childCount - 1);
            // Updates the content panel to accommodate the total height
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
                // Add a Button component if the image doesn't already have one
                Button button = image.GetComponent<Button>();
                if (button == null)
                {
                    button = image.gameObject.AddComponent<Button>();
                }

                // Add a listener to update the screen texture to display the selected image when clicked
                button.onClick.AddListener(() => MediaDisplayManager.UpdateScreenTexture(screenObject, image.mainTexture, cameraObject, false));
            }
        }
    }
}
#endif