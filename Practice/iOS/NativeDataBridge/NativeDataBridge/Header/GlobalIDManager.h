#ifndef _GLOBALIDMANAGER_H_
#define _GLOBALIDMANAGER_H_
#include "Common.h"
namespace LWN
{
	class GlobalIDManager
	{
	public:
		static INT GenerateGlobalID();
		static VOID ResetGlobalID();
	private:
		static INT __GlobalIDS;
	};
}
#endif


