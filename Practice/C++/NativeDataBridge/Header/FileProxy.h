#ifndef _FILE_PROXY_H_
#define _FILE_PROXY_H_
#include "Common.h"
#include "IFile.h"
namespace LWN
{
	class FileProxy
	{
	private:
		IFile* file;
	public:
		FileProxy();

		FileProxy(STRING szFileName);

		~FileProxy();

		__inline bool IsValid() { if (file != NULL) return file->IsValid(); return false; }

		bool open(STRING szFileName);

		__inline bool read(VOID* buffer,UINT dataSize) { return file->read(buffer, dataSize); }

		__inline void seek(UINT offset) { file->seek(offset); }

		__inline UINT length() { return file->length(); }
	};
}
#endif


