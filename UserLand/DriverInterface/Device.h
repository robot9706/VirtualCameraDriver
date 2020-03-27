#pragma once

#include "Common.h"

#define PROP_GUID 0xcb043957, 0x7b35, 0x456e, 0x9b, 0x61, 0x55, 0x13, 0x93, 0xf, 0x4d, 0x8e
#define PROP_DATA_ID 0

#define WIDTH 1280
#define HEIGHT 720

class Device
{
private:
	IBaseFilter* filter;
	IKsPropertySet* propertySet;
public:
	Device(IBaseFilter* filter);
	~Device();

	int Init();

	int SetData(PVOID dataPointer, ULONG dataLength);
};

