#include "../Header/FileProxy.h"
#include "../Header/DebugLog.h"
#if __ANDROID__
#include "../Android/AssetFile.h"
#endif
#include "../Header/BaseFile.h"

namespace LWN
{
	FileProxy::FileProxy()
		:file(NULL)
	{
	}
	FileProxy::FileProxy(STRING szFileName)
		:file(NULL)
	{
		open(szFileName);
	}


	FileProxy::~FileProxy()
	{
		if(file !=NULL)
		{
			delete file;
			file = NULL;
		}
	}

	bool FileProxy::open(STRING szFileName)
	{
		if (file != NULL)
		{
			delete file;
			file = NULL;
		}
#if __ANDROID__
		if (szFileName[0] != '/')
		{
			file = new AssetFile();
			return file->open(szFileName);
		}
#endif
		file = new BaseFile();
		return file->open(szFileName);

	}
}



