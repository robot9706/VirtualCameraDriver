#include "Common.h"
#include "DeviceEnumeration.h"
#include "Device.h"

#define NUM_MAX_PATHS 16
static string cachedPaths[NUM_MAX_PATHS];
static int numDevices;

static Device* activeDevice = NULL;

#define TEMPORARY_BUFFER_SIZE (WIDTH * HEIGHT * 3)
static PVOID temporaryBuffer = NULL;

EXPORT int Init()
{
	HRESULT hr = CoInitializeEx(NULL, COINIT_MULTITHREADED);
	if (!SUCCEEDED(hr))
	{
		return 0;
	}

	numDevices = EnumerateDevicePaths(cachedPaths, NUM_MAX_PATHS);
	if (numDevices < 0) {
		return 0;
	}

	temporaryBuffer = malloc(TEMPORARY_BUFFER_SIZE);
	if (temporaryBuffer == NULL)
	{
		return 0;
	}

	return 1;
}

EXPORT int Free()
{
	if (activeDevice != NULL) 
	{
		delete activeDevice;
	}

	free(temporaryBuffer);

	CoUninitialize(); 

	return 1;
}

EXPORT int GetNumDevices()
{
	return numDevices;
}

EXPORT int GetDevicePath(int index, char* str, int maxLen)
{
	if (index < 0 || index >= numDevices) {
		return -1;
	}

	strcpy_s(str, maxLen, cachedPaths[index].c_str());

	return 1;
}

EXPORT void DestroyDevice()
{
	if (activeDevice != NULL)
	{
		delete activeDevice;
		activeDevice = NULL;
	}
}

EXPORT int SetDevice(char* str, int strLen) 
{
	DestroyDevice();

	IBaseFilter* filter = NULL;
	if (!GetFilter(string(str), &filter) || filter == NULL)
	{
		return 0;
	}

	activeDevice = new Device(filter);
	if (!activeDevice->Init()) 
	{
		delete activeDevice;
		activeDevice = NULL;

		return 0;
	}

	return 1;
}

EXPORT int SetBuffer(PVOID data, DWORD stride, DWORD width, DWORD height)
{
	if (activeDevice == NULL) 
	{
		return -1;
	}

	if (width != WIDTH || height != HEIGHT) 
	{
		return -1;
	}

	memset(temporaryBuffer, 0x00, TEMPORARY_BUFFER_SIZE);

	PUCHAR inputData = (PUCHAR)data;
	PUCHAR buffer = (PUCHAR)temporaryBuffer;
	for (ULONG y = 0; y < height; y++)
	{
		PUCHAR sourceLine = inputData + stride * y;
		PUCHAR targetLine = buffer + ((WIDTH * 3) * y);
		memcpy(targetLine, sourceLine, WIDTH * 3);
	}

	activeDevice->SetData(temporaryBuffer, TEMPORARY_BUFFER_SIZE);

	return 1;
}