# VirtualCameraDriver
Windows virtual camera driver using the AVStream minidriver.

This project was a challenge for myself. I wanted to create a virtual camera driver for Windows.

The project has two parts: the *Windows driver* and the *user mode apps* which can push frames to the driver.

## Driver
The driver is based on the **avshws** driver example from Microsoft.

The filter implemented in this driver is extended with a custom property which accepts a buffer (1280x720, RGB), this buffer is then copied to the output buffer.

* *GUID* of the property set: *{CB043957-7B35-456E-9B61-5513930F4D8E}*
* *ID* of the property: *0*

Accessing this property can be done using DirectShow.

### Driver installation:
After building the driver (Windows SDK and Windows Driver Kit required) the inf can be installed using **hdwwiz.exe** (can be launched in CMD).


## UserMode apps
These applications can push frames to the driver using the property exposed in the filter. The apps are based on the **Driver** interface library which handles enumerating devices and setting the value of the property. This is written in VC++.

There are two example applications:
* **UserDriverStaticImage**: This app can push static images to the driver.
* **UserDriverCanon**: This application can push the live view of a Canon EOS camera to the driver, essentially turning it into a webcam. EDSDK not included in this repository!
