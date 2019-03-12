#ifndef _BYTE_BUFFER_H_
#define _BYTE_BUFFER_H_
#include "Common.h"
namespace AmQ
{
	class ByteBuffer
	{
	public:
		bool Valid;
		int  BufferID;
		//static bool IsLittleEndian;
	private:
		BYTE * buffer;
	public:
		ByteBuffer();
		~ByteBuffer();
	public:
		BYTE GetByte(UINT offset);

		DOUBLE GetDouble(UINT offset);

		FLOAT GetFloat(UINT offset);

		INT GetInt(UINT offset);

		INT64 GetLong(UINT offset);

		SBYTE GetSbyte(UINT offset);

		SHORT GetShort(UINT offset);

		USHORT GetUShort(UINT offset);

		UINT GetUInt(UINT offset);

		UINT64 GetULong(UINT offset);

		BYTE*  GetData();

	friend class ByteBufferManager;
	};
	extern ByteBuffer s_ByteBuffer;
}
#endif


