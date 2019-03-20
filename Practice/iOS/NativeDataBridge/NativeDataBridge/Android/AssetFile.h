#if __ANDROID__
#ifndef _ASSET_FILE_H_
#define _ASSET_FILE_H_
#include "../Header/Common.h"
#include "../Header/IFile.h"
#include <android/asset_manager.h>
#include <android/asset_manager_jni.h>
namespace LWN
{
	class AssetFile :public IFile
	{
		friend class FileProxy;
	private:
		AssetFile();

		STRING m_szFileName;

		UINT   m_uSize;
	public:
		virtual ~AssetFile();

		bool open(STRING szFileName);

		bool read(VOID* buffer, UINT dataSize);

		UINT length();
	public:
		static AAssetManager*  s_pAssetMgr;
	};
}
#endif
#endif