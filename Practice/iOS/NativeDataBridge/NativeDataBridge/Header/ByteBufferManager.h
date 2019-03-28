#ifndef _BYTEBUFFERMANAGER_H_
#define _BYTEBUFFERMANAGER_H_
#include "Common.h"
#include <unordered_map>

#define LWNTEST

namespace LWN
{
	class ByteBuffer;
	class FileProxy;
	class ByteBufferManager
	{
	public:
#ifdef LWNTEST
		static FLOAT memeryTotal;
		static FLOAT memeryDelete;
		static FLOAT memeryReload;
#endif
	public:
		static ByteBuffer& GetByteBuffer(INT bufferID);
		static ByteBuffer& CreateByteBuffer(STRING szFileName, UINT dataSize, UINT offset, UINT len);
		static VOID Destroy();
		static INT DestroyDataByBufferId(UINT bufferID);
		static ByteBuffer& ReloadByteBuffer(ByteBuffer& byteBuffer);
	private:
		static std::unordered_map<INT, ByteBuffer&> bufferMap;
	};
}

#endif

