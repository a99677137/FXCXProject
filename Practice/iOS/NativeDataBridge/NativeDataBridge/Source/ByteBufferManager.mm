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
		alog(@"----ByteBufferManager::GetByteBuffer----bufferID = %d", bufferID);
		std::unordered_map<INT, ByteBuffer&>::iterator iter = bufferMap.find(bufferID);
		if (iter != bufferMap.end())
		{
			if (iter->second.Valid) {
				return iter->second;
			}
			else {
				ReloadByteBuffer(iter->second);
				alog(@"----ByteBufferManager::GetByteBuffer----after reload---vaild = %d", iter->second.Valid);
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
					filename[len] =  '\0';
					alog(@"----ByteBufferManager::CreateByteBuffer----filename = %s",filename);
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

	INT ByteBufferManager::DestroyDataByBufferId(UINT bufferID) {
		if (bufferID == 0) {
			return -1;
		}
		std::unordered_map<INT, ByteBuffer&>::iterator iter = bufferMap.find(bufferID);
		if (iter != bufferMap.end())
		{
			ByteBuffer& buf = iter->second;
			alog(@"----------ByteBufferManager::DestroyDataByBufferId----bufferID=%d filename=%s Vaild=%d DataSize=%d", bufferID, buf.FileName, buf.Valid, buf.DataSize);
			buf.DeleteData();
			alog(@"----------ByteBufferManager::DestroyDataByBufferId----bufferID=%d filename=%s ", bufferID, buf.FileName);
			return 1.0;
		}
		return 0;
	}

	ByteBuffer& ByteBufferManager::ReloadByteBuffer(ByteBuffer& byteBuffer) {
		STRING fileName = byteBuffer.FileName;
		int dataSize = byteBuffer.DataSize;
		int offset = byteBuffer.Offset;
		alog(@"----ByteBufferManager::ReloadByteBuffer----fileName=%s dataSize=%d offset=%d",fileName,dataSize,offset);
		LWN::FileProxy file;
		if (file.open(fileName))
		{
			alog(@"----ByteBufferManager::ReloadByteBuffer----file.open(fileName)");
			UINT fileLen = file.length();
			alog(@"----ByteBufferManager::ReloadByteBuffer----file.open(fileName)---fileLen = %d", fileLen);
			if ( dataSize <= 0) {
				alog(@"----ByteBufferManager::ReloadByteBuffer----file.open(fileName)---dont seek");
				dataSize = fileLen;
			}
			file.seek(offset);
			alog(@"----ByteBufferManager::ReloadByteBuffer----file.open(fileName)---file.seek");
			if (offset + dataSize > fileLen)
			{
				alog(@"----ByteBufferManager::ReloadByteBuffer----file.open(fileName)---offset + dataSize > fileLen");
				return byteBuffer;
			}
			else
			{
				CHAR* buffer = new CHAR[dataSize];
				if (file.read(buffer, dataSize))
				{
					alog(@"----ByteBufferManager::ReloadByteBuffer----file.open(fileName)---file.read ");
					byteBuffer.buffer = (BYTE*)buffer;
					byteBuffer.Valid = true;
					alog(@"----ByteBufferManager::ReloadByteBuffer----file.open(fileName)---Valid = true");
				}

			}
		}
		alog(@"----ByteBufferManager::ReloadByteBuffer----file.open(fileName)---return byteBuffer");
		return byteBuffer;
	}
	
}
