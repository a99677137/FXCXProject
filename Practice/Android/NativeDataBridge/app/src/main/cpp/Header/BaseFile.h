#ifndef _BASE_FILE_H_
#define _BASE_FILE_H_
#include "Common.h"
#include "stdio.h"
#include "IFile.h"
namespace AmQ
{
	class BaseFile:public IFile
	{
		friend class FileProxy;
	private:
		FILE * fp;
	private:
		BaseFile();
	public:
		virtual ~BaseFile();

		bool open(STRING szFileName);

		bool read(VOID* buffer,UINT dataSize);

		UINT length();
	};
}
#endif


