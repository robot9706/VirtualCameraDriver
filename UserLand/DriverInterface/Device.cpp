#include "Device.h"

const GUID GUID_PROP_CLASS = { PROP_GUID };

Device::Device(IBaseFilter* filter)
	: filter(filter), propertySet(NULL)
{
}

Device::~Device()
{
	if (propertySet != NULL)
	{
		propertySet->Release();
	}

	filter->Release();
}

int Device::Init()
{
	HRESULT hr = filter->QueryInterface(IID_PPV_ARGS(&propertySet));
	if (!SUCCEEDED(hr)) 
	{
		return 0;
	}

	DWORD supportFlags = 0;
	hr = propertySet->QuerySupported(GUID_PROP_CLASS, PROP_DATA_ID, &supportFlags);
	if (!SUCCEEDED(hr)) 
	{
		return 0;
	}

	if (supportFlags & KSPROPERTY_SUPPORT_SET != KSPROPERTY_SUPPORT_SET)
	{
		return 0;
	}

	return 1;
}

int Device::SetData(PVOID dataPointer, ULONG dataLength)
{
	if (dataLength != WIDTH * HEIGHT * 3) 
	{
		return -1;
	}

	HRESULT hr = propertySet->Set(GUID_PROP_CLASS, PROP_DATA_ID, NULL, 0, dataPointer, dataLength);

	return SUCCEEDED(hr);
}