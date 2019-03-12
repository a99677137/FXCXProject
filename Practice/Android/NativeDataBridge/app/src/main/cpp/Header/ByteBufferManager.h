#ifndef _BYTEBUFFERMANAGER_H_
#define _BYTEBUFFERMANAGER_H_
#include "Common.h"
#include <unordered_map>
namespace AmQ
{
	class ByteBuffer;
	class FileProxy;
	class ByteBufferManager
	{
	public:
		static ByteBuffer& GetByteBuffer(INT bufferID);
		static ByteBuffer& CreateByteBuffer(FileProxy& file, UINT dataSize);
		static VOID Destroy();
	private:
		static std::unordered_map<INT, ByteBuffer&> bufferMap;
	};
}

#endif

