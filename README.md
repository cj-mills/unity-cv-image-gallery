# Unity CV Image Gallery
CVImageGallery is a Unity package that provides an interactive image gallery and a Scroll View prefab, designed to facilitate testing of computer vision applications, such as image classification, object detection, pose estimation, and style transfer. The image gallery allows users to select different images at runtime to test their models, offering customization options for image sizes and content panel adjustments.



## Getting Started

### Prerequisites

- Unity game engine

### Installation

#### Step 1: Install the UnityMediaDisplay package

First, install the UnityMediaDisplay package using the Unity Package Manager:

1. Open your Unity project.
2. Go to Window > Package Manager.
3. Click the "+" button in the top left corner, and choose "Add package from git URL..."
4. Enter the GitHub repository URL: `https://github.com/cj-mills/Unity-Media-Display.git`
5. Click "Add". The UnityMediaDisplay package will be added to your project.

#### Step 2: Install the CVImageGallery package

After installing the UnityMediaDisplay package, install the CVImageGallery package:

1. Go back to the Unity Package Manager.
2. Click the "+" button in the top left corner, and choose "Add package from git URL..."
3. Enter the GitHub repository URL: `https://github.com/cj-mills/unity-cv-image-gallery.git`
4. Click "Add". The CVImageGallery package will be added to your project.

For Unity versions older than 2021.1, add the Git URL to the `manifest.json` file in your project's `Packages` folder as a dependency:

```json
{
  "dependencies": {
    "com.cj-mills.cv-image-gallery": "https://github.com/cj-mills/Unity-Media-Display.git",
    // other dependencies...
  }
}
```



## Usage

1. Import the CVImageGallery package into your project.
2. Add a Quad Screen object to the Unity scene to display the selected images.
3. Add a Canvas object to the Unity scene for the user interface.
4. Attach the `GalleryScrollView` prefab (renamed from "Scroll View") to the Canvas object.
5. Select the `GalleryScrollView > Viewport > Content` object in the Unity hierarchy.
6. In the `ImageGallery` script component, assign the Screen, Camera, Content, and Content > `ImagePrefab` objects to their respective fields.
7. Add the images you want to display in the gallery as Sprites to the `imageSprites` list in the `ImageGallery` script component.




## License

This project is licensed under the MIT License. See the [LICENSE](Documentation~/LICENSE) file for details.