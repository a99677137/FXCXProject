#ifndef _BYTEBUFFERMANAGER_H_
#define _BYTEBUFFERMANAGER_H_
#include "Common.h"
#include <unordered_map>
namespace LWN
{
	class ByteBuffer;
	class FileProxy;
	class ByteBufferManager
	{
	public:
		static ByteBuffer& GetByteBuffer(INT bufferID);
		static ByteBuffer& CreateByteBuffer(STRING szFileName, UINT dataSize, UINT offset, UINT len);
		static VOID Destroy();
		static FLOAT DestroyDataByBufferId(UINT bufferID);
	private:
		static std::unordered_map<INT, ByteBuffer&> bufferMap;
	};
}

#endif

