#include "../Header/ByteBufferManager.h"
#include "../Header/ByteBuffer.h"
#include "../Header/FileProxy.h"
#include "../Header/GlobalIDManager.h"

#include "../Header/android_log.h"

namespace AmQ
{
	std::unordered_map<INT, ByteBuffer&> ByteBufferManager::bufferMap;
	ByteBuffer& ByteBufferManager::GetByteBuffer(INT bufferID)
	{
		std::unordered_map<INT, ByteBuffer&>::iterator iter = bufferMap.find(bufferID);
		if (iter != bufferMap.end())
		{
			return iter->second;
		}
		return s_ByteBuffer;
	}
	VOID ByteBufferManager::Destroy()
	{
		for (auto v : bufferMap)
		{
			delete &(v.second);
		}
		bufferMap.clear();
	}
	ByteBuffer& ByteBufferManager::CreateByteBuffer(FileProxy& file, UINT dataSize)
	{
	    //LOGE("*************************ByteBufferManager:CreateFileBuffer-------dataSize = %d",dataSize);
		ByteBuffer* pByteBuffer = new ByteBuffer();
		CHAR* buffer = new CHAR[dataSize];
		if (file.read(buffer, dataSize))
		{
			pByteBuffer->buffer = (BYTE*)buffer;
			pByteBuffer->BufferID = AmQ::GlobalIDManager::GenerateGlobalID();
			pByteBuffer->Valid = true;
			bufferMap.insert(std::unordered_map<INT, ByteBuffer&>::value_type(pByteBuffer->BufferID, *pByteBuffer));
			return *pByteBuffer;
		}
		else
		{
			delete[] buffer;
			delete pByteBuffer;
		}
		return s_ByteBuffer;
	}
}
