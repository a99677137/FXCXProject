#include "../Header/ByteBufferManager.h"
#include "../Header/ByteBuffer.h"
#include "../Header/FileProxy.h"
#include "../Header/GlobalIDManager.h"
#include "../Header/DebugLog.h"

namespace LWN
{
	std::unordered_map<INT, ByteBuffer&> ByteBufferManager::bufferMap;

	ByteBuffer& ByteBufferManager::GetByteBuffer(INT bufferID)
	{
		std::unordered_map<INT, ByteBuffer&>::iterator iter = bufferMap.find(bufferID);
		if (iter != bufferMap.end())
		{
			if (iter->second.Valid) {
				return iter->second;
			}
			else {
				
			}
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

	ByteBuffer& ByteBufferManager::CreateByteBuffer(STRING szFileName, UINT dataSize, UINT offset, UINT len)
	{
		LWN::FileProxy file;
		if (file.open(szFileName))
		{
			UINT fileLen = file.length();
			if (offset < 0 || dataSize < 0) {
				dataSize = fileLen;
			}
			else {
				file.seek(offset);
			}
			if (offset + dataSize > fileLen)
			{
				return s_ByteBuffer;
			}
			else
			{
				ByteBuffer* pByteBuffer = new ByteBuffer();
				CHAR* buffer = new CHAR[dataSize];
				if (file.read(buffer, dataSize))
				{
					pByteBuffer->buffer = (BYTE*)buffer;
					pByteBuffer->BufferID = LWN::GlobalIDManager::GenerateGlobalID();
					CHAR * filename = new CHAR[len];
					memcpy(filename, szFileName, len);
					pByteBuffer->FileName = filename;
					pByteBuffer->Offset = offset;
					pByteBuffer->DataSize = dataSize;
					pByteBuffer->Valid = true;
					bufferMap.insert(std::unordered_map<INT, ByteBuffer&>::value_type(pByteBuffer->BufferID, *pByteBuffer));
					return *pByteBuffer;
				}
				else
				{
					delete[] buffer;
					delete pByteBuffer;
				}
			}
		}
		return s_ByteBuffer;
	}

	FLOAT ByteBufferManager::DestroyDataByBufferId(UINT bufferID) {
		if (bufferID < 0) {
			return -1;
		}
		std::unordered_map<INT, ByteBuffer&>::iterator iter = bufferMap.find(bufferID);
		if (iter != bufferMap.end())
		{
			ByteBuffer buf = iter->second;
			buf.DeleteData();
			return 1.0;
		}
		return 0;
	}

}
