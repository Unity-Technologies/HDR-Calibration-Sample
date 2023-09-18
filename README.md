# HDR Calibration Sample
This HDR Calibration Sample project shows how an HDR calibration menu with **Neutral Tonemapping** can be implemented.

**Requirement:**
- Unity 2022.3.10f1 or above
- Universal Render Pipeline (URP) or High Definition Render Pipeline (HDRP)

<img src="https://github.com/Unity-Technologies/HDR-Calibration-Sample/assets/25224986/53ca705f-c496-4fe9-9b32-ce2e8d9d53f3" width="600">

## Getting the project
1. Clone this repository OR click [here](https://github.com/Unity-Technologies/HDR-Calibration-Sample/archive/refs/heads/main.zip) to download project zip
3. Open project with Unity Editor (please use the version listed in the above Requirement section)

## How to use
1. Open “HDRCalibrationSample” scene
2. Hit play
3. Control - you can either use mouse or keyboard or game controller to interact with the UI
   
| Function             | Mouse / Touch                               | Keyboard          | Game Controller                           |
| -------------------- | ------------------------------------------- | ----------------- | ----------------------------------------- |
| Menu Item Select     | Click on-screen buttons                     | Arrow keys        | D-Pad Up/Down                             |
| Confirm              | Click on-screen buttons                     | Enter             | South Button                              |
| Back                 | Click on-screen buttons                     | Esc / Backspace   | East Button                               |
| Show/Hide UI         | Click on-screen buttons                     | Tab               | North Button                              |
| Display Info         | Click on-screen buttons                     | Shift             | West Button                               |
| Switch Sample Images | Click on-screen buttons<br>Swipe left/right | PageUp / PageDown | Left/Right Trigger<br>Left/Right Shoulder |

### Default Tonemapping values
<img src="https://github.com/Unity-Technologies/HDR-Calibration-Sample/assets/25224986/32d98e2a-a829-453d-b4d7-502d844b3358"><br>
- If HDR is available on the current display, the default values will be using the `HDROutputSettings` API `paperWhiteNits`, `minToneMapLuminance` and `maxToneMapLuminance` values.
- If HDR is not available or the `HDROutputSettings` API couldn’t detect the luminance values, the default will be a set of hard-coded values.

### Sample images
<img src="https://github.com/Unity-Technologies/HDR-Calibration-Sample/assets/25224986/83284de6-2227-4a73-9766-a0b3550b582d" width="600"><br>
The HDR Calibration Sample comes with a few .EXR images captured from the HDRP template project. The images showcase some outdoor (bright) and indoor (dark) environments.
When you are implementing your own calibration scenes, you should use the brightest and darkest environments in your project.
<br>
These images are useful for users to adjust the brightness values to make the bright part not overexposed, dark part not completely invisible, and the UI is still clear to read.

### Display info
<img src="https://github.com/Unity-Technologies/HDR-Calibration-Sample/assets/25224986/8ed3808a-948b-4d45-9907-613697779ed3" width="600"><br>
The popup prints the important information using the `HDROutputSettings` API. 
<br>
Note that the luminance values might change according to screen brightness, and is only effective on player start (i.e. if you change screen brightness while player is running, you will have to restart the player to refresh the value).
<br>
For more information about HDROutputSettings API, visit [documentation](https://docs.unity3d.com/ScriptReference/HDROutputSettings.html).

### Error popup
<img src="https://github.com/Unity-Technologies/HDR-Calibration-Sample/assets/25224986/5bddf70d-d75b-476c-b629-29374ea59230" width="600"><br>
If GameView or player is not running in HDR, the popup will appear and indicate possible reasons of why HDR is not available.

### Fullscreen min/max calibration
<img src="https://github.com/Unity-Technologies/HDR-Calibration-Sample/assets/25224986/cbf424c2-7556-48bf-9f4f-75a1f6c7d623" width="600"><br>
The full-screen calibration allows users to calibrate the tonemapping values with max/min brightness. It has 3 pages:
1. The background has maximum brightness. The Unity logo brightness value determines the tonemapping **max nits**.
2. The background is completely dark (0 brightness). The Unity logo brightness value determines the tonemapping **min nits**.
3. Half of the background is in max brightness and half in 0 brightness. This page allows users to adjust a suitable **paper white** value for shader value 1 elements like UI. This is expected that while adjusting the brightness on this page, both the background and logo / UI brightness will be affected.

### Camera stacking
The “HDRCalibrationSample" also showcases the setup for camera stacking. In order for HDR rendering to work, all cameras need to be in the same color space. Therefore, make sure **Tonemapping is only applied on the last camera** on the camera stack.
<br>
<img src="https://github.com/Unity-Technologies/HDR-Calibration-Sample/assets/25224986/5f0bf037-ccfc-4ff6-b7bf-46efb19643c7" width="600"><br>
In the “HDRCalibrationSample scene, there is:
- Main Camera
   - It is rendering 3D objects and only taking Volume_MainCamera for Post-processing which is on layer “Default”.
   - On the VolumeProfile_MainCamera, you can apply any post-processing effects besides Tonemapping.
- UI Camera
   - It is rendering UI Canvas on UI layer, only takes Volume_Tonemapping for Post-processing, which is on layer “Tonemapping”.
   - On the VolumeProfile_Tonemapping, Tonemapping must be only applied here as this VolumeProfile is being used on the last camera of the camera stack. You can apply further post-processing effects as well.

### SpectrumTestScene
<img src="https://github.com/Unity-Technologies/HDR-Calibration-Sample/assets/25224986/97ff6e35-c54b-4c31-9f9a-8432e93faf56" width="600"><br>

Open the “SpectrumTestScene” scene and this scene is useful for testing the colors on the display.













