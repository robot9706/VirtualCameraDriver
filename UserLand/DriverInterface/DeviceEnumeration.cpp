#include "DeviceEnumeration.h"

HRESULT EnumerateDevices(REFGUID category, IEnumMoniker **ppEnum)
{
	ICreateDevEnum *pDevEnum;
	HRESULT hr = CoCreateInstance(CLSID_SystemDeviceEnum, NULL,
		CLSCTX_INPROC_SERVER, IID_PPV_ARGS(&pDevEnum));

	if (SUCCEEDED(hr))
	{
		hr = pDevEnum->CreateClassEnumerator(category, ppEnum, 0);
		if (hr == S_FALSE)
		{
			hr = S_FALSE;
		}
		pDevEnum->Release();
	}
	return hr;
}

int EnumerateDevicePaths(string* paths, int maxCount)
{
	int index = 0;

	IEnumMoniker *pEnum;

	HRESULT hr = EnumerateDevices(CLSID_VideoInputDeviceCategory, &pEnum);
	if (SUCCEEDED(hr))
	{
		IMoniker *pMoniker = NULL;
		while (pEnum->Next(1, &pMoniker, NULL) == S_OK && index < maxCount)
		{
			LPOLESTR str;
			pMoniker->GetDisplayName(0, 0, &str);

			size_t numChars = 0;
			char cstr[1024];
			wcstombs_s(&numChars, cstr, str, 512);

			paths[index].append(cstr);

			index++;
		}

		pEnum->Release();
	}
	else
	{
		return -1;
	}

	return index;
}

int GetFilter(string path, IBaseFilter** filter)
{
	bool found = false;

	IEnumMoniker *pEnum;

	HRESULT hr = EnumerateDevices(CLSID_VideoInputDeviceCategory, &pEnum);
	if (SUCCEEDED(hr))
	{
		IMoniker *pMoniker = NULL;
		while (pEnum->Next(1, &pMoniker, NULL) == S_OK)
		{
			LPOLESTR str;
			pMoniker->GetDisplayName(0, 0, &str);

			size_t numChars = 0;
			char cstr[1024];
			wcstombs_s(&numChars, cstr, str, 512);

			if (strcmp(path.c_str(), cstr) == 0)
			{
				hr = pMoniker->BindToObject(0, 0, IID_IBaseFilter, (void**)filter);
				if (SUCCEEDED(hr))
				{
					found = true;
				}

				break;
			}
		}

		pEnum->Release();
	}
	
	return found;
}