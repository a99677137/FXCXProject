#include "../Header/DebugLog.h"
#include "../Header/ByteBuffer.h"
#include "stdio.h"
#include "string.h"

namespace LWN
{
	//static int TestIsLittleEndian()
	//{
	//	int x = 1;
	//	if (*(char*)&x == 1)
	//	{
	//		return 1;
	//	}
	//	else
	//	{
	//		return 0;
	//	}
	//}

	template<typename T>
	static T bytes2T(BYTE* bytes, UINT offset)
	{
		T res = 0;
		int n = sizeof(T);
		memcpy(&res, bytes + offset, n);
		return res;
	}
	//template<typename T> 
	//static BYTE* T2bytes(T u) 
	//{ 
	//	int n = sizeof(T);     
	//	BYTE* b = new BYTE[n];
	//	memcpy(b, &u, n);     
	//	return b; 
	//}


	//UINT64 ReadLittleEndian(BYTE* bytes, UINT offset, UINT dataSize)
	//{
	//	UINT64 r = 0;
	//	if (ByteBuffer::IsLittleEndian)
	//	{
	//		for (UINT i = 0; i < dataSize; i++)
	//		{
	//			r |= (UINT64)bytes[offset + i] << i * 8;
	//		}
	//	}
	//	else
	//	{
	//		for (UINT i = 0; i < dataSize; i++)
	//		{
	//			r |= (UINT64)bytes[offset + dataSize - 1 - i] << i * 8;
	//		}
	//	}
	//	return r;
	//}

	//bool ByteBuffer::IsLittleEndian = true;
	ByteBuffer s_ByteBuffer;
	ByteBuffer::ByteBuffer()
		:Valid(false), BufferID(-1), buffer(NULL), FileName(NULL), Offset(-1), DataSize(-1)
	{
	}


	ByteBuffer::~ByteBuffer()
	{
		if (buffer != NULL)
		{
			delete[] buffer;
			buffer = NULL;
		}
	}
	BYTE ByteBuffer::GetByte(UINT offset)
	{
		return buffer[offset];
	}

	DOUBLE ByteBuffer::GetDouble(UINT offset)
	{
		return bytes2T<DOUBLE>(buffer, offset);
		//UINT64 data = ReadLittleEndian(buffer, offset, sizeof(DOUBLE));
		//DOUBLE* p = (DOUBLE*)&data;
		//return *p;
	}

	FLOAT ByteBuffer::GetFloat(UINT offset)
	{
		return bytes2T<FLOAT>(buffer, offset);
		//INT data = (INT)ReadLittleEndian(buffer, offset, sizeof(FLOAT));
		//float* p = (float*)&data;
		//return *p;
	}
	INT ByteBuffer::GetInt(UINT offset)
	{
		return bytes2T<INT>(buffer, offset);
		//return (INT)ReadLittleEndian(buffer, offset, sizeof(INT));
	}

	INT64 ByteBuffer::GetLong(UINT offset)
	{
		return bytes2T<INT64>(buffer, offset);
		//return ReadLittleEndian(buffer, offset, sizeof(INT64));
	}

	SBYTE ByteBuffer::GetSbyte(UINT offset)
	{
		SBYTE* buff = (SBYTE*)buffer;
		return buff[offset];
	}

	SHORT ByteBuffer::GetShort(UINT offset)
	{
		return bytes2T<SHORT>(buffer, offset);
		//return (SHORT)ReadLittleEndian(buffer, offset, sizeof(SHORT));
	}

	USHORT ByteBuffer::GetUShort(UINT offset)
	{
		return bytes2T<USHORT>(buffer, offset);
		//return (USHORT)ReadLittleEndian(buffer, offset, sizeof(USHORT));
	}

	UINT ByteBuffer::GetUInt(UINT offset)
	{
		return bytes2T<UINT>(buffer, offset);
		//return (UINT)ReadLittleEndian(buffer, offset, sizeof(UINT));
	}

	UINT64 ByteBuffer::GetULong(UINT offset)
	{
		return bytes2T<UINT64>(buffer, offset);
		//return ReadLittleEndian(buffer, offset,sizeof(UINT64));
	}

	BYTE*  ByteBuffer::GetData()
	{
		return buffer;
	}

	VOID ByteBuffer::DeleteData() {
		if (buffer != NULL)
		{
			delete[] buffer;
			buffer = NULL;
			Valid = false;
		}
	}

}


