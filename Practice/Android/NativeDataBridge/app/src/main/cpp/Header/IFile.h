#ifndef _IFILE_H_
#define _IFILE_H_
#include "Common.h"
#include "stdio.h"
namespace AmQ
{
	class IFile
	{
	protected:
		bool Valid;
		UINT m_uOffset;

	public:
		__inline bool IsValid() { return Valid; }

	public:
		IFile():Valid(false), m_uOffset(0){}
		virtual ~IFile(){}

		virtual bool open(STRING szFileName) = 0;

		virtual bool read(VOID* buffer,UINT dataSize) = 0;

		__inline void seek(UINT offset) { m_uOffset = offset;}

		virtual UINT length() = 0;
	};
}
#endif


