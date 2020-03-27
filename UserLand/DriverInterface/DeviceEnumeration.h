#pragma once

#include "Common.h"

int EnumerateDevicePaths(string* paths, int maxCount);
int GetFilter(string path, IBaseFilter** filter);