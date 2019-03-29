#include "../Header/ByteBufferManager.h"
#include "../Header/ByteBuffer.h"
#include "../Header/FileProxy.h"
#include "../Header/GlobalIDManager.h"
#include "../Header/DebugLog.h"

namespace LWN
{
#ifdef LWNTEST
	FLOAT ByteBufferManager::memeryTotal = 0.f;
	FLOAT ByteBufferManager::memeryDelete = 0.f;
	FLOAT ByteBufferManager::memeryReload = 0.f;
#endif

	std::unordered_map<INT, ByteBuffer&> ByteBufferManager::bufferMap;

	ByteBuffer& ByteBufferManager::GetByteBuffer(INT bufferID)
	{
		//alog("----ByteBufferManager::GetByteBuffer----bufferID = %d", bufferID);
		std::unordered_map<INT, ByteBuffer&>::iterator iter = bufferMap.find(bufferID);
		if (iter != bufferMap.end())
		{
			if (iter->second.Valid) {
				return iter->second;
			}
			else {
				ReloadByteBuffer(iter->second);
				//alog("----ByteBufferManager::GetByteBuffer----after reload---vaild = %d", iter->second.Valid);
				return iter->second;
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
			if (dataSize <= 0) {
				dataSize = fileLen;
			}

			file.seek(offset);

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
					CHAR * filename = new CHAR[len+1];
					memcpy(filename, szFileName, len);
					filename[len] = '\0';
					//alog("----ByteBufferManager::CreateByteBuffer----filename = %s",filename);
					pByteBuffer->FileName = filename;
					pByteBuffer->Offset = offset;
					pByteBuffer->DataSize = dataSize;
					pByteBuffer->Valid = true;
					bufferMap.insert(std::unordered_map<INT, ByteBuffer&>::value_type(pByteBuffer->BufferID, *pByteBuffer));
#ifdef LWNTEST
					memeryTotal = memeryTotal + (FLOAT)dataSize;
					alog("----------ByteBufferManager::CreateByteBuffer----bufferID=%d filename=%s dataSize=%d memeryTotal=%fMB memeryDelete=%fMB memeryReload=%fMB", pByteBuffer->BufferID, pByteBuffer->FileName, pByteBuffer->DataSize, memeryTotal / 1024 / 1024, memeryDelete / 1024 / 1024, memeryReload / 1024/1024);
#endif
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

	INT ByteBufferManager::DestroyDataByBufferId(UINT bufferID) {
		if (bufferID == 0) {
			return -1;
		}
		std::unordered_map<INT, ByteBuffer&>::iterator iter = bufferMap.find(bufferID);
		if (iter != bufferMap.end())
		{
			ByteBuffer& buf = iter->second;
            if(buf.Valid == false){
                //alog("----------ByteBufferManager::DestroyDataByBufferId----bufferID=%d filename=%s Vaild=%d DataSize=%d buf.(Valid = false)!!!", bufferID, buf.FileName, buf.Valid, buf.DataSize);
                return 1;
            }
			//alog("----------ByteBufferManager::DestroyDataByBufferId----bufferID=%d filename=%s Vaild=%d DataSize=%d", bufferID, buf.FileName, buf.Valid, buf.DataSize);
			buf.DeleteData();
#ifdef LWNTEST
			memeryTotal = memeryTotal - (FLOAT)buf.DataSize;
			memeryDelete = memeryDelete + (FLOAT)buf.DataSize;
			alog("----------ByteBufferManager::DestroyDataByBufferId----bufferID=%d filename=%s dataSize=%d memeryTotal=%fMB memeryDelete=%fMB memeryReload=%fMB", bufferID, buf.FileName,buf.DataSize, memeryTotal/1024/1024, memeryDelete/1024/1024, memeryReload/1024/1024);
#endif
			return 1;
		}
		return 0;
	}

	ByteBuffer& ByteBufferManager::ReloadByteBuffer(ByteBuffer& byteBuffer) {
		STRING fileName = byteBuffer.FileName;
		int dataSize = byteBuffer.DataSize;
		int offset = byteBuffer.Offset;
		LWN::FileProxy file;
		if (file.open(fileName))
		{
			UINT fileLen = file.length();
			if ( dataSize <= 0) {
				dataSize = fileLen;
			}
			file.seek(offset);
			if (offset + dataSize > (int)fileLen)
			{
				return byteBuffer;
			}
			else
			{
				CHAR* buffer = new CHAR[dataSize];
				if (file.read(buffer, dataSize))
				{
					byteBuffer.buffer = (BYTE*)buffer;
					byteBuffer.Valid = true;
					//alog("----ByteBufferManager::ReloadByteBuffer----file.open(fileName)---Valid = true");
				}
#ifdef LWNTEST
				memeryReload = memeryReload + (FLOAT)dataSize;
				memeryTotal = memeryTotal + (FLOAT)dataSize;
				alog("----ByteBufferManager::ReloadByteBuffer----fileName=%s buffId=%d memeryReload=%fMB memeryTotal=%fMB memeryDelete=%fMB", fileName, byteBuffer.BufferID, memeryReload/1024/1024, memeryTotal/1024/1024, memeryDelete/1024/1024);
#endif
			}
		}
		return byteBuffer;
	}
	
}
