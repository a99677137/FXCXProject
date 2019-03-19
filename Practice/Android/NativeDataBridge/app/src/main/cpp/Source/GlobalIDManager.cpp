#include "../Header/GlobalIDManager.h"

namespace LWN
{
	INT GlobalIDManager::__GlobalIDS = 0;
	INT GlobalIDManager::GenerateGlobalID()
	{
		return ++__GlobalIDS;
	}
	VOID GlobalIDManager::ResetGlobalID()
	{
		__GlobalIDS = 0;
	}
}


